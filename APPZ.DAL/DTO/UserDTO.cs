namespace APPZ.DAL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Group { get; set; }

        public bool IsAdmin { get; set; }
    }
}
