using AidManager.API.ManageCosts.Domain.Model.Commands;
using AidManager.API.ManageCosts.Interfaces.REST.Resources;

namespace AidManager.API.ManageCosts.Interfaces.REST.Transform;

public class UpdateLinesChartBarCommandFromResourceAssembler
{
    public static UpdateLinesChartBarCommand ToCommandFromResource(int id, UpdateLinesChartBarResource resource)
    {
        return new UpdateLinesChartBarCommand(
            id,
            resource.Lines
        );
    }
}