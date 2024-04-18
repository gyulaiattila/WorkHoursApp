// <copyright file="EmployeeItemViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WorkHoursApp.ViewModel
{
    using WorkHoursApp.Model;

    /// <summary>
    /// Class for EmployeeItemViewModel.
    /// </summary>
    public class EmployeeItemViewModel : ViewModelBase
    {
        private readonly EmployeeDO _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeItemViewModel"/> class.
        /// </summary>
        public EmployeeItemViewModel(EmployeeDO model)
        {
            this._model = model;
        }

        /// <summary>
        /// Gets Employee's Id.
        /// </summary>
        public int Id => this._model.Id;

        /// <summary>
        /// Gets or sets Employee's name.
        /// </summary>
        public string Nev
        {
            get => this._model.Name;
            set
            {
                this._model.Name = value;
                this.RaisePropertyChanged();
            }
        }
    }
}