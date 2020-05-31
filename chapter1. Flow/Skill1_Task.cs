using System;
using System.Collections.Concurrent;
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
         *          Stop и Break, Break гарантирует выполнение всех предыдущих итераций


         * PLINQ дает возможность выполняеть AsParallel.
         *          Метод AsParallel проверяет запрос, чтобы определить,
         *          ускорит ли его использование параллельной версии. ForceParallelism - заставляет действовать параллельно
         *          
         *          параллельные исключения создаются как AggregateException
         *          ParallelEnumerable.Range.ForAll


         * Task.Run <=> new Task().Start. Run для простых небольших задач
         * Task.Run использует метод Task.Factory.StartNew для создания и запуска простых задач, используя планировщик задач по умолчанию,
         * который использует пул потоков платформы
         * Task.Run имеют установленный параметр TaskCreationOptions.DenyChildAttach
         * Если есть континуе виз то не будет AggregationException. Внешняя среда вообще ничего не узнает


         * Для более детального запуска - таск.фэктори
         *      AttachedToParent - родительская задача завершится только когда завершатся все дочерние
         *      DenyChildAttach - дети - отдельные дочерние


         * Потоки низкоуровневые, задачи - высоко-.
         * Потоки имеют приоритет. Задачи обеспечивают агрегацию исключений, а потоки - нет
         * Лучший способ прервать поток - использовать переменную общего флага.
         * 
         * ThreadStatic - атрибут для создания копии значения СТАТИЧЕСКОЙ переменной в каждом отдельном потоке
         * ThreadLocal<> обертка для создания статического локального значения для каждого потока с помощью делегата (позволяет уникально инициализировать значение)
         *
         * контекст потока - имя потока (если есть),
         *                   приоритет потока, будь то передний план или фон,
         *                   культура потоков (содержит информацию о культуре в значении типа CultureInfo),
         *                   контекст безопасности потока
         *
         *  Потокобезопасные коллекции:
         *          BlockingCollection<T> - очередь, когда у вас есть некоторые задачи, производящие данные, и другие задачи,
         *                                  потребляющие данные. Он обеспечивает потокобезопасное средство добавления и
         *                                  удаления элементов в хранилище данных. Он называется блокирующей коллекцией,
         *                                  потому что действие «Взять» блокирует задачу. Можно задать верхний предел.
         *                                  может выступать в качестве оболочки для других классов одновременной коллекции,
         *                                  включая ConcurrentQueue(по умолч), ConcurrentStack и ConcurrentBag
         *          ConcurrentQueue<T>
         *          ConcurrentStack<T>
         *          ConcurrentBag<T> - неупорядоченная коллекция
         *          ConcurrentDictionary<TKey, TValue>
         */

        void DoWork()
        {
            Console.WriteLine("Start Work");
            Thread.Sleep(2000);
            Console.WriteLine("Stop Work");
        }

        [ThreadStatic]
        static object o = new object(); // инициализация один раз для каждого потока
        ThreadLocal<int> x = new ThreadLocal<int>(() => DateTime.Now.Millisecond);

        public void Do()
        {
            // ParallelQuery();
            // AttachChildTask();
            // BlockingCollection();
        }

        private static void BlockingCollection()
        {
            BlockingCollection<int> data = new BlockingCollection<int>(2);
            data.Add(1);
            data.Add(1);
            Console.WriteLine("Completed");
            data.CompleteAdding(); // completedAddting = true, нельзя больше добавлять
            int i = data.Take();
            i = data.Take();

            //Свойство IsCompleted возвращает true, если коллекция пуста и вызван CompleteAdding
        }

        private static void AttachChildTask()
        {
            Task.Factory.StartNew(() =>
            {
                Task.Factory.StartNew(() => Thread.Sleep(4000), TaskCreationOptions.AttachedToParent);
            }).Wait();
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
