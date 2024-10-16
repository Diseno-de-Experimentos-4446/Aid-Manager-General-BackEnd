namespace AidManager.API.ManageCosts.Interfaces.REST.Resources;

public record UpdateAnalyticProgressbarResource(
    int Id,
    List<double> Progressbar
    );