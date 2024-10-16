using AidManager.API.ManageCosts.Domain.Model.Entities;

namespace AidManager.API.ManageCosts.Interfaces.REST.Resources;

public record CreateAnalyticsResource(
    int ProjectId,
    int Id,
    List<LineChartData> LinesChartBarData,
    List<BarData> BarData,
    List<double> Progressbar,
    List<double> Status,
    List<double> Tasks
    );