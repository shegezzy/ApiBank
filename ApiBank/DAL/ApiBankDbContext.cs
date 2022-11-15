using ApiBank.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBank.DAL
{
    public class ApiBankDbContext : DbContext
    {
        public ApiBankDbContext(DbContextOptions<ApiBankDbContext> options) : base(options)
        {

        }

        //dbset
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
