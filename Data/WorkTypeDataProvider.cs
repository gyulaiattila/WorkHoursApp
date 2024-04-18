// <copyright file="WorkTypeDataProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WorkHoursApp.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WorkHoursApp.Model;

    /// <summary>
    /// Class for WorkTypeDataProvider, contains database operations.
    /// </summary>
    public class WorkTypeDataProvider : IWorkTypeDataProvider
    {
        /// <inheritdoc/>
        public async Task<IEnumerable<WorkTypeDO>> GetAllAsync()
        {
            using (HoursDBEntities db = new HoursDBEntities())
            {
                List<WorkType> workType = (from wtp in db.WorkTypes select wtp).ToList();
                return workType.Select(e => new WorkTypeDO() { Id = e.Id, Type = e.Type });
            }
        }

        /// <inheritdoc/>
        public int Add(WorkTypeDO workType)
        {
            using (HoursDBEntities db = new HoursDBEntities())
            {
                var newWorkType = new WorkType() { Type = workType.Type };
                db.WorkTypes.Add(newWorkType);
                db.SaveChanges();
                return newWorkType.Id;
            }
        }

        /// <inheritdoc/>
        public void Remove(WorkTypeDO workType)
        {
            using (HoursDBEntities db = new HoursDBEntities())
            {
                var removeWorkType = db.WorkTypes.First(x => x.Id == workType.Id);
                db.WorkTypes.Remove(removeWorkType);
                db.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public void Update(WorkTypeDO workType)
        {
            using (HoursDBEntities db = new HoursDBEntities())
            {
                var updateWorkType = db.WorkTypes.First(x => x.Id == workType.Id);
                updateWorkType.Type = workType.Type;
                db.SaveChanges();
            }
        }
    }
}