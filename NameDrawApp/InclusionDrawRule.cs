using System;
using System.Collections.Generic;
using System.Text;

namespace jones.jeremy.namedraw
{
    internal class InclusionDrawRule : DrawRule
    {
        protected internal InclusionDrawRule(Person from, Person to) : base(from, to, Rule.Include)
        {
        }
    }
}
