// <copyright file="WorkTypeItemViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WorkHoursApp.ViewModel
{
    using WorkHoursApp.Model;

    /// <summary>
    /// Class for WorkTypeViewModel.
    /// </summary>
    public class WorkTypeItemViewModel : ViewModelBase
    {
        private readonly WorkTypeDO _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkTypeItemViewModel"/> class.
        /// </summary>
        public WorkTypeItemViewModel(WorkTypeDO model)
        {
            this._model = model;
        }

        /// <summary>
        /// Gets WorkType's Id.
        /// </summary>
        public int Id => this._model.Id;

        /// <summary>
        /// Gets or sets WorkType's Type.
        /// </summary>
        public string Tipus
        {
            get => this._model.Type;
            set
            {
                this._model.Type = value;
                this.RaisePropertyChanged();
            }
        }
    }
}