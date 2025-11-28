using Azure.AI.ContentSafety;
using Shared.Objects;

namespace ContentModerator.Infrastructure.AzureAIContentModeration
{
    internal class ImageResultMapper
    {
        public ModerationResult Map(AnalyzeImageResult result) =>
            new(
                result.CategoriesAnalysis.First(x => x.Category == ImageCategory.Hate).Severity ?? 7,
                result.CategoriesAnalysis.First(x => x.Category == ImageCategory.SelfHarm).Severity ?? 7,
                result.CategoriesAnalysis.First(x => x.Category == ImageCategory.Sexual).Severity ?? 7,
                result.CategoriesAnalysis.First(x => x.Category == ImageCategory.Violence).Severity ?? 7
            );
    }
}
