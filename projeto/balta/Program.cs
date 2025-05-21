using balta.ContentContext;
using balta.ContentContext.Enums;

var articles = new List<Article>();
articles.Add(new Article("Artigo sobre OOP", "orientacao-objetos"));
articles.Add(new Article("Artigo sobre C#", "csharp"));
articles.Add(new Article("Artigo sobre .NET", "dotnet"));

//foreach (var article in articles)
//{
//    Console.WriteLine(article.Id);
//    Console.WriteLine(article.Title);
//    Console.WriteLine(article.Url);
//}

var courses = new List<Course>();
var courseOOP = new Course("Artigo sobre OOP", "orientacao-objetos");
var couseCsharp = new Course("Artigo sobre C#", "csharp");
var couseAspNet = new Course("Artigo sobre ASP.NET", "dotnet");
courses.Add(courseOOP);
courses.Add(couseCsharp);
courses.Add(couseAspNet);

var careers = new List<Career>();
var careerDotnet = new Career("Especialista .NET", "especialista-dotnet");
var careerItem2 = new CareerItem(2, "Comece por OOP", "", courseOOP);
var careerItem = new CareerItem(1, "Comece por aqui", "", couseCsharp);
var careerItem3 = new CareerItem(3, "Comece por .NET", "", couseAspNet);
careerDotnet.Items.Add(careerItem2);
careerDotnet.Items.Add(careerItem);
careerDotnet.Items.Add(careerItem3);
careers.Add(careerDotnet);

foreach (var career in careers)
{
    Console.WriteLine(career.Title);
    foreach (var item in career.Items.OrderBy(x => x.Ordem))
    {
        Console.WriteLine($"{item.Ordem} - {item.Title}");
        Console.WriteLine(item.Course.Title);
        Console.WriteLine(item.Course.Level);
    }
}
