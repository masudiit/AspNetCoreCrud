using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagment.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name="Mary",
                    Department=Dept.IT,
                    Email ="abc@gmail.com"
                },
                new Employee
                {
                    Id = 2,
                    Name = "Cristmas",
                    Department = Dept.IT,
                    Email = "abc@gmail.com"
                }
                );

        }
    }
}
