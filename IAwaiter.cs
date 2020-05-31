using System.Runtime.CompilerServices;

namespace exam483
{
    public interface IAwaiter : INotifyCompletion
    {
        bool IsCompleted { get; }
        void GetResult() { }
    }
}