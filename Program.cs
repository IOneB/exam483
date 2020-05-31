using exam483.chapter1._Flow;
using exam483.chapter2._Types;
using exam483.chapter3._Debug._Security;
using exam483.chapter4._Data_access;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace exam483
{
    partial class Program
    {
        static event Action<int> @event;
        static async Task Main(string[] args)
        {
            //chapter 1
            //new Skill1_Task().Do();
            //new Skill2_Multithreading().Do();
            //new Skill3_ProgramFlow().Do();
            //new Skill4_Events().Do();
            //new Skill5_Exception().Do();

            //chapter 2
            //new Skill1_Create().Do();
            //new Skill2_Consuming().Do();
            //new Skill3_Encapsulation().Do();
            //new Skill4_Hierarchy().Do();
            //new Skill5_Runtime().Do();
            //new Skill6_Lifecycle().Do();
            //new Skill7_String().Do();

            //chapter 3
            //new Skill1_ValidateInput().Do();
            //new Skill2_Encryption().Do();
            //new Skill3_Assemblies().Do();
            //new Skill4_Debug().Do();
            //new Skill5_Diagnostic().Do();

            //chapter 4
            //new Skill1_IO().Do();
            //new Skill2_Consume().Do();
            //new Skill3_QueryLinq().Do();
            //new Skill4_Serialize().Do();
            //new Skill5_Store().Do();

            // FakeQueryProvider();
            // XMLLinqToObject();
            //var sw = Stopwatch.StartNew();
            //ParallelLinq();
            //Console.WriteLine(sw.ElapsedMilliseconds);
            //await new A();


            Console.ReadKey();
        }

        private static void ParallelLinq()
        {
            ParallelEnumerable
                .Range(0, 100).WithDegreeOfParallelism(10).WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                .ForAll(x => Thread.Sleep(100));
            Console.WriteLine("Все");
        }

        private static void XMLLinqToObject()
        {
            var xml = new XElement("root",
                        new XElement("child",
                            "grand-child", "text"),
                        new XElement("second-child",
                            new XAttribute("name", "Vasya")));
            Console.WriteLine(xml.ToString());
        }

        private static void FakeQueryProvider()
        {
            var res = (from x in new FakeQuery<string>()
                       where x.StartsWith("abc")
                       select x.Length);
            foreach (var item in res)
            {
                Console.WriteLine(res);
            }
            Console.WriteLine(res.Average());
        }
    }

    class FakeQuery<T> : IQueryable<T>
    {
        internal FakeQuery(IQueryProvider queryProvider, Expression expression)
        {
            Provider = queryProvider;
            Expression = expression;
            ElementType = typeof(T);
        }
        internal FakeQuery() : this(new FakeQueryProvider(), null)
        {
            Expression = Expression.Constant(this);
        }

        public Type ElementType { get; }
        public Expression Expression { get; }

        public IQueryProvider Provider { get; }

        public IEnumerator<T> GetEnumerator()
        {
            return Enumerable.Empty<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public override string ToString()
        {
            return "FakeProvider";
        }
    }

    internal class FakeQueryProvider : IQueryProvider
    {
        public IQueryable<T> CreateQuery<T>(Expression expression)
        {
            return new FakeQuery<T>(this, expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            Type queryType = typeof(FakeQuery<>).MakeGenericType(expression.Type);
            object[] constructorArgs = new object[] { this, expression };
            return (IQueryable)Activator.CreateInstance(queryType, constructorArgs);
        }

        public object Execute(Expression expression) => default;

        public TResult Execute<TResult>(Expression expression) => default;
    }
}
