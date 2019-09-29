using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleAsync
{
    public static class Utils
    {
        public static string CurrentThread
        {
            get
            {
                return "(thread: " + 
                    Thread.CurrentThread.ManagedThreadId.ToString() + ")";
            }
        }
    }
}
