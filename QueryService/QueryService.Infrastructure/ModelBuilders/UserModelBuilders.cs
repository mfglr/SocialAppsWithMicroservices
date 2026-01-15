using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueryService.Domain.UserDomain;

namespace QueryService.Infrastructure.ModelBuilders
{
    internal class UserModelBuilders : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .OwnsMany(
                    x => x.Media,
                    x => {
                        x.OwnsMany(x => x.Thumbnails);
                        x.OwnsOne(x => x.Metadata);
                        x.OwnsOne(x => x.ModerationResult);
                        x.ToJson();
                    }
                );

            builder.Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
