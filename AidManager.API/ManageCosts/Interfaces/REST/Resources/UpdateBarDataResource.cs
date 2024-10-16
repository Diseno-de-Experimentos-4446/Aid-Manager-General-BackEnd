using AidManager.API.ManageCosts.Domain.Model.Entities;

namespace AidManager.API.ManageCosts.Interfaces.REST.Resources;

public record UpdateBarDataResource(
    int Id,
    List<BarData> BarData
    );