// <copyright file="EmployeeDataProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WorkHoursApp.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WorkHoursApp.Model;

    /// <summary>
    /// Class for EmployeeDataProvider, contains database operations.
    /// </summary>
    public class EmployeeDataProvider : IEmployeeDataProvider
    {
        /// <inheritdoc/>
        public async Task<IEnumerable<EmployeeDO>> GetAllAsync()
        {
            using (HoursDBEntities db = new HoursDBEntities())
            {
                List<Employee> employee = (from emp in db.Employees select emp).ToList();
                return employee.Select(e => new EmployeeDO() { Id = e.Id, Name = e.Name });
            }
        }

        /// <inheritdoc/>
        public int Add(EmployeeDO employee)
        {
            using (HoursDBEntities db = new HoursDBEntities())
            {
                var newEmployee = new Employee() { Name = employee.Name };
                db.Employees.Add(newEmployee);
                db.SaveChanges();
                return newEmployee.Id;
            }
        }

        /// <inheritdoc/>
        public void Remove(EmployeeDO employee)
        {
            using (HoursDBEntities db = new HoursDBEntities())
            {
                var removeEmployee = db.Employees.First(x => x.Id == employee.Id);
                db.Employees.Remove(removeEmployee);
                db.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public void Update(EmployeeDO employee)
        {
            using (HoursDBEntities db = new HoursDBEntities())
            {
                var updateEmployee = db.Employees.First(x => x.Id == employee.Id);
                updateEmployee.Name = employee.Name;
                db.SaveChanges();
            }
        }
    }
}