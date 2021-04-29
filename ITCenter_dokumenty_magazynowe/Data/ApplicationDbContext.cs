using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ITCenter_dokumenty_magazynowe.Models.DbModels;

namespace ITCenter_dokumenty_magazynowe.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<OperationLog> OperationLogs { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<WarehouseDoc> WarehouseDocs { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
