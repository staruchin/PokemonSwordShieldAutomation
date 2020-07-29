using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.Utilities
{
    public static class TaskExtensions
    {
        public static void NoWait(this Task task)
        {
            if (task == null) { throw new ArgumentNullException(nameof(task)); }
        }
    }
}
