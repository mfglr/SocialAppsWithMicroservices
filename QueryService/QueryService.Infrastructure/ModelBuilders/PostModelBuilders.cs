using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueryService.Domain.PostDomain;

namespace QueryService.Infrastructure.ModelBuilders
{
    internal class PostModelBuilders : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder
                .OwnsOne(
                    x => x.Content,
                    x => {
                        x.OwnsOne(x => x.ModerationResult);
                    }
                );

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
