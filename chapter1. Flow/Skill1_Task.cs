using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace exam483.chapter1._Flow
{
    class Skill1_Task
    {
        /*
         * Task.Parallel - три метода
         *      Invoke - параллельно выполняет набор делегатов action, приостанавливает флоу до завершения всех задач
         *      Foreach - принимает коллекцию и Action                      могут иметь ParallelLoopState, который позволяет итерируемому коду контролировать процесс итерации.
         *      For -                                                       также возвращают значение типа ParallelLoopResult для определения успешного завершения
         *
         *          Stop и Break, Break гарантирует выполнение всех предыдущих итераций
         *
         * PLINQ дает возможность выполняеть AsParallel.
         *          Метод AsParallel проверяет запрос, чтобы определить,
         *          ускорит ли его использование параллельной версии. ForceParallelism - заставляет действовать параллельно
         *          
         *          параллельные исключения создаются как AggregateException
         */




        void DoWork()
        {
            Console.WriteLine("Start Work");
            Thread.Sleep(2000);
            Console.WriteLine("Stop Work");
        }


        public void Do()
        {
            // ParallelQuery();

            var t = new Task(DoWork);
            t.Wait();
        }

        private static void ParallelQuery()
        {
            var humans = new[] { new { Age = 10, Name = "A" }, new { Age = 20, Name = "b" }, new { Age = 30, Name = "c" }, new { Age = 40, Name = "d" }, };

            var result = (from h in humans.AsParallel() //Parallel Query
                            .WithDegreeOfParallelism(4)
                            .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                          where h.Age > 10
                          orderby h.Age
                          select new
                          {
                              h.Name
                          });

            result.ForAll(h => Console.WriteLine(h));
        }
    }
}
