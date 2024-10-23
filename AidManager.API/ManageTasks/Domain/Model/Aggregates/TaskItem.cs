using AidManager.API.ManageTasks.Domain.Model.Commands;

namespace AidManager.API.ManageTasks.Domain.Model.Aggregates;

public class TaskItem 
{
    public int Id { get; private set; }
    
    public string Title { get; private set; }
    
    public string Description { get; private set; }
    
    public DateOnly DueDate { get; private set; }
    
    public int ProjectId { get; private set; }
    
   public string State {get; private set;}
    
   public string Assignee {get; private set;}
   
    public int AssigneeId {get; private set;}
    
    public string profileImg {get; private set;}
    public DateOnly CreatedAt { get; private set; }
   
    protected TaskItem()
    {
        this.Title = string.Empty;
        this.Description = string.Empty;
        this.DueDate = DateOnly.MinValue;
        this.ProjectId = 0;
        this.State = "ToDo";
        this.Assignee = string.Empty;
        this.AssigneeId = 0;
        this.CreatedAt = DateOnly.FromDateTime(DateTime.Now);
    }

    public TaskItem(CreateTaskCommand command, string assignee, string assigneeImage)
    {
        this.Title = command.Title;
        this.Description = command.Description;
        this.DueDate = command.DueDate;
        this.ProjectId = command.ProjectId;
        this.State = command.State;
        this.AssigneeId = command.AssigneeId;
        this.Assignee = assignee;
        this.profileImg = assigneeImage;
        this.CreatedAt= DateOnly.FromDateTime(DateTime.Now);

    }
    
    public void UpdateTask(UpdateTaskCommand command, string assignee, string assigneeImage)
    {
        this.Title = command.Title;
        this.Description = command.Description;
        this.DueDate = command.DueDate;
        this.State = command.State;
        this.Assignee = assignee;
        this.AssigneeId = command.AssigneeId;
        this.profileImg = assigneeImage;
    }
    
    
    
}