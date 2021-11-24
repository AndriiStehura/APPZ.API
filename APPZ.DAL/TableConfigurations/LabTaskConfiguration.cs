using APPZ.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APPZ.DAL.TableConfigurations
{
    class LabTaskConfiguration : IEntityTypeConfiguration<LabTask>
    {
        public void Configure(EntityTypeBuilder<LabTask> builder)
        {
            builder.ToTable("Tasks")
                .HasOne(x => x.Theme);
        }
    }
}
