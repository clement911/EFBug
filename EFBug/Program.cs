using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using (var ctx = new MyDbContext())
{
    ctx.Add(new MyEntity() { StartedAt = new DateTime(2000, 1, 1, 1, 1, 1, millisecond: 0) });
    await ctx.SaveChangesAsync();//WORKS

    ctx.Add(new MyEntity() { StartedAt = new DateTime(2000, 1, 1, 1, 1, 1, millisecond: 1) });
    await ctx.SaveChangesAsync();//FAILS: The database operation was expected to affect 1 row(s), but actually affected 0 row(s); 
}

public class MyEntity
{
    [Column(TypeName = "datetime2(0)")]
    public DateTime StartedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
}

public class MyDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<MyEntity>().HasKey(i => new { i.StartedAt, i.Id });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=(local)\\sql2019;Database=EFBugRepro;Integrated Security=True;");
    }
}

