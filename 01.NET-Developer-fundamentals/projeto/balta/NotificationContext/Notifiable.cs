namespace balta.NotificationCOntext;

public abstract class Notifiable 
{
    public List<Notification> Notifications { get; set; }

    protected Notifiable()
    {
        Notifications = new List<Notification>();
    }

    public void AddNotification(Notification notification)
    {
        Notifications.Add(notification);
    }
    public void AddNotifications(IEnumerable<Notification> notification)
    {
        Notifications.AddRange(notification);
    }
    public bool IsInValid => !Notifications.Any();

}

