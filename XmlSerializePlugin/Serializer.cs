using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerializePlugin
{
    public static class Serializer
    {
        public static void Serialize(Action<Stream, object> serialize, object toSerialize, string fileName)
        {
            using (Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                serialize(fStream, toSerialize);
            }
        }

        public static object Deserialize(Func<Stream, object> deserialize, string fileName)
        {
            object desObject;
            using (Stream fStream = File.OpenRead(fileName))
            {
                desObject = deserialize(fStream);
            }

            return desObject;
        }
    }
}
