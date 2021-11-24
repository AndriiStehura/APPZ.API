namespace APPZ.DAL.Entities
{
    public class Identity: IEntity<int>
    {
        public int Id { get; set; }

        public string PasswordHash { get; set; }
    }
}
