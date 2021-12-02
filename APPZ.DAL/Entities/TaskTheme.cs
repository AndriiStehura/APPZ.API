using System.Collections.Generic;

namespace APPZ.DAL.Entities
{
    public class TaskTheme : IEntity<int>
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
}
