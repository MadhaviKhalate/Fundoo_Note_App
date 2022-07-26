﻿using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<NotesEntity> NotesEntities { get; set; }
        public DbSet<CollaboratorEntity> CollaboratorEntities { get; set; }
        public DbSet<LabelEntity> LabelEntities { get; set; }

    }
}
