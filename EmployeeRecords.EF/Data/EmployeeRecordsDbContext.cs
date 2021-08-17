using EmployeeRecords.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRecords.EF.Data
{
    public class EmployeeRecordsDbContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }

        public EmployeeRecordsDbContext(DbContextOptions<EmployeeRecordsDbContext> options) : base(options)
        {}
    }
}
