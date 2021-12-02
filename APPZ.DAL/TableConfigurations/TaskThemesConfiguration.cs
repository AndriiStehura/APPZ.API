using APPZ.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APPZ.DAL.TableConfigurations
{
    class TaskThemesConfiguration : IEntityTypeConfiguration<TaskTheme>
    {
        public void Configure(EntityTypeBuilder<TaskTheme> builder)
        {
            builder.ToTable("Themes");
        }
    }
}
