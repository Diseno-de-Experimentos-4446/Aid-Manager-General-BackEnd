using AidManager.API.ManageCosts.Domain.Model.Commands;
using AidManager.API.ManageCosts.Interfaces.REST.Resources;

namespace AidManager.API.ManageCosts.Interfaces.REST.Transform;

public class UpdateLinesChartBarCommandFromResourceAssembler
{
    public static UpdateLinesChartBarCommand ToCommandFromResource(int projectId, UpdateLinesChartBarResource resource)
    {
        return new UpdateLinesChartBarCommand(
            projectId,
            resource.Lines
        );
    }
}