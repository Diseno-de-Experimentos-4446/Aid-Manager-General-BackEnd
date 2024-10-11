namespace AidManager.API.ManageTasks.Interfaces.REST.Resources;

public record TaskItemResource(int Id, string Title, string Description, string DueDate, int ProjectId, string State, string UserId);