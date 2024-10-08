namespace AidManager.API.IAM.Domain.Model.Commands;

public record UpdateUserCommand(string Username, string Password, int UserRole);
