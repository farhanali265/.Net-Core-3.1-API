using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class AppLogMap : IEntityTypeConfiguration<AppLog> {
        public void Configure(EntityTypeBuilder<AppLog> builder) {

            builder.ToTable("AppLog", "dbo");
            builder.HasKey(c => c.ID);

            // Column Mappings
            builder.Property(c => c.ID).HasColumnName("LogId");
            builder.Property(c => c.Date).HasColumnName("Date");
            builder.Property(c => c.Logger).HasColumnName("Logger");
            builder.Property(c => c.Message).HasColumnName("Message");
            builder.Property(c => c.ViewName).HasColumnName("ViewName");
            builder.Property(c => c.SearchedZipCode).HasColumnName("SearchedZipCode");
            builder.Property(c => c.SessionId).HasColumnName("SessionId");
            builder.Property(c => c.SessionData).HasColumnName("SessionData");
            builder.Property(c => c.SessionUserName).HasColumnName("SessionUserName");
            builder.Property(c => c.UserId).HasColumnName("UserId");
            builder.Property(c => c.DrxApiResponse).HasColumnName("DrxAPiResponse");
            builder.Property(c => c.DrxApiRequest).HasColumnName("DrxAPIRequest");
            builder.Property(c => c.DrxSessionId).HasColumnName("DrxSessionId");

            // Relationships
        }
    }
}