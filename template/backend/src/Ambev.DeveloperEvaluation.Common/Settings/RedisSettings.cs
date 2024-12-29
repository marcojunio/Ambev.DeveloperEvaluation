namespace Ambev.DeveloperEvaluation.Common.Settings;

public class RedisSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public string InstanceName { get; set; } = string.Empty;
    public int DefaultCacheDurationMinutes { get; set; }
    public int DefaultCacheDurationHours { get; set; }
}