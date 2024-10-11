using AidManager.API.ManageTasks.Domain.Model.Aggregates;
using AidManager.API.ManageTasks.Interfaces.REST.Resources;

namespace AidManager.API.ManageTasks.Interfaces.REST.Transform;

public static class ProjectResourceFromEntityAssembler
{
    public static ProjectResource ToResourceFromEntity(Project entity) =>
        new ProjectResource(entity.Id,entity.Name, entity.Description,entity.ImageUrl?.Select(img => img.Url).ToList() ?? new List<string>(), entity.CompanyId);
}