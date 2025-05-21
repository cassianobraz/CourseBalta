namespace balta.ContentContext;

public class CareerItem : Base
{
    public IList<string> Notifications { get; set; }
    public CareerItem(int ordem, string title, string description, Course course)
    {
        if (course is null)
            throw new Exception("Course cannot be null");

        Ordem = ordem;
        Title = title;
        Description = description;
        Course = course;
    }

    public int Ordem { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Course Course { get; set; }
}