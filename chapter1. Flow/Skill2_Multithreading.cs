using System;
using System.Threading;
using System.Threading.Tasks;

namespace exam483.chapter1._Flow
{
    class Skill2_Multithreading
    {

        /*
         * lock
         * Преимущество Monitor (гибрид) - TryEnter
         * Пользовательск. - Interlocked, volatile 
         */

        public void Do()
        {
            // InterruptTask();


        }

        private void InterruptTask()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var token = cts.Token;

            var task = Task.Run(() => {
                while (!token.IsCancellationRequested)
                {
                    Console.WriteLine("wait");
                    Thread.Sleep(500);
                    token.ThrowIfCancellationRequested(); // Исключение, но не положит систему
                }
            });
            Console.WriteLine("Нажмите любую клавишу");
            Console.ReadKey();
            cts.Cancel();
            // task.Wait(); - необработанное исключение Aggregate
            Console.WriteLine("Задача отменена");
        }
    }
}
