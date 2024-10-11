namespace AidManager.API.ManageTasks.Interfaces.REST.Resources;

public record CreateTaskItemResource(string Title, string Description, string DueDate, string State, int AssigneeId);