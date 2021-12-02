using APPZ.DAL.Entities;
using System.Collections.Generic;

namespace APPZ.DAL.DTO
{
    public class ThemeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<LabTask> Tasks { get; set; }
    }
}
