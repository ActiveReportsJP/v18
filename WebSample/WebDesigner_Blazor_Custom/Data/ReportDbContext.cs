using GrapeCity.ActiveReports.Rendering.Tools;
using GrapeCity.ActiveReports.Web.Designer;
using GrapeCity.ActiveReports.Web.Viewer;
using GrapeCity.Enterprise.Data.Expressions.Tools;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebDesigner_Blazor_Custom.Data;

internal class ReportsDbContext : DbContext
{
    public required DbSet<Report> Reports { get; set; }

    private string _dbPath { get; }
    
    public ReportsDbContext()
    {
        _dbPath = Path.Combine(Directory.GetCurrentDirectory(), "resources", "Storage.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={_dbPath}");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Report>()
            .HasKey(e => e.Id);
             
        modelBuilder.Entity<Report>()
            .Property(e => e.Id)
            .ValueGeneratedNever();
        
        base.OnModelCreating(modelBuilder);
    }
}

public class Report : ReportInfo
{
    public required byte[] Content { get; set; }
    public bool Readonly { get; set; }
}
