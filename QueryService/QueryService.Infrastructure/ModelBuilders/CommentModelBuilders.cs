using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueryService.Domain.CommentDomain;

namespace QueryService.Infrastructure.ModelBuilders
{
    internal class CommentModelBuilders : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .OwnsOne(
                    x => x.Content,
                    x => {
                        x.OwnsOne(x => x.ModerationResult);
                    }
                );
            builder.Property(x => x.RowVerstion).IsRowVersion();
        }
    }
}
