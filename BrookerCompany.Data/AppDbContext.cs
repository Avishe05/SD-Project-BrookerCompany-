
namespace BrookerCompany.Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using BrookerCompany.Models;

    public class AppDbContext :DbContext
    {
        private const string connectionString = @"Server=DESKTOP-OP0O6I6; Initial Catalog=BrookerCompany; Integrated Security=true; Trusted_Connection=true";
        public virtual DbSet<Address> Addresses { get; set; }
    
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Brooker> Brookers { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectClient> ProjectClients { get; set; }
        public virtual DbSet<ProjectBrooker> ProjectBrookers { get; set; }
        public virtual DbSet<Town> Towns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProjectClient>(option =>
            {
                option.HasKey(x => new { x.ProjectId, x.ClientId });
            });
            modelBuilder.Entity<ProjectBrooker>(option =>
            {
                option.HasKey(x => new { x.ProjectId, x.BrookerId });
            });

            modelBuilder.Entity<Project>(option =>
            {
                option.HasIndex(x => x.Name).IsUnique(true);
            });
            modelBuilder.Entity<Client>(option =>
            {
                option.HasIndex(x => x.PhoneNumber).IsUnique(true);
            });
            modelBuilder.Entity<Brooker>(option =>
            {
                option.HasIndex(x => x.PhoneNumber).IsUnique(true);
            });
            modelBuilder.Entity<Department>(option =>
            {
                option.HasIndex(x => x.Name).IsUnique(true);
            });
            modelBuilder.Entity<Project>()
               .HasMany(pc => pc.ProjectClients)
               .WithOne(p => p.Project)
               .HasForeignKey(pc => pc.ProjectId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Brooker>()
                .HasMany(pb => pb.ProjectBrookers)
                .WithOne(b => b.Brooker)
                .HasForeignKey(pb => pb.BrookerId)
                .OnDelete(DeleteBehavior.Restrict);




        }
    }
}
