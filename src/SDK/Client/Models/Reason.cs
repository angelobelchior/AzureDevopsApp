namespace AzureDevops.Client.Models;

public enum Reason
{
    All,
    BatchedCI,
    BuildCompletion,
    CheckInShelveset,
    IndividualCI,
    Manual,
    None,
    PullRequest,
    Schedule,
    ScheduleForced,
    Triggered,
    UserCreated,
    ValidateShelveset
}