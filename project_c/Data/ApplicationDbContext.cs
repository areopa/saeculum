using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using project_c.Models;

namespace project_c.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //dbset voor de games
        public DbSet<Game> Games { get; set; }
        //dbset voor de orders
        public DbSet<Order> Orders { get; set; }
        //dbset voor de relaties tussen games en orders
        public DbSet<GameOrder> GameOrder { get; set; }
        //dbset voor de Favorieten
        public DbSet<Favorieten> Favorieten { get; set; }

        //methode waarmee de database-model gemaakt wordt: ORM, Object Relational Mapper
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Many <> Many relatie tussen Game en Order
            modelBuilder.Entity<GameOrder>()
                .HasOne(ma => ma.Order)
                .WithMany(m => m.Games)
                .HasForeignKey(ma => ma.OrderId);
            modelBuilder.Entity<GameOrder>()
                .HasOne(ma => ma.Game)
                .WithMany(m => m.Orders)
                .HasForeignKey(ma => ma.GameId);

            //One <> Many relatie tussen ApplicationUser en Order
            modelBuilder.Entity<Order>()
                .HasOne(ma => ma.ApplicationUser)
                .WithMany(m => m.Orders)
                .HasForeignKey(ma => ma.UserId);

            //One <> Many relatie tussen ApplciationUser en Order
            modelBuilder.Entity<Favorieten>()
                .HasKey(t => t.UserId);
            modelBuilder.Entity<Favorieten>()
                .HasOne(ma => ma.ApplicationUser)
                .WithOne(m => m.Favorieten)
                .HasForeignKey<Favorieten>(ma => ma.UserId);
        }

        //methode waarmee de database-model gemaakt wordt: ORM, Object Relational Mapper
        public DbSet<project_c.Models.ApplicationUser> ApplicationUser { get; set; }
    }
}
