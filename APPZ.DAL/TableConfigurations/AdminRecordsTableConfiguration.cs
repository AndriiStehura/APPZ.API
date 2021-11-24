using APPZ.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APPZ.DAL.TableConfigurations
{
    class AdminRecordsTableConfiguration : IEntityTypeConfiguration<AdminRecord>
    {
        public void Configure(EntityTypeBuilder<AdminRecord> builder)
        {
            builder.ToTable("AdminRecords")
                .HasOne(x => x.User);
        }
    }
}
