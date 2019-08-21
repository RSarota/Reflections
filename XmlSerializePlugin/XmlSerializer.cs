using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlSerializePlugin
{
    public class XmlSerializer
    {
        public string Name
        {
            get { return "XML SERIALIZER"; }
        }
        public string Desctription
        {
            get { return "Serialize objects to XML files or deserialize files to objects"; }
        }

        public XmlSerializer()
        {

        }

        public void Serialize(Person person)
        {
            var serializedPerson = JsonConvert.SerializeObject(person, Formatting.Indented);
            var Jobject = new JObject();
            Jobject.Add(nameof(Person), serializedPerson);
            using (StreamWriter streamWriter = new StreamWriter("People/" + person.UniqueID + ".json"))
            {
                streamWriter.Write(serializedPerson);
            }
        }
        public List<Person> Deserialize()
        {
            int count = 0;
            int peopleCount = Person.ID;
            var list = new List<Person>();
            while (count != peopleCount)
            {
                using (StreamReader streamReader = new StreamReader("People/" + count + ".json"))
                {
                    var str = streamReader.ReadToEnd();
                    list.Add(JsonConvert.DeserializeObject<Person>(str));
                }
                count++;
            }
            return list;
        }
    }
}
}
