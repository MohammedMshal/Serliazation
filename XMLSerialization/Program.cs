
using System.Xml;
using System.Xml.Serialization;

namespace XMLSerialization01
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee e1 = new()
            {
                Id = 1001,
                Fname = "Issam",
                Lname = "A.",
                Benefits = { "Pension", "Health Insurance" }
            };

            string xmlContent = SerializationToXml(e1);
            File.WriteAllText("XmlData.xml", contents:  xmlContent);
            //Console.WriteLine(xmlContent);

            Employee e2 = DeserializedToObject(xmlContent);
            Console.WriteLine(e2.Fname);

            Console.ReadKey();
        }

        private static Employee DeserializedToObject(string xmlContent)
        {
            Employee? e;
            XmlSerializer serializer = new(typeof(Employee));
            using(TextReader  reader = new StringReader(xmlContent))
            {
                e = serializer.Deserialize(reader) as Employee;
            }
            return e!;
        }

        private static string SerializationToXml(Employee e1)
        {
            string reslut;
            XmlSerializer serializer = new(typeof(Employee)); // or e.GetType()
            using(StringWriter sw = new())
            {
                using(XmlWriter w = XmlWriter.Create(sw, new XmlWriterSettings { Indent= true}))
                {
                    serializer.Serialize(w, e1);
                    reslut = sw.ToString()!;
                }
            }
            return reslut;
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public List<string> Benefits { get; set; } = new List<string>();
    }
}