using AidManager.API.ManageTasks.Application.Internal.OutboundServices.ACL;
using AidManager.API.ManageTasks.Domain.Model.Aggregates;
using AidManager.API.ManageTasks.Domain.Model.Queries;
using AidManager.API.ManageTasks.Domain.Repositories;
using AidManager.API.ManageTasks.Domain.Services;

namespace AidManager.API.ManageTasks.Application.Internal.QueryServices;

public class TaskQueryService(ITaskRepository taskRepository, ExternalUserService external) : ITaskQueryService
{
        
    public async Task<TaskItem?> Handle(GetTaskByIdQuery query)
    {
        return await taskRepository.GetTaskById(query.Id);
    }
    
    public async Task<List<TaskItem>> Handle(GetTasksByProjectIdQuery query)
    {
        return await taskRepository.GetTasksByProjectId(query.ProjectId);
    }

    public async Task<List<TaskItem>> Handle(GetTasksByCompanyId query)
    {
        try
        {
            var projects = await external.GetProjectsByCompany(query.CompanyId);

            var tasksList = new List<TaskItem>();
                
            foreach (var project in projects)
            {
                var tasks = await taskRepository.GetTasksByProjectId(project.Id);
                tasksList.AddRange(tasks);
            }

            return tasksList;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}