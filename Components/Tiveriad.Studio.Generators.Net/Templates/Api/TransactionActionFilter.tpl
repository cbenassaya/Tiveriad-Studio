using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Tiveriad.Repositories;
using Linkypharm.Services.Core.Entities;
using Linkypharm.Services.Core.Services;

namespace {{model.itemnamespace}};

public class TransactionActionFilter : IAsyncActionFilter
{
    private readonly DbContext _context;
    private readonly IUserManagerService _userManagerService;
    private readonly ILoggerService<TransactionActionFilter> _loggerService;

    public TransactionActionFilter(DbContext context, IUserManagerService userManagerService, ILoggerService<TransactionActionFilter> loggerService)
    {
        _context = context;
        _userManagerService = userManagerService;
        _loggerService = loggerService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //await using var tx = await _context.Database.BeginTransactionAsync();
        var result = await next();
        if (result.Exception == null || result.ExceptionHandled)
        {
            foreach (var entry in _context.ChangeTracker.Entries<IAuditable<string>>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _userManagerService.GetUserId();
                        entry.Entity.Created = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _userManagerService.GetUserId();
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }

            var organizationId = await _userManagerService.GetCurrentOrganizationIdAsync();
            foreach (var entry in _context.ChangeTracker.Entries<IWithOrganization>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.OrganizationId = organizationId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.OrganizationId = organizationId;
                        break;
                }

            await _context.SaveChangesAsync();
            //await tx.CommitAsync();
        }
    }
}