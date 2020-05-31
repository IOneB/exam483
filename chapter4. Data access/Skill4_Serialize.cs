using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace exam483.chapter4._Data_access
{
    class Skill4_Serialize
    {
        /*
         * Binary требует Serializable
         * DataContractSerializer требует атрибуты DataContract и DataMember
         * ISerializable - своя сериализация
         * OnSerialized(de- -ing) - отслеживать события
         * XmlSerializer, JsonSerializer, SoapSerializer
         */

        public void Do()
        {
            BinarySerialize();
        }

        [Serializable]
        private class X : ISerializable
        {
            public int Age;
            public string Name;

            protected X(SerializationInfo info, StreamingContext context)
            {
                Name = info.GetString("Name");
            }
            public X() { }

            [OnDeserialized]
            public void SerEvent(StreamingContext context)
            {
                Age = 15;
                Console.WriteLine("Внутренняя пустота");
            }

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("Name", Name);
            }

            public override string ToString()
            {
                return Age + " " + Name;
            }
        }

        private void BinarySerialize()
        {
            var binary = new BinaryFormatter();
             
            var x = new X{ Age = 10, Name = "a" };
            //serialize
            using var stream = new MemoryStream();
            binary.Serialize(stream, x);

            //read
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            Console.WriteLine(reader.ReadToEnd());

            //deserialize
            stream.Position = 0;
            Console.WriteLine(binary.Deserialize(stream));
        }
    }
}
