using System.IO;

namespace exam483.chapter2._Types
{
    class Skill7_String
    {
        /*
         * StringReader
         * StringWriter
         * StringBuilder
         * StringComparison позволяет задать культуру сравнения и игнорировать case
         *
         *
         */

        public void Do()
        {
            var sw = new StringWriter();
            sw.Write("aaaaaaa");
            sw.WriteLine("b");
            sw.Close();
            System.Console.WriteLine(sw.ToString());
        }
    }
}
