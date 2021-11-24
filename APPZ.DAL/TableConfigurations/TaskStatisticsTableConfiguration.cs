using APPZ.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace APPZ.DAL.TableConfigurations
{
    class TaskStatisticsTableConfiguration : IEntityTypeConfiguration<TaskStatistics>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TaskStatistics> builder)
        {
            builder.ToTable("Statistics")
                .HasOne(x => x.User);
            builder.HasOne(x => x.Task);
        }
    }
}
