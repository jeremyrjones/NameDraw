using System;

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

            NameDrawConfig nameDrawConfig = new NameDrawConfig();

            // add participants
            nameDrawConfig.Participants.Add(person1);
            nameDrawConfig.Participants.Add(person2);
            nameDrawConfig.Participants.Add(person3);
            nameDrawConfig.Participants.Add(person4);
            nameDrawConfig.Participants.Add(person5);

            // TODO: draw rules are not yet implemented
            // add draw rules (person1 must draw person3; person2 must not draw person4)
            DrawRule includeDrawRule = new InclusionDrawRule( person1, person3);
            DrawRule excludeDrawRule = new ExclusionDrawRule( person2, person4);
            nameDrawConfig.DrawRules.Add(includeDrawRule);
            nameDrawConfig.DrawRules.Add(excludeDrawRule);

            // add items
            NameDrawItem item1 = new NameDrawItem {ItemAmount = 20};
            NameDrawItem item2 = new NameDrawItem {ItemAmount = 10};
            nameDrawConfig.Items.Add(item1);
            nameDrawConfig.Items.Add(item2);

            // set the date and time
            nameDrawConfig.NameDrawDateTime = new DateTime(2018, 12, 25, 12, 0, 0);

            // add an event description
            nameDrawConfig.NameDrawDescription = "Meet at person1's house";

            // configure output providers for the results of the name draw selections
            // TODO: try to get an email provider working
            ConsoleOutput consoleOutput = new ConsoleOutput();
            nameDrawConfig.OutputProviders.Add(consoleOutput);

            NameDrawEvent nameDrawEvent = new NameDrawEvent {DrawConfig = nameDrawConfig};
            nameDrawEvent.ExecuteNameDraw();
        }
    }
}
