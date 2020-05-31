using System;
using System.Linq;
using System.Xml.Linq;

namespace exam483.chapter4._Data_access
{
    class Skill3_QueryLinq
    {
        /*
         * join
         */


        public void Do()
        {
            // JOIN();
            // xmlLinq();
        }

        private void xmlLinq()
        {
            string XMLText =
                "<MusicTracks> " +
                    "<MusicTrack> " +
                        "<Artist>Rob Miles</Artist>  " +
                        "<Title>My Way</Title>  " +
                        "<Length>150</Length>" +
                    "</MusicTrack>" +
                    "<MusicTrack>" +
                        "<Artist>Immy Brown</Artist>  " +
                        "<Title>Her Way</Title>  " +
                        "<Length>200</Length>" +
                    "</MusicTrack>" +
                "</MusicTracks>";

            var x = from xml in XDocument.Parse(XMLText).Descendants("MusicTracks")
                    select xml;
            x.ToList().ForEach(x => Console.WriteLine(x.Element("Title").FirstNode));
        }

        private void JOIN()
        {
            var first = new[]
            {
                new { Id = 1, Name = "a"},
                new { Id = 2, Name = "b"},
                new { Id = 3, Name = "c"},
            };
            var second = new[]
{
                new { Id = 1, Age = 11, fId = 1},
                new { Id = 2, Age = 12, fId = 1},
                new { Id = 3, Age = 13, fId = 2},
                new { Id = 4, Age = 14, fId = 2},
                new { Id = 5, Age = 15, fId = 3},
                new { Id = 6, Age = 16, fId = 0},
            };

            var joined = from f in first.Skip(0).Take(2) where f.Id < 4
                         join s in second on f.Id equals s.fId
                         group f by f.Name
                         into fs
                         select new
                         {
                             Name = fs.Key,
                             Count = fs.Count()
                         };


            Console.WriteLine(string.Join("\n", joined));
            Console.WriteLine(string.Join("\n", second.GroupBy(x => x.fId, x => x.Age).Select(x => System.Text.Json.JsonSerializer.Serialize(x))));
        }
    }
}
