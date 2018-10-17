using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jones.jeremy.namedraw
{
    internal class NameDrawEvent
    {
        internal NameDrawConfig DrawConfig { get; set; }
        internal IList<NameDrawResult> DrawResults { get; private set; }

        internal void ExecuteNameDraw()
        {
            PopulateResults();
            OutputResults();
        }

        private void PopulateResults()
        {
            var participants = DrawConfig.Participants;
            IList<NameDrawResult> result = new List<NameDrawResult>();

            // TODO: refine this algorithm, and implement inclusions and exclusions
            // TODO: don't allow self to be drawn

            foreach (var item in DrawConfig.Items)
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

            DrawResults = result;
        }

        private void OutputResults()
        {
            foreach (var outputProvider in DrawConfig.OutputProviders)
            {
                outputProvider.OutputResult(DrawResults);
            }
        }

        private static IList<Person> Randomize(IList<Person> personList)
        {
            Random r = new Random();
            return personList.OrderBy(x => (r.Next())).ToList();
        }
    }
}
