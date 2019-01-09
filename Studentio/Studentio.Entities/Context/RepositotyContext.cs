﻿using Microsoft.EntityFrameworkCore;
using Studentio.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Studentio.Entities.Context
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        { }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

    }
}
