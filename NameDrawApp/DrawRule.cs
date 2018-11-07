namespace jones.jeremy.namedraw
{
    internal abstract class DrawRule
    {
        protected internal enum Rule
        {
            Exclude,
            Include
        }

        internal Person From { get; set; }
        internal Person To { get; set; }
        internal Rule RuleType { get; set; }

        protected DrawRule(Person from, Person to, Rule rule)
        {
            From = from;
            To = to;
            RuleType = rule;
        }
    }
}
