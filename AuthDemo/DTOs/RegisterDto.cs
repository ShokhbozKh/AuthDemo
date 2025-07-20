namespace AuthDemo.DTOs
{
    public class RegisterDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } = null!;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
