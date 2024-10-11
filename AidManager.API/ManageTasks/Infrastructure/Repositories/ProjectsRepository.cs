using AidManager.API.ManageTasks.Domain.Model.Aggregates;
using AidManager.API.ManageTasks.Domain.Repositories;
using AidManager.API.Shared.Infraestructure.Persistence.EFC.Configuration;
using AidManager.API.Shared.Infraestructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AidManager.API.ManageTasks.Infrastructure.Repositories;

public class ProjectsRepository(AppDBContext context) : BaseRepository<Project>(context), IProjectRepository
{
    
    public async Task<bool> ExistsProject(int projectId)
    {
        return await Context.Set<Project>().AnyAsync(f => f.Id == projectId);
    }
    
    public async Task<bool> ExistsByName(string name)
    {
        return await Context.Set<Project>().AnyAsync(f => f.Name == name);
    }

    public async Task<Project> GetProjectById(int projectId)
    {
        return await Context.Set<Project>()
            .Include(p => p.ImageUrl)
            .FirstOrDefaultAsync(p=> p.Id == projectId);
    }

    public async Task<List<Project>> GetProjectsByCompanyId(int companyId)
    {
        return await Context.Set<Project>()
            .Include(p => p.ImageUrl) // Ensure eager loading of ImageUrl
            .Where(f => f.CompanyId == companyId).ToListAsync();

    }
    
    
}