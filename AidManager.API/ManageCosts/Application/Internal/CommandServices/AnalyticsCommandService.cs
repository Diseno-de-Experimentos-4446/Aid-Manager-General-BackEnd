using AidManager.API.ManageCosts.Domain.Model.Aggregates;
using AidManager.API.ManageCosts.Domain.Model.Commands;
using AidManager.API.ManageCosts.Domain.Repositories;
using AidManager.API.ManageCosts.Domain.Services;
using AidManager.API.Shared.Domain.Repositories;

namespace AidManager.API.ManageCosts.Application.Internal.CommandServices;

public class AnalyticsCommandService(
    IAnalyticsRepository analyticsRepository,
    IUnitOfWork unitOfWork
    ): IAnalyticsCommandService
{

    public async Task<Analytics?> Handle(CreateAnalyticsCommand command)
    {
        try
        {
            var analytic = new Analytics(command);
            await analyticsRepository.AddAsync(analytic);
            
            await unitOfWork.CompleteAsync();
            
            return analytic;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<Analytics?> Handle(UpdateBarDataPaymentsCommand command)
    {
        try
        {
            
            var analytic = await analyticsRepository.GetAnalyticsById(command.Id);
            
            if (analytic == null)
            {
                return null;
            }
            
            analytic.BarData = command.BarData;
            
            await unitOfWork.CompleteAsync();
            
            return analytic;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<Analytics?> Handle(UpdateAnalyticProgressbarCommand command)
    {
        try
        {
            
            var analytic = await analyticsRepository.GetAnalyticsById(command.Id);
            
            if (analytic == null)
            {
                return null;
            }
            
            analytic.Progressbar = command.Progressbar;
            
            await unitOfWork.CompleteAsync();
            
            return analytic;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<Analytics?> Handle(UpdateAnalyticStatusCommand command)
    {
        try
        {

            var analytic = await analyticsRepository.GetAnalyticsById(command.Id);
            
            if (analytic == null)
            {
                return null;
            }
            
            analytic.Status = command.Status;
            
            await unitOfWork.CompleteAsync();
            
            return analytic;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<Analytics?> Handle(UpdateLinesChartBarCommand command)
    {
        try
        {

            var analytic = await analyticsRepository.GetAnalyticsById(command.Id);
            
            if (analytic == null)
            {
                return null;
            }
            
            analytic.LinesChartBarData = command.Lines;
            
            await unitOfWork.CompleteAsync();
            
            return analytic;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<Analytics?> Handle(UpdateAnalyticTasksCommand command)
    {
        try
        {

            var analytic = await analyticsRepository.GetAnalyticsById(command.Id);
            
            if (analytic == null)
            {
                return null;
            }
            
            analytic.Tasks = command.Tasks;
            
            await unitOfWork.CompleteAsync();
            
            return analytic;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}