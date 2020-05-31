using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace exam483.chapter3._Debug._Security
{
    class Skill1_ValidateInput
    {
        /*
         *Если вы хотите сохранить и загрузить частные свойства в классе,
         * вам нужно пометить эти элементы [JsonProperty]атрибутом
         *System.Xml.Serialization - нужнен конструктор без параметров, XmlSerializer
         * 
         * Если вы храните только строки , вы можете использовать StringCollectionи StringListклассы
         */


        public void Do()
        {
            StringCollection collection = new StringCollection();

            foreach (var item in "A та uyq".Split())
            {
                collection.Add(item);
            }
        }
    }
}
