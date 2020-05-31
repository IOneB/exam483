using System;

namespace exam483.chapter1._Flow
{
    class Skill5_Exception
    {

        /*
         *
         * catch ex when ex.Value > 1 - прикольно
         */

        public void Do()
        {
            // FailWithoutFinally();
        }

        private static void FailWithoutFinally()
        {
            try
            {
                Environment.FailFast("Fast");
            }
            catch { }
            finally
            {
                Console.WriteLine("AAAAAAAA");
            }
        }
    }
}
