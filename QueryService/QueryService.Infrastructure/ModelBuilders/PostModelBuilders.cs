using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueryService.Domain.PostDomain;

namespace QueryService.Infrastructure.ModelBuilders
{
    internal class PostModelBuilders : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.CreatedAt);
            builder.Property(x => x.RowVersion).IsRowVersion();
            builder
                .OwnsOne(
                    x => x.Content,
                    x => {
                        x.OwnsOne(x => x.ModerationResult);
                    }
                );
        }
    }
}
