namespace AidManager.API.ManageTasks.Domain.Model.Commands;

public record UpdateTaskCommand(int Id, string Title, string Description, string DueDate, string State, int AssigneeId);

