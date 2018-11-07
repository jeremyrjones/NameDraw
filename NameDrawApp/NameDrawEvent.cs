using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jones.jeremy.namedraw
{
    internal static class NameDrawEvent
    {
        private static int NameDrawRetryLimit = 100;
        private static List<NameDrawEventResult> NameDrawEventResults = new List<NameDrawEventResult>();

        internal static NameDrawConfig CreateNameDrawConfig(
            IList<Person> persons,
            IList<DrawRule> drawRules,
            IList<NameDrawItem> nameDrawItems,
            DateTime nameDrawDateTime,
            string nameDrawDescription,
            IList<INameDrawOutput> outputProviders)
        {
            return new NameDrawConfig
            {
                Participants = persons,
                DrawRules = drawRules,
                Items = nameDrawItems,
                NameDrawDateTime = nameDrawDateTime,
                NameDrawDescription = nameDrawDescription,
                OutputProviders = outputProviders
            };
        }

        internal static NameDrawEventResult GetNameDrawEventResult(int resultId)
        {
            return NameDrawEventResults.Single(result => result.EventId == resultId);
        }

        internal static IList<NameDrawResult> ExecuteNameDraw(NameDrawConfig config)
        {
            Console.WriteLine($"Executing namedraw with config id: {config.ConfigId}");

            int drawAttempts = 1;
            var result = ExecuteDraw(config);
            while (!ResultsAreValid(result, config) && drawAttempts < NameDrawRetryLimit)
            {
                result = ExecuteDraw(config);
                drawAttempts++;
            }

            if (drawAttempts < NameDrawRetryLimit)
            {
                Console.WriteLine($"Took {drawAttempts} draw attempts to satisfy all rules.");
                OutputResult(result, config.OutputProviders);

                NameDrawEventResults.Add(new NameDrawEventResult
                {
                    Config = config,
                    Results = result
                });

                return result;
            }

            throw new Exception("could not satisfy name-draw rule requirements based on the current number of draw attempts!");
        }

        private static IList<NameDrawResult> ExecuteDraw(NameDrawConfig config)
        {
            var participants = config.Participants;
            IList<NameDrawResult> result = new List<NameDrawResult>();

            foreach (var item in config.Items)
            {
                // execute draw for this item

                // list of drawers, random order
                IList<Person> drawers = participants
                    .Select(person => new Person {EmailAddress = person.EmailAddress, Name = person.Name}).ToList();
                drawers = Randomize(drawers);

                // list of drawees, random order
                IList<Person> drawees = participants
                    .Select(person => new Person { EmailAddress = person.EmailAddress, Name = person.Name }).ToList();
                drawees = Randomize(drawees);

                for (int i = 0; i < participants.Count; i++)
                {
                    NameDrawResult nameDrawResult =
                        new NameDrawResult {From = drawers[i], To = drawees[i], Item = item};
                    result.Add(nameDrawResult);
                }
            }

            return result;
        }

        private static bool ResultsAreValid(IList<NameDrawResult> resultList, NameDrawConfig config)
        {
            foreach (var nameDrawResult in resultList)
            {
                // don't draw self
                // TODO: for now, using email address as unique id... once we use a db, can use an auto-increment field
                if (nameDrawResult.From.EmailAddress == nameDrawResult.To.EmailAddress)
                {
                    return false;
                }

                // verify draw rules are satisified
                foreach (var drawRule in config.DrawRules)
                {
                    // TODO: for now, using email address as unique id... once we use a db, can use an auto-increment field
                    if (drawRule.RuleType == DrawRule.Rule.Exclude)
                    {
                        if (drawRule.From.EmailAddress == nameDrawResult.From.EmailAddress
                            && drawRule.To.EmailAddress == nameDrawResult.To.EmailAddress)
                        {
                            return false;
                        }
                    }
                }
            }

            foreach (var drawRule in config.DrawRules)
            {
                bool foundIt = false;
                if (drawRule.RuleType == DrawRule.Rule.Include)
                {
                    foreach (var nameDrawResult in resultList)
                    {
                        if (drawRule.From.EmailAddress == nameDrawResult.From.EmailAddress
                            && drawRule.To.EmailAddress == nameDrawResult.To.EmailAddress)
                        {
                            foundIt = true;
                            break;
                        }
                    }

                    if (!foundIt)
                    {
                        return false;
                    }
                }
            }

            // TODO: still need to add one more rule to ensure one person does not draw the same person for multiple items

            return true;
        }

        private static void OutputResult(IList<NameDrawResult> results, IList<INameDrawOutput> outputProviders)
        {
            foreach (var outputProvider in outputProviders)
            {
                outputProvider.OutputResult(results);
            }
        }

        private static IList<Person> Randomize(IList<Person> personList)
        {
            Random r = new Random();
            return personList.OrderBy(x => (r.Next())).ToList();
        }
    }
}
