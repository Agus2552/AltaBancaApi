using AltaBancaApi.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaBancaApi.Data
{
    public class AltaBancaContext : IdentityDbContext
    {
        public AltaBancaContext(DbContextOptions<AltaBancaContext> options) : base(options){}
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
