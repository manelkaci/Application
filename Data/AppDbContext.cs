using AGB_Bank.Models;
using AGB_Bank.Models.product;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AGB_Bank.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{

    public DbSet<pack_product> packProduct { get; set; }
    public DbSet<carte_product> carteProduct { get; set; }
    public DbSet<credit_product> creditProduct { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}
