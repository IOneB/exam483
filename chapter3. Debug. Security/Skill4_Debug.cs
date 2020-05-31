#define A
#define Condition

using System;
using System.Diagnostics;

namespace exam483.chapter3._Debug._Security
{

    class Skill4_Debug
    {
        /*
         *
         */

        public void Do()
        {
            DoCondition();
#if DEBUG
#if A
            global::System.Console.WriteLine("Debug");
#else
            global::System.Console.WriteLine("NotDebug");
#endif
#endif
//#error aaaaaaaaaaaaaaaaaaaaa
        }

        [Conditional("Condition")]
        private static void DoCondition()
        {
            Console.WriteLine("Condition");
        }
    }
}
