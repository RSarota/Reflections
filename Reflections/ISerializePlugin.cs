using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflections
{
    public interface ISerializePlugin
    {
        string Name { get; }
        string Desctription { get; }
        void Serialize(Person person);
        List<Person> Deserialize();
    }
}
