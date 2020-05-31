namespace exam483.chapter2._Types
{
    class Skill6_Lifecycle
    {
        /*
         * Сборка мусора происходит только тогда,
         * когда объем доступной памяти для новых объектов падает ниже порогового значения
         *
         * Финализация - помещение финализируемых объектов в очередь финализации, освобождение будет выполнено при следующей сборке
         * SuppressFinalize <-> ReRegisterFinalizer
         *
         */

        class Person
        {
            long[] personArray;
            public Person(int amount)
            {
                personArray = new long[amount];
            }
        }
        public void Do()
        {
            // GCWork();
        }

        private static void GCWork()
        {
            for (int i = 0; i < 100000000000; i++)
            {
                Person p = new Person(1000000);
                System.Threading.Thread.Sleep(3);
            }
        }
    }
}
