using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jones.jeremy.namedraw
{
    internal static class NameDrawEvent
    {
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

        internal static IList<NameDrawResult> ExecuteNameDraw(NameDrawConfig config)
        {
            var result = ExecuteDraw(config);
            OutputResult(result, config.OutputProviders);
            return result;
        }

        private static IList<NameDrawResult> ExecuteDraw(NameDrawConfig config)
        {
            var participants = config.Participants;
            IList<NameDrawResult> result = new List<NameDrawResult>();

            // TODO: refine this algorithm, and implement inclusions and exclusions
            // TODO: don't allow self to be drawn

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
