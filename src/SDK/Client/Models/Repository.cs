namespace AzureDevops.Client.Models;

public record Repository(
    string Id, 
    string Type, 
    string Name, 
    string Url, 
    object Clean, 
    bool CheckoutSubmodules);