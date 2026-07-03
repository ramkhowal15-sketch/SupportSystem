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
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    
    public DbSet<Department> Departments { get; set; }
 
    public DbSet<Notification> Notification { get; set; }
    public DbSet<Otp> Otps { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<SLASetting> Settings { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketAttechment> TicketAttechments { get; set; }
    public DbSet<TicketHistory> TicketHistories { get; set; }
    public DbSet<TicketReply> TicketReplies { get; set; }

}
