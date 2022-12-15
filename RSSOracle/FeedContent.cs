internal class FeedContent
{
    public required string Title { get; init; }

    public required string Link { get; init; }

    public required string? Description { get; init; }

    public required DateTimeOffset? PublishingDate { get; init; }

    public required string Author { get; init; }

    public required string Id { get; init; }

    public required ICollection<string> Categories { get; init; }

    public required string Content { get; init; }
}