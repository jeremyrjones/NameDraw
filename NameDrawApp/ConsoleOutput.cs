using System;
using System.Collections.Generic;
using System.Text;

namespace jones.jeremy.namedraw
{
    internal class ConsoleOutput : INameDrawOutput
    {
        public void OutputResult(IList<NameDrawResult> drawResults)
        {
            Console.WriteLine("Name Draw Results:");

            foreach (var nameDrawResult in drawResults)
            {
                Console.WriteLine($"Person {nameDrawResult.From.Name} drew {nameDrawResult.To.Name}, for gift amount: {nameDrawResult.Item.ItemAmount:c}");
            }
        }
    }
}
