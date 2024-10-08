namespace AidManager.API.Authentication.Domain.Model.Commands;

public record EditCompanyIdCommand(string BrandName, string IdentificationCode, string Country, string Email, int UserId);