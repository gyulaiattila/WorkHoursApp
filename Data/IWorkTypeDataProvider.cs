// <copyright file="IWorkTypeDataProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WorkHoursApp.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WorkHoursApp.Model;

    /// <summary>
    /// Interface for WorkTypeDataProvider.
    /// </summary>
    public interface IWorkTypeDataProvider
    {
        /// <summary>
        /// Load worktypes from database into a list.
        /// </summary>
        /// <returns>Returns with worktype database object.</returns>
        Task<IEnumerable<WorkTypeDO>> GetAllAsync();

        /// <summary>
        /// Add new worktype to the table.
        /// </summary>
        /// <param name="workType">Contains the new worktype.</param>
        /// <returns>Returns with new worktype Id.</returns>
        int Add(WorkTypeDO workType);

        /// <summary>
        /// Modify worktype title.
        /// </summary>
        /// <param name="workType">Contains what we need to modify.</param>
        void Update(WorkTypeDO workType);

        /// <summary>
        /// Remove worktype from database.
        /// </summary>
        /// <param name="workType">Contains what we needs to remove.</param>
        void Remove(WorkTypeDO workType);
    }
}
