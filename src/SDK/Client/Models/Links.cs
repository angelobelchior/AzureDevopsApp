namespace AzureDevops.Client.Models;

public record Links(
    Link Self, 
    Link Web, 
    Link SourceVersionDisplayUri, 
    Link Timeline, 
    Link Badge, 
    Link Avatar);