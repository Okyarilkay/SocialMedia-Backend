﻿using Core.Entities;
using DataAccess.Entities;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Context
{
    public class SocialMediaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = localhost\SQLEXPRESS ; Database = SocialMedia ; Trusted_Connection = true ; TrustServerCertificate = true");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims{ get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Topic> Topics { get; set; }
    }
}
