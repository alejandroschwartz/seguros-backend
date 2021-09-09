using Microsoft.EntityFrameworkCore;
using SegurosApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SegurosApi.Context
{
    public class AutosContext: DbContext
    {
        public AutosContext(DbContextOptions<AutosContext> options): base(options)
        {

        }

        public DbSet<Autos> Autos { get; set; }
    }
}