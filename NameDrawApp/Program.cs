using System;
using System.Collections.Generic;

namespace jones.jeremy.namedraw
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the name-draw app!");

            // sample execution:

            Person person1 = new Person { EmailAddress = "person1@test.com", Name = "person1" };
            Person person2 = new Person { EmailAddress = "person2@test.com", Name = "person2" };
            Person person3 = new Person { EmailAddress = "person3@test.com", Name = "person3" };
            Person person4 = new Person { EmailAddress = "person4@test.com", Name = "person4" };
            Person person5 = new Person { EmailAddress = "person5@test.com", Name = "person5" };

            IList<Person> persons = new List<Person>
            {
                person1,
                person2,
                person3,
                person4,
                person5
            };

            // TODO: draw rules are not yet implemented
            // add draw rules (person1 must draw person3; person2 must not draw person4)
            IList<DrawRule> drawRules = new List<DrawRule>
            {
                new InclusionDrawRule(person1, person3),
                new ExclusionDrawRule(person2, person4)
            };

            // add items
            IList<NameDrawItem> nameDrawItems = new List<NameDrawItem>
            {
                new NameDrawItem {ItemAmount = 20},
                new NameDrawItem {ItemAmount = 10}
            };

            // set the date and time
            DateTime nameDrawDateTime = new DateTime(2018, 12, 25, 12, 0, 0);

            // add an event description
            string nameDrawDescription = "Meet at person1's house";

            // configure output providers for the results of the name draw selections
            // TODO: try to get an email provider working
            ConsoleOutput consoleOutput = new ConsoleOutput();

            IList<INameDrawOutput> outputProviders = new List<INameDrawOutput>
            {
                consoleOutput
            };

            var nameDrawConfig = NameDrawEvent.CreateNameDrawConfig(
                persons, 
                drawRules, 
                nameDrawItems, 
                nameDrawDateTime, 
                nameDrawDescription,
                outputProviders);

            NameDrawEvent.ExecuteNameDraw(nameDrawConfig);
        }
    }
}
