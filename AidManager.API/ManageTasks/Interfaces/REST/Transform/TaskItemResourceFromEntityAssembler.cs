using AidManager.API.ManageTasks.Domain.Model.Aggregates;
using AidManager.API.ManageTasks.Interfaces.REST.Resources;

namespace AidManager.API.ManageTasks.Interfaces.REST.Transform;

public static class TaskItemResourceFromEntityAssembler
{
    public static TaskItemResource ToResourceFromEntity(TaskItem entity) =>
        new TaskItemResource(entity.Id, entity.Title, entity.Description, entity.DueDate.ToString(), entity.ProjectId, entity.State,
            entity.Assignee);
}