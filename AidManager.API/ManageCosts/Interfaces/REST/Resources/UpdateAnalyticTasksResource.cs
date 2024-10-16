namespace AidManager.API.ManageCosts.Interfaces.REST.Resources;

public record UpdateAnalyticTasksResource(
    int Id,
    List<double> Tasks
    );