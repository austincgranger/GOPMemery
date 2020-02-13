﻿using GOPMemery.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOPMemery.Models
{
    public class Repo : DbContext, IDataStore<User>
    {
        /// <summary>
        /// Creates our Repository
        /// </summary>
        /// <param name="dbPath">the platform specific path to the database</param>
        public Repo(string dbPath) : base()
        {
            _dbPath = dbPath;

            Database.EnsureCreated();
            Database.Migrate();
        }

        string _dbPath;
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Debug.WriteLine("**** OnConfiguring");
            optionsBuilder.UseSqlite($"Filename={_dbPath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Debug.WriteLine("**** OnModelCreating");
            // Setting Id as PrimaryKey
            modelBuilder.Entity<User>()
                .HasKey(p => p.UserId);

            // Require Text to be set

            //modelBuilder.Entity<Item>()
            //    .Property(p => p.Text)
            //    .IsRequired();

            // As an example in case any of you need to see how

            // Added initialization data
#if DEBUG
            modelBuilder.Entity<User>()
                .HasData(
                    new User { UserId = Guid.NewGuid().ToString(), UserUsername = "First user" },
                    new User { UserId = Guid.NewGuid().ToString(), UserUsername = "Second user" },
                    new User { UserId = Guid.NewGuid().ToString(), UserUsername = "Third user" },
                    new User { UserId = Guid.NewGuid().ToString(), UserUsername = "Fourth user" }
                );
#endif
        }

        #region IDataStore<User> start
        public async Task<User> GetUserAsync(string id) // BY ID
        {
            var user = await Users.FirstOrDefaultAsync(u => u.UserId == id).ConfigureAwait(false);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(bool forceRefresh = false)
        {
            // TODO: forceRefresh
            var allItems = await Users.ToListAsync().ConfigureAwait(false);
            return allItems;
        }

        public async Task<bool> AddUserAsync(User user)
        {

            user.UserId = Guid.NewGuid().ToString(); // Id is of type string but is generated by a GUID
            await Users.AddAsync(user);
            await SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            Users.Update(user);
            await SaveChangesAsync().ConfigureAwait(false);
            // No error handling yet
            return true;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var userRemove = Users.FirstOrDefault(x => x.UserId == id);
            if (userRemove != null)
            {
                Users.Remove(userRemove);
                await SaveChangesAsync().ConfigureAwait(false);
            }

            return true;
        }
        #endregion
    }
}
