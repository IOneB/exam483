using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace exam483.chapter4._Data_access
{
    class Skill1_IO
    {
        /*
         * FileStream, File(нет асинхронности), StreamReader, StreamWriter - работа с файлами
         * GZipStream - промежуточный стрим между File -> GZip -> Writer/Reader для сжатия (Compress/Decompress)
         * Работа с дисками - DriveInfo, аттрибуты файлов - FileInfo
         * Directory, DirectoryInfo, Path(Может изменить расширение, можно генерировать имена файлов)
         *
         * Network - основа - WebRequest - запрос по URI(http, ftp...)
         * Ответ - WebResponse - значение ответа в потоке
         *
         * Более продвинуто - WebClient, HttpClient
         */

        object Log
        {
            set
            {
                Console.WriteLine(value);
            }
        }

        public void Do()
        {
            // FileStream();
            // FileInfoWork();
            // WebReq();
            // Client();

        }

        private void Client()
        {
            var client = new WebClient(); //HttpClient
            Console.WriteLine(client.DownloadString("https://habr.com/ru/"));
        }

        private void WebReq()
        {
            WebRequest request = WebRequest.Create("https://habr.com/ru/");
            var response = request.GetResponse();

            using var reader = new StreamReader(response.GetResponseStream());
            Console.WriteLine(reader.ReadToEnd());
        }

        private void FileInfoWork()
        {
            Log = new FileInfo("out").Attributes;
        }

        private void FileStream()
        {
            var fs = new FileStream("out", FileMode.OpenOrCreate, FileAccess.Write);
            byte[] bytes = Encoding.UTF8.GetBytes("text");
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            fs = new FileStream("out", FileMode.OpenOrCreate, FileAccess.Read);
            byte[] readBytes = new byte[bytes.Length];
            fs.Read(readBytes, 0, readBytes.Length);
            fs.Close();
            Console.WriteLine(Encoding.UTF8.GetString(readBytes));
        }
    }
}
