using System.IO;
using GrapeCity.ActiveReports.Web.Designer;
using Microsoft.EntityFrameworkCore;

namespace WebDesigner_Custom.Data;

public class ReportsDbContext : DbContext
{
    public DbSet<Report> Reports { get; set; }

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
    public byte[] Content { get; set; }
    public bool Readonly { get; set; }
}
