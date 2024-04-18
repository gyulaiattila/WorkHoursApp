// <copyright file="RecordDO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WorkHoursApp.Model
{
    /// <summary>
    /// Class for Record database object.
    /// </summary>
    public class RecordDO
    {
        /// <summary>
        /// Gets or sets record Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets employee name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets Time.
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// Gets or sets date.
        /// </summary>
        public System.DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets comment.
        /// </summary>
        public string Comment { get; set; }
    }
}
