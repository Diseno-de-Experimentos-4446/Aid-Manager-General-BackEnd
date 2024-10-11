namespace AidManager.API.ManageTasks.Domain.Model.Commands;

public record CreateTaskCommand(string Title, string Description, string DueDate, int ProjectId, string State, int AssigneeId);