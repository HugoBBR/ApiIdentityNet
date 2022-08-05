namespace ApiChida.Models
{
    public class OutputUserModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string ?PasswordHash { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public bool IsEnabled { get; set; }

    }
}
