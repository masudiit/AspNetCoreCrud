﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagment.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                 new Employee(){Id =1 , Department =Dept.HR, Email ="abc2@gmail.com", Name="sdasd"},
                   new Employee(){Id =2 , Department =Dept.IT, Email ="abc2@gmail.com", Name="sdasd"},
                     new Employee(){Id =3 , Department =Dept.HR, Email ="abc2@gmail.com", Name="sdasd"}
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id ==  Id);
        }

       
    }
}
