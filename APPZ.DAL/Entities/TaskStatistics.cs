namespace APPZ.DAL.Entities
{
    public class TaskStatistics: IEntity<int>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int TaskId { get; set; }
        public LabTask Task { get; set; }
        public double Grade { get; set; }
    }
}
