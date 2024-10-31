using AidManager.API.ManageTasks.Domain.Model.Aggregates;
using AidManager.API.ManageTasks.Domain.Model.Queries;

namespace AidManager.API.ManageTasks.Domain.Services;

public interface ITaskQueryService
{
    Task<TaskItem> Handle(GetTaskByIdQuery query); 
    Task<List<TaskItem>> Handle(GetTasksByProjectIdQuery query);
    
    Task<List<List<TaskItem>>> Handle(GetTasksByCompanyId query);
    
}