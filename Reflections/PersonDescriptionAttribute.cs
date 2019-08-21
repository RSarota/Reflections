using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflections
{
    sealed class PersonDescriptionAttribute:System.Attribute
    {
        public string Description { get; set; }

        public PersonDescriptionAttribute(string description)
        {
            Description = description;
        }
        public PersonDescriptionAttribute()
        {

        }
    }
}
