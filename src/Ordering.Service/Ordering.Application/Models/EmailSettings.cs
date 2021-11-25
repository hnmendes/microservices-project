namespace Ordering.Application.Models
{
    public class EmailSettings
    {
        public string FromName { get; set; }
        public string Domain { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
