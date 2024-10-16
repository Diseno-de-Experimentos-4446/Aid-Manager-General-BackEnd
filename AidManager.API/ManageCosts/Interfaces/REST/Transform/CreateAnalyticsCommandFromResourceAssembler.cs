using AidManager.API.ManageCosts.Domain.Model.Commands;
using AidManager.API.ManageCosts.Interfaces.REST.Resources;

namespace AidManager.API.ManageCosts.Interfaces.REST.Transform;

public class CreateAnalyticsCommandFromResourceAssembler
{
    public static CreateAnalyticsCommand ToCommandFromResource(CreateAnalyticsResource resource)
    {
        return new CreateAnalyticsCommand(
            resource.ProjectId,
            resource.LinesChartBarData,
            resource.BarData,
            resource.Progressbar,
            resource.Status,
            resource.Tasks
        );
    }
}