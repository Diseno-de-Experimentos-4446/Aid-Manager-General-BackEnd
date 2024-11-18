using AidManager.API.ManageTasks.Domain.Model.Commands;

namespace AidManager.API.ManageTasks.Application.Internal.OutboundServices;

public interface ITaskEventHandlerService
{
    Task HandleAddTeamMember(AddTeamMemberCommand command);

}