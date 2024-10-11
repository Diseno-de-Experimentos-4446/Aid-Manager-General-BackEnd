namespace AidManager.API.ManageTasks.Interfaces.REST.Resources;

public record UpdateTaskItemResource(int Id, string Title, string Description, string DueDate, string State, int UserId);