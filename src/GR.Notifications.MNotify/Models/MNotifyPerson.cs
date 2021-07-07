namespace GR.Notifications.MNotify.Models
{
    public class MNotifyPerson
    {
        public MNotifyPerson()
        {

        }

        public MNotifyPerson(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}