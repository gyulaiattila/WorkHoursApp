// <copyright file="TaskDataProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WorkHoursApp.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WorkHoursApp.Model;

    /// <summary>
    /// Class for TaskDataProvider, contains database operations.
    /// </summary>
    public class TaskDataProvider : ITaskDataProvider
    {
        /// <inheritdoc/>
        public async Task<IEnumerable<Model.RecordDO>> GetAllAsync()
        {
            using (HoursDBEntities db = new HoursDBEntities())
            {
                List<Record> record = (from rcrd in db.Records select rcrd).ToList();
                return record.Select(r => new RecordDO() { Id = r.Id, Name = r.Name, Date = r.Date, Time = r.Time, Type = r.Type, Comment = r.Comment });
            }
        }

        /// <inheritdoc/>
        public int Add(RecordDO record)
        {
            using (HoursDBEntities db = new HoursDBEntities())
            {
                var newRecord = new Record() { Name = record.Name, Date = record.Date, Time = record.Time, Type = record.Type, Comment = record.Comment };
                db.Records.Add(newRecord);
                db.SaveChanges();
                return newRecord.Id;
            }
        }

        /// <inheritdoc/>
        public void Remove(RecordDO record)
        {
            using (HoursDBEntities db = new HoursDBEntities())
            {
                var removeRecord = db.Records.First(x => x.Id == record.Id);
                db.Records.Remove(removeRecord);
                db.SaveChanges();
            }
        }
    }
}