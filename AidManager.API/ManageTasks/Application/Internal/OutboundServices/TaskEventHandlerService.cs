using AidManager.API.ManageTasks.Application.Internal.OutboundServices.ACL;
using AidManager.API.ManageTasks.Domain.Model.Commands;
using AidManager.API.ManageTasks.Domain.Repositories;
using AidManager.API.Shared.Domain.Repositories;

namespace AidManager.API.ManageTasks.Application.Internal.OutboundServices;

public class TaskEventHandlerService(IProjectRepository projectRepository, ExternalUserService externalUserService, IUnitOfWork unitOfWork) : ITaskEventHandlerService
{
    public async Task HandleAddTeamMember(AddTeamMemberCommand command)
    {
        var project = await projectRepository.GetProjectById(command.ProjectId);
        var newUser = await externalUserService.GetUserById(command.UserId);

        if (project == null)
        {
            throw new Exception("Project not Found");
        }

        if (project.TeamMembers.All(tm => tm.Id != newUser.Id))
        {
            project.AddTeamMember(newUser);
            await projectRepository.Update(project);
        }

        await unitOfWork.CompleteAsync();
    }
}