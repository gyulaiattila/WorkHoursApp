// <copyright file="ITaskDataProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WorkHoursApp.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WorkHoursApp.Model;

    /// <summary>
    /// Interface for TaskDataProvider.
    /// </summary>
    public interface ITaskDataProvider
    {
        /// <summary>
        /// Load records from database to list.
        /// </summary>
        /// <returns>Returns records in database object.</returns>
        Task<IEnumerable<RecordDO>> GetAllAsync();

        /// <summary>
        /// Add record to the table.
        /// </summary>
        /// <param name="record">Contains the new record's parameters.</param>
        /// <returns>Void returns with Id.</returns>
        int Add(RecordDO record);

        /// <summary>
        /// Remove record from the table.
        /// </summary>
        /// <param name="record">Contains which record needs to be removed.</param>
        void Remove(RecordDO record);
    }
}
