using System;
using System.Threading;
using System.Threading.Tasks;

namespace exam483.chapter1._Flow
{
    class Skill4_Events
    {
        /* В событиях использовать EventHandler!
         *
         *
         */

        private class AlarmEventArgs : EventArgs
        {
            public string Info { get; set;}
        }

        delegate void Deleg();

        event EventHandler<AlarmEventArgs> A = delegate { };

        public void Do()
        {
            A.Invoke(this, new AlarmEventArgs { Info="Все хорошо" }); // Правильное использование событий.
                                                                      // Проверка не нужна из-за пустого делегата при инициализации
            Deleg deleg = new Deleg(delegate { });
            Delegate @delegate = deleg;
            deleg.Invoke();
            @delegate.DynamicInvoke();
        }
    }
}
