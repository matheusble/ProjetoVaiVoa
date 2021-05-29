using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaiVoa.Models;

namespace VaiVoa.Data
{
    public class cardcontext : DbContext
    {
        public cardcontext(DbContextOptions<cardcontext> options)
            : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }
    }
}
