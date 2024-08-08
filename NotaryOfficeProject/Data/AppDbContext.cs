using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotaryOfficeProject.Models;

namespace NotaryOfficeProject.Data;

public partial class AppDbContext : IdentityDbContext<Visitor>
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<Vehical> Vehicals { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    
}
