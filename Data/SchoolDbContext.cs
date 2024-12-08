using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models;
using System.Dynamic;

namespace SchoolAPI.Models.Data
{

    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  modelBuilder.Entity<OpCoClientInfo>().HasKey(u => new 
            // { 
            //     u.Id, 
            //     u.OfficialCompanyName 
            // });
        }

        public DbSet<Attendence> Attendence { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Districts> Districts { get; set; }
        public DbSet<Fee> Fee { get; set; }
        public DbSet<Genders> Genders { get; set; }
        public DbSet<Mandals> Mandals { get; set; }
        public DbSet<Privileges> Privileges { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Students_Status> Students_Status { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Teachers_Status> Teachers_Status { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Users_Types> Users_Types { get; set; }
        public DbSet<UserType_Privileges> UserType_Privileges { get; set; }
        public DbSet<Villages> Villages { get; set; }


        public override int SaveChanges()
        {
            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E =>
            {
                // E.Property("CreatedDate").CurrentValue = DateTime.Now;
                // E.Property("CreatedBy").CurrentValue = 1;
            });

            var EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

            EditedEntities.ForEach(E =>
            {
                // E.Property("UpdatedDate").CurrentValue = DateTime.Now;
                // E.Property("UpdatedBy").CurrentValue = 1;
            });

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();
            try
            {
                AddedEntities.ForEach(E =>
                {
                    E.Property("CreatedDate").CurrentValue = DateTime.Now;
                    E.Property("CreatedBy").CurrentValue = 1;
                    // E.Property("Guid").CurrentValue = Guid.NewGuid().ToString();
                });

                var EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

                EditedEntities.ForEach(E =>
                {
                    E.Property("UpdatedDate").CurrentValue = DateTime.Now;
                    E.Property("UpdatedBy").CurrentValue = 1;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

    }
}
