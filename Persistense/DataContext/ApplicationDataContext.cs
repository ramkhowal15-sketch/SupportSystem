using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistense.DataContext;

public class ApplicationDataContext : DbContext
{
    public ApplicationDataContext(DbContextOptions options) : base(options)
    {  }
    public DbSet<User> users { get; set; }
    public DbSet<Manager> manager { get; set; }
    public DbSet<Product> products { get; set; }
}
