using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Reflections;
using Json.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;


namespace JsonSerializePlugin
{
    public class JsonSerializer : ISerializePlugin
    {
        public string Name
        {
            get { return "JSON SERIALIZER"; }
        }
        public string Desctription
        {
            get { return "Serialize objects to JSON files or deserialize files to objects"; }
        }

        public JsonSerializer()
        {

        }

        public void Serialize(Person person)
        {
            var serializedPerson = JsonConvert.SerializeObject(person,Formatting.Indented);
            var Jobject = new JObject();
            Jobject.Add(nameof(Person),serializedPerson);
            using(StreamWriter streamWriter=new StreamWriter("People/"+person.UniqueID+".json"))
            {
                streamWriter.Write(serializedPerson);
            }
        }
        public List<Person> Deserialize()
        {
            int count = 0;
            
            int fileCount = Directory.GetFiles("people/").Length;
            var list = new List<Person>();
            while (count != fileCount)
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
