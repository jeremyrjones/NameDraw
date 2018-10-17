namespace jones.jeremy.namedraw
{
    internal abstract class DrawRule
    {
        protected enum Rule
        {
            Exclude,
            Include
        }

        Person From { get; set; }
        Person To { get; set; }
        Rule RuleType { get; set; }

        protected DrawRule(Person from, Person to, Rule rule)
        {
            From = from;
            To = to;
            RuleType = rule;
        }
    }
}
