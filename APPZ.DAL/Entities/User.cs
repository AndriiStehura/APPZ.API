namespace APPZ.DAL.Entities
{
    public class User : IEntity<int>
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Group { get; set; }

        public int IdentityId { get; set; }

        public Identity Identity { get; set; }
    }
}
