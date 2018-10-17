using System;
using System.Collections.Generic;
using System.Text;

namespace jones.jeremy.namedraw
{
    internal interface INameDrawOutput
    {
        void OutputResult(IList<NameDrawResult> DrawResults);
    }
}
