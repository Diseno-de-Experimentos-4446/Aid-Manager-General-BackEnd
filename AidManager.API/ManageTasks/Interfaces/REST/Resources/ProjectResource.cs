namespace AidManager.API.ManageTasks.Interfaces.REST.Resources;

public record ProjectResource(int Id, string Name, string Description, List<string> ImageUrl, int CompanyId);