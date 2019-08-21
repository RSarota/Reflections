using Newtonsoft.Json;
using System;
using System.Runtime.InteropServices;
using System.Reflection;
using Newtonsoft.Json.Converters;

namespace Reflections
{
    public enum Sex
    {
        Man,
        Woman
    }
    [Serializable]
    [JsonObject]
    public class Person
    {
        public static int ID=0;
        [JsonIgnore]
        public int UniqueID { get; set; }
        [PersonDescription("Person name")]
        [JsonProperty]
        public string Name { get; set; }
        [PersonDescription("Person surname")]
        [JsonProperty]
        public string Surname { get; set; }
        [PersonDescription("Person age")]
        [JsonProperty]
        public int Age { get; set; }
        [PersonDescription("Person date of birth")]
        [JsonProperty]
        public DateTime DateOfBirth { get; set; }
        [PersonDescription("Sex")]
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public Sex Sex { get; set; }
        [PersonDescription("Is person married?")]
        [JsonProperty]
        public bool IsMarried { get; set; }

        public Person()
        {
            UniqueID = ID++;
        }
        public Person(string name, string surname, int age, string dateOfBirth, Sex sex, bool isMarried)
        {
            UniqueID = ID++;
            Name = name;
            Surname = surname;
            Age = age;
            DateOfBirth = DateTime.Parse(dateOfBirth);
            Sex = sex;
            IsMarried = isMarried;
        }

        public override string ToString()
        {
            
            return $"{Name} {Surname}, Age: {Age}, Date of birth: {DateOfBirth}, Sex: {Sex}, Married: {IsMarried}";
        }

        public static Person AddNewPerson()
        {
            var personType = typeof(Person);
            var newPerson = Activator.CreateInstance(personType);
            var properties = personType.GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                var attr = properties[i].GetCustomAttributes<PersonDescriptionAttribute>();
                var propType = properties[i].PropertyType;
                //Console.WriteLine(propType);
                foreach (var a in attr)
                {
                    Console.WriteLine(a.Description);
                    var input = Console.ReadLine();
                    properties[i].SetValue(newPerson, ParseInput(input, propType));                    
                }
            }


            return (Person)newPerson;
        }
        public static dynamic ParseInput(string input, Type type)
        {
            dynamic output=default;
            switch (type.Name)
            {
                case nameof(String):
                    output = input;
                    break;
                case nameof(Int32):
                    int.TryParse(input, out int o1);
                    output = o1;
                    break;
                case nameof(Boolean):
                    Boolean.TryParse(input, out bool o2);
                    output = o2;
                    break;
                case nameof(DateTime):
                    DateTime.TryParse(input, out DateTime o3);
                    output = o3;
                    break;
                case nameof(Reflections.Sex):
                    switch (input)
                    {
                        case "Man":
                            output = Reflections.Sex.Man;
                            break;
                        case "Woman":
                            output= Reflections.Sex.Man;
                            break;
                    }
                    break;
            }
            return output;
        }

    }
}