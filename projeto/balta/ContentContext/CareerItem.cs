using balta.NotificationCOntext;
using balta.SharedContext;

namespace balta.ContentContext;

public class CareerItem : Base
{
    public IList<string> Notifications { get; set; }
    public CareerItem(int ordem, string title, string description, Course course)
    {
        if (course == null)
            AddNotification(new Notification("Course", "Curso inválido."));

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