namespace UniGuide.Models
{
    public class Admin
    {
        public int AdminID { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
