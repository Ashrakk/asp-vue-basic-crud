using System.Text;
using CorsoASP.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;

namespace CorsoASP.Infrastructure.Data;

public class AppDbContext: DbContext
{
    private readonly IConfiguration _config;
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Regions> Regions { get; set; }
    public DbSet<Walks> Walks { get; set; }
    
    public AppDbContext(IConfiguration config)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = _config.GetConnectionString("DefaultConnection");
        options.UseSqlite(connectionString, b => b.MigrationsAssembly("CorsoASP.UI"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        SeedData(builder);
    }

    private void SeedData(ModelBuilder builder)
    {
        var rand = new Random();
        
        List<Regions> reg = new List<Regions>();
        List<Walks> walk = new List<Walks>();
        
        int id = 1;
        string desc = "";
        double leng = 0;
        int regionFK = 0;
        int difficultyFK = 0;
        string code = "";
        string name = "";

        builder.Entity<Difficulty>().HasData(
            new Difficulty { ID = 1, Name = "Bassa", CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow},
            new Difficulty { ID = 2, Name = "Media", CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow  },
            new Difficulty { ID = 3, Name = "Alta", CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow },
            new Difficulty { ID = 4, Name = "Atroce", CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow });

        for (int x = 0; x < 50; x++)
        {
            code = "Code " + Encoding.ASCII.GetString(new byte[]{ (Byte)rand.Next(65, 90) }) + rand.Next(10,90).ToString();
            name = "Region " + rand.Next(10, 90).ToString();
            reg.Add(new Regions{ID = id, Code = code, Name = name, CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow});
            id++;
        }

        id = 1;
        
        for (int x = 0; x < 200; x++)
        {
            desc = "Descrizione: " + rand.Next(0, 100).ToString() + " Randomico";
            leng = rand.Next(5, 15) + rand.NextDouble();
            regionFK = rand.Next(1, 50);
            difficultyFK = rand.Next(1, 3);
            
            walk.Add(new Walks{ID = id, Description = desc, LengthKm = leng, RegionFK = regionFK, DifficultyFK = difficultyFK, CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow});
            
            id++;
        }
        
        builder.Entity<Regions>().HasData(reg);
        builder.Entity<Walks>().HasData(walk);
    }

}