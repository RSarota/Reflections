using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Reflections
{
    class Program
    {
        public static void PrintMenu()
        {
            Console.Write("Choose one:\n" +
                "1. Fill out new form\n" +
                "2. Serialize new forms\n" +
                "3. Load last forms\n" +
                "Press any key to quit\n");
        }
        static void Main(string[] args)
        {
            var assembly = Assembly.LoadFile("C:/Users/rafal.sarota/source/repos/Reflections/JsonSerializePlugin/bin/debug/JsonSerializePlugin.dll");
            Type t = assembly.GetType("JsonSerializePlugin.JsonSerializer");
            var Serialize = t.GetMethod("Serialize", new Type[] { typeof(Person) });
            var Deserialize = t.GetMethod("Deserialize");
            
            var jsonSerializer = Activator.CreateInstance(t);

            var people = new List<Person>();

            //Person person1 = new Person()
            //{
            //    Name = "Adam",
            //    Surname = "aaaaaaaaaaaa",
            //    Age = 20,
            //    DateOfBirth = DateTime.Parse("01/30/1994"),
            //    Sex= Sex.Man,
            //    IsMarried = false,
            //};
            //people.Add(person1);

            //Person person2 = new Person()
            //{
            //    Name = "Adam",
            //    Surname = "asczxzxc",
            //    Age = 33,
            //    DateOfBirth = DateTime.Parse("01/30/2000"),
            //    Sex = Sex.Man,
            //    IsMarried = false,
            //};
            //people.Add(person2);

            PrintMenu();
            int.TryParse(Console.ReadLine(), out int number);
            do
            {
                switch (number)
                {
                    case 1:
                        people.Add(Person.AddNewPerson());
                        break;
                    case 2:
                        Console.WriteLine("Select serializer:\n" +
                            "1. JSON\n" +
                            "2. XML");
                        int.TryParse(Console.ReadLine(), out int value);
                        switch (value)
                        {
                            case 1:
                                foreach (var person in people)
                                    Serialize.Invoke(jsonSerializer, new object[] { person });
                                break;
                            case 2:
                                foreach (var person in people)
                                { }
                                break;
                            default:
                                Console.WriteLine("Wrong choice. App will be closed");
                                break;
                        }
                        break;
                    case 3:
                        var peopleDes = (List<Person>)Deserialize.Invoke(jsonSerializer, null);
                        foreach (var person in peopleDes)
                        {
                            Console.WriteLine(person);
                        }
                        break;
                    default:
                        break;
                }
                PrintMenu();
                int.TryParse(Console.ReadLine(), out number);
            } while (number > 0 && number < 5);



            Console.ReadKey();
        }         
    }
}
