using ContractManagment.Api.Models;
using ContractManagment.Api.Models.ClientsModels;
using ContractManagment.Api.Models.ContractsModels;
using ContractManagment.Api.Models.ContractsModels.ContractDocumentsModels;
using ContractManagment.Api.Services.ModelInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace ContractManagment.Api.Data;

public partial class ApplicationDbContext
: DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Clients> Clients { get; set; }
    public DbSet<Industry> Industries { get; set; }
    public DbSet<Contracts> Contracts { get; set; }
    public DbSet<ContractDocuments> ContractDocuments { get; set; }
    public DbSet<ContractDocumentType> ContractDocumentTypes { get; set; }
    public DbSet<Categories> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ContractApi");

        modelBuilder.Entity<Contracts>(entity =>
        {
            entity.ToTable(c => c.HasCheckConstraint("CK_Contract_StartDate_EndDate",
                                                     "[StartDate] < [EndDate]"));
            entity.HasIndex(c => c.ContractNumber).IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);

    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    /// <summary>
    /// Save change overrides
    /// </summary>
    /// <returns></returns>
    public override int SaveChanges()
    {
        TimerUpdates();
        SaveAudits();
        return base.SaveChanges();
    }
    // Override SaveChangesAsync method
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        TimerUpdates();
        SaveAudits();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void TimerUpdates()
    {
        var entries = ChangeTracker.Entries<IDateTimeAuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State is EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.Now;
                entry.Entity.LastUpdatedAt = DateTime.Now;
            }
            else if (entry.State is EntityState.Modified)
            {
                entry.Entity.LastUpdatedAt = DateTime.Now;
            }
        }
    }


    private void SaveAudits()
    {
        var modfied = ChangeTracker.Entries()
                           .Where(c => c.Entity is not AuditLog)
                           .Where(c => c.State is EntityState.Added or EntityState.Modified or EntityState.Deleted).ToList();
        Guid groupId = Guid.NewGuid();

        foreach (var item in modfied)
        {
            //            Math.Round(10.002,0,MidpointRounding.ToZero)

            var audit = new AuditLog
            {
                Action = item.State.ToString(),
                GroupKey = groupId,
                ModelId = (int)(item.Property("Id").CurrentValue ?? -1),
                TableName = item.Metadata?.GetTableName() ?? item.GetType().Name,
                Changes = GetChanges(item),
            };
            AuditLogs.Add(audit);
        }
    }
    private static string GetChanges(EntityEntry entry)
    {
        if (entry.State == EntityState.Added)
        {
            Dictionary<string, Dictionary<string, dynamic>> changes = new Dictionary<string, Dictionary<string, dynamic>>();

            foreach (var property in entry.OriginalValues.Properties)
            {
                var newValueToCheck = entry.CurrentValues[property];
                changes.Add(property.Name, new Dictionary<string, dynamic>()
                        {
                              { "new", newValueToCheck }
                         });

            }
            return JsonSerializer.Serialize(changes);
        }
        else if (entry.State == EntityState.Modified)
        {

            Dictionary<string, Dictionary<string, dynamic>> changes = new Dictionary<string, Dictionary<string, dynamic>>();

            foreach (var property in entry.OriginalValues.Properties)
            {
                var original = entry.OriginalValues[property];
                var newValueToCheck = entry.CurrentValues[property];
                if (!Equals(original, newValueToCheck))
                {
                    if (!changes.ContainsKey(property.Name))
                        changes.Add(property.Name, new Dictionary<string, dynamic>()
                        {
                              { "old", original },
                              { "new", newValueToCheck }

                         });
                }
            }
            return JsonSerializer.Serialize(changes);
        }
        else if (entry.State == EntityState.Deleted)
        {
            Dictionary<string, Dictionary<string, dynamic>> changes = new Dictionary<string, Dictionary<string, dynamic>>();


            foreach (var property in entry.OriginalValues.Properties)
            {
                var oldValue = entry.CurrentValues[property];
                changes.Add(property.Name, new Dictionary<string, dynamic>()
                        {
                              { "old", oldValue },
                         });
            }
            return JsonSerializer.Serialize(changes);

        }
        else
        {
            throw new NotImplementedException("An Entity which is not modified or added or deleted should not be in the audit function in the first place");
        }
    }

}