using System;
using Balta.ContentContext;

namespace Balta
{
    class Program
    {
        static void Main(string[] args)
        {
            var articles = new List<Article>();
            articles.Add(new Article("Artigo sobre OOP", "orietacao-objetos"));
            articles.Add(new Article("Artigo sobre C#", "csharp"));
            articles.Add(new Article("Artigo sobre .NET", "dotnet"));
            foreach (var article in articles)
            {
                Console.WriteLine(article.Id);
                Console.WriteLine(article.Title);
                Console.WriteLine(article.Url);
            }

            var courses = new List<Course>();
            var courseOOP = new Course("Fundamentos sobre OOP", "fundamentos-orietacao-objetos");
            var courseCsharp = new Course("Fundamentos sobre C#", "fundamentos-csharp");
            var courseAspNet = new Course("Fundamentos sobre .NET", "fundamentos-dotnet");
            courses.Add(courseOOP);
            courses.Add(courseCsharp);
            courses.Add(courseAspNet);

            var careers = new List<Career>();
            var careerDotnet = new Career("Especialista .NET", "especialista-dotnet");
            var careerItem = new CareerItem(1, "Comece por aqui", "", courseOOP);
            careerDotnet.Items.Add(careerItem);
            careers.Add(careerDotnet);

            foreach (var career in careers)
            {
                Console.WriteLine(career.Title);
                foreach (var item in careerDotnet.Items)
                {
                    Console.WriteLine($"{item.Order} - {item.Title}");
                }
            }
        }
    }
}
