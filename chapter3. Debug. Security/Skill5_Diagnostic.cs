using System.Diagnostics;

namespace exam483.chapter3._Debug._Security
{
    class Skill5_Diagnostic
    {
        /* Indent - отступ
         *  Debug - вывод, отсутствует в релизе, Trace - вывод с доп инфой, 
         * Assert - утверждение, остановка при условии
         *
         *  ConsoleTraceListener Отправляет вывод на консоль.
            DelimitedTextTraceListener Отправляет вывод в TextWriter
            EventLogTraceListener Отправляет вывод в журнал событий
            EventSchemaTraceListener Отправляет вывод в файл в кодировке XML, соответствующий схеме журнала событий.
            TextWriterTraceListener Отправляет вывод в указанный TextWriter
            XMLWriterTraceListener Отправляет вывод в формате XML на устройство записи XML
         *
         * StopWatch
         */

        public void Do()
        {
            Debug.WriteLine("Запуск программы");
            Debug.Indent();
            Debug.WriteLine("Внутри функции");
            Debug.Unindent();
            Debug.WriteLine("Вне функции");
            string customerName = "Rob";
            Debug.WriteLineIf(string.IsNullOrEmpty(customerName), "Имя пусто");

            Trace.WriteLine("Запуск программы");
            Trace.TraceInformation("Это информационное сообщение");
            Trace.TraceWarning("Это предупреждающее сообщение");
            Trace.TraceError("Это сообщение об ошибке");

            Debug.Assert(string.IsNullOrWhiteSpace(customerName));
        }
    }
}
