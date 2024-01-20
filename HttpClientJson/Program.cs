using System.Text.Json;

namespace HttpClientJosn
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (HttpClient client = new ())
            {
                string todoJsonContent = await client.GetStringAsync("https://jsonplaceholder.typicode.com/todos");
                List<Todo>? todosList = JsonSerializer.Deserialize<List<Todo>>(todoJsonContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                foreach (Todo item in todosList!)
                {
                   Console.WriteLine(item);
                }
                
            }
            Console.ReadKey();
        }

    }

    // يستحسن عدم التعديل على الخصائص لكن لجمال الكود تم التعديل علي او حرف وجعله كبير وفى المقابل تم إضافة الخاصية
    // , new JsonSerializerOptions { PropertyNameCaseInsensitive=true} للتجاوز ذلك
    public class Todo
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
        public override string ToString()
        {
            return $"\n [{Id} - UserId: {UserId}] -  {Title} {(Completed ? "completed" : "not completed")}";
        }
    }

}