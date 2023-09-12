namespace AzureDevops.Client.Models;

public record RetryPolicyConfiguration(int RetryCount, int RetryAttemptFactor)
{
    public static readonly RetryPolicyConfiguration Default = new RetryPolicyConfiguration(3, 1);
}