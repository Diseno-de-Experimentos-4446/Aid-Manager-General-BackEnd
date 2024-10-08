namespace AidManager.API.Authentication.Domain.Model.Commands;

public record ValidateUserCredentialsCommand(string Email, string Password, string CompanyRegisterCode);