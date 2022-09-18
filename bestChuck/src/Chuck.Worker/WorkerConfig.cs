namespace Chuck.Worker;

public record WorkerConfig
{
    public TimeSpan Frequency { get; set; }
    public string QuoteUrl { get; set; }
    public int FetchCount { get; set; }
}