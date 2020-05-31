using System;

namespace exam483
{
    partial class Program
    {
        class A 
        {
            public IAwaiter GetAwaiter()
            {
                throw new Exception();
            }
        }
    }
}
