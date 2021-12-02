using APPZ.DAL.Enums;

namespace APPZ.DAL.Entities
{
    public class LabTask : IEntity<int>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Answer { get; set; }

        public Complexity ComplexityLevel { get; set; }

        public int ThemeId { get; set; }

        public TaskTheme Theme { get; set; }
    }
}