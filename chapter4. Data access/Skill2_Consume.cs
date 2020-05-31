using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

namespace exam483.chapter4._Data_access
{
    class Skill2_Consume
    {
        /* Использовать параметризованные запросы!
         * Есть асинхронные версии
         * XmlTextReader - читает XML из потока
         *
         * WCF - интерфейс ServiceContract, метод OperationContract
         *
         *
         * 
         */

        public void Do()
        {
            // SqlConnect();
            // XmlRead();
            // XMLDom();

            // XLinq();
        }

        private static void XLinq()
        {
            var doc = XDocument.Load("doc.xml");
            var r = from p in doc.Descendants("Product")
                    join s in doc.Descendants("Supplier") on (int)p.Attribute("SupplierID") equals (int)s.Attribute("SupplierID")
                    where (float)p.Attribute("Price") > 10
                    select new
                    {
                        Product = (string)p.Attribute("Name"),
                        Sup = (string)s.Attribute("Name")
                    };

            Console.WriteLine(string.Join(", ", r.ToList()));
        }

        private void XMLDom()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<?xml version=\"1.0\" encoding=\"ISO - 8859 - 1\"?>" +
                "<note> " +
                "<to>Tove</to>" +
                "<from> Jani </from>" +
                "<heading> Reminder </heading>" +
                "<body> Don't forget me this weekend!</body>  " +
                "</note>");

            var root = xml.DocumentElement;
            Console.WriteLine(root.FirstChild.Value); // Ничего непонятно
        }

        private void XmlRead()
        {
            string XMLDocument = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
    "<MusicTrack xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"" +
    "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"> " +
    "<Artist>Rob Miles</Artist>" +
    "<Title>My Way</Title> " +
    "<Length>150</Length> " +
    "</MusicTrack>";

            using StringReader sreader = new StringReader(XMLDocument);
            XmlTextReader reader = new XmlTextReader(sreader);
            while (reader.Read())
            {
                Console.WriteLine("Type: {0}, Name: {1}, Value: {2}", reader.NodeType, reader.Name, reader.Value);
            }
        }

        private async Task SqlConnect()
        {
            var connstring = "ConnectionString";
            using SqlConnection connection = new SqlConnection(connstring);
            await connection.OpenAsync();
            var command = new SqlCommand("Select 1, 'aa' as text", connection); // Select * from X where y = @art
            var reader = await command.ExecuteReaderAsync();                               // command.Parameters.AddWithValue("@art", "2")
            while (await reader.ReadAsync())
            {
                Console.WriteLine(reader.GetInt32(0) + reader["text"].ToString());
            }
        }
    }
}
