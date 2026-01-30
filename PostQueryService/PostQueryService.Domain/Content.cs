namespace PostQueryService.Domain
{
    public record Content(
        string Value,
        ModerationResult? ModerationResult
    );
}
