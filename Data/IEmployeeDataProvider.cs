// <copyright file="IEmployeeDataProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WorkHoursApp.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WorkHoursApp.Model;

    /// <summary>
    /// Interface for EmployeeDataProvider.
    /// </summary>
    public interface IEmployeeDataProvider
    {
        /// <summary>
        /// Load employees into a list.
        /// </summary>
        /// <returns>Returns employees in a Database object.</returns>
        Task<IEnumerable<EmployeeDO>> GetAllAsync();

        /// <summary>
        /// Add new employee to the table.
        /// </summary>
        /// <param name="employee">Contains the object what we adding to the table.</param>
        /// <returns>Void returns with Id.</returns>
        int Add(EmployeeDO employee);

        /// <summary>
        /// Remove employee from table.
        /// </summary>
        /// <param name="employee">Removable emplyee.</param>
        void Remove(EmployeeDO employee);

        /// <summary>
        /// Modify employee name.
        /// </summary>
        /// <param name="employee">The element what we modify.</param>
        void Update(EmployeeDO employee);
    }
}