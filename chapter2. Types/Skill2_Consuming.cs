using System.Dynamic;

namespace exam483.chapter2._Types
{
    class Skill2_Consuming
    {
        /*
         * ExpandoObject в связке с dynamic позволяет добавлять любые свойство в объект
         * 
         */

        public void Do()
        {
            // ExpandoObject();

        }

        private static void ExpandoObject()
        {
            dynamic d = new ExpandoObject();
            d.Age = 15;
            System.Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(d));
        }
    }
}
