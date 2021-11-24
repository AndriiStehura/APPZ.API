namespace APPZ.DAL.Entities
{
    public class AdminRecord : IEntity<int>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
