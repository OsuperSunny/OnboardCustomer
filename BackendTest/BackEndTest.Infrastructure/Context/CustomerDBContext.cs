using BackEndTest.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Infrastructure.Context
{
    public class CustomerDBContext:IdentityDbContext
    {
        public CustomerDBContext(DbContextOptions<CustomerDBContext> options)
           : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<LGA> LGAs { get; set; }
    }
}
