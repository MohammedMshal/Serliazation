﻿using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BinarySerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            var e1 = new Employee
            {
                Id = 1001,
                Fname = "Issam",
                Lname = "A.",
                Benefits = { "Pension", "Health Insurance" }
            };

            string binaryContent = NonSerializeToBinaryString(e1);
            Console.WriteLine(binaryContent);

            Employee e2 = DeserializeFromBinaryContent(binaryContent)!;
            
            Console.ReadKey();
        }

        private static Employee? DeserializeFromBinaryContent(string binaryContent)
        {
            byte[] bytes = Convert.FromBase64String(binaryContent);
            using (var stream = new MemoryStream(bytes))
            {
                DataContractSerializer serializer = new(typeof(Employee));
                return serializer.ReadObject(stream) as Employee;
            }
        }

        private static string NonSerializeToBinaryString(Employee employee)
        {
            using (var stream = new MemoryStream())
            {
                //var binaryFormatter = new BinaryFormatter();
                //binaryFormatter.Serialize(stream, employee);
                stream.Flush();
                stream.Position = 0;
                return Convert.ToBase64String(stream.ToArray());
            }
        }
    }
    [Serializable]
    public class Employee
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public List<string> Benefits { get; set; } = new List<string>();
    }
}