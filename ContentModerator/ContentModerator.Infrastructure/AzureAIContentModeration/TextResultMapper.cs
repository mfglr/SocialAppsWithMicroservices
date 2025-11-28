using Azure.AI.ContentSafety;
using Shared.Objects;

namespace ContentModerator.Infrastructure.AzureAIContentModeration
{
    internal class TextResultMapper
    {
        public ModerationResult Map(AnalyzeTextResult result) =>
            new(
                result.CategoriesAnalysis.First(x => x.Category == TextCategory.Hate).Severity ?? 7,
                result.CategoriesAnalysis.First(x => x.Category == TextCategory.SelfHarm).Severity ?? 7,
                result.CategoriesAnalysis.First(x => x.Category == TextCategory.Sexual).Severity ?? 7,
                result.CategoriesAnalysis.First(x => x.Category == TextCategory.Violence).Severity ?? 7
            );
    }
}
