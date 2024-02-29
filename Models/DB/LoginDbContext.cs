using System.Collections.Generic;



using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exa.Models.DB
{
    public class LoginDbContext : DbContext
    {
        public DbSet<User> Usuarios { get; set; }
       
        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=GRIMALDOLAPTOP\\SQLEXPRESS;Database=Login;Integrated Security=SSPI;TrustServerCertificate=true;");
        }


    }
}