using AidManager.API.ManageCosts.Domain.Model.Commands;
using AidManager.API.ManageCosts.Interfaces.REST.Resources;

namespace AidManager.API.ManageCosts.Interfaces.REST.Transform;

public class UpdateBarDataCommandFromResourceAssembler
{
    public static UpdateBarDataPaymentsCommand ToCommandFromResource(int projectId, UpdateBarDataResource resource)
    {
        return new UpdateBarDataPaymentsCommand(
            projectId,
            resource.BarData
        );
    }
}