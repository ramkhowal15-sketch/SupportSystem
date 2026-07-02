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
    public DbSet<Product> products { get; set; }
    public DbSet<Customer> customers { get; set; }
    public DbSet<Department> departments { get; set; }
    public DbSet<Employee> employees { get; set; }
    public DbSet<Notification> notification { get; set; }
    public DbSet<Otp> otps { get; set; }
    public DbSet<Purchase> purchases { get; set; }
    public DbSet<Role> roles { get; set; }
    public DbSet<SLASetting> settings { get; set; }
    public DbSet<Ticket> tickets { get; set; }
    public DbSet<TicketAttechment> ticketAttechments { get; set; }
    public DbSet<TicketHistory> ticketHistories { get; set; }
    public DbSet<TicketReply> ticketReplies { get; set; }

}
