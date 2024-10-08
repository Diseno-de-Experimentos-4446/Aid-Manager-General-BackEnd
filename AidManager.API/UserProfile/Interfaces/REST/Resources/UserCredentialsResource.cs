namespace AidManager.API.Authentication.Interfaces.REST.Resources;

public record UserCredentialsResource(string Email, string Password, string CompanyRegisterCode);