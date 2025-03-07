using Friendly_.Models;
using Microsoft.EntityFrameworkCore;

namespace Friendly_.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RecintoDesportivo> Recintos { get; set; }
        internal DbSet<User> User { get; set; }
        internal DbSet<Reserva> Reserva { get; set; }
        internal DbSet<Pagamento> Pagamento { get; set; }
        public DbSet<MetodoPagamento> MetodoPagamento { get; set; }
        public DbSet<UserContacts> UserContacts { get; set; }

    }
}
