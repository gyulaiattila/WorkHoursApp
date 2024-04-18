// <copyright file="AdminViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WorkHoursApp.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows;
    using WorkHoursApp.Model;

    /// <summary>
    /// Class for AdminViewModel.
    /// </summary>
    public class AdminViewModel : ViewModelBase
    {
        private readonly Data.IEmployeeDataProvider _employeeDataProvider;
        private readonly Data.IWorkTypeDataProvider _workTypeDataProvider;
        private EmployeeDO _selectedEmployee;
        private WorkTypeDO _selectedWorkType;
        private SetVisibility _setVisibilityE;
        private SetVisibility _setVisibilityWT;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminViewModel"/> class.
        /// </summary>
        /// <param name="employeeDataProvider">Contains database operations for employees.</param>
        /// <param name="workTypeDataProvider">Contains database operations for worktypes.</param>
        public AdminViewModel(Data.IEmployeeDataProvider employeeDataProvider, Data.IWorkTypeDataProvider workTypeDataProvider)
        {
            this._employeeDataProvider = employeeDataProvider;
            this._workTypeDataProvider = workTypeDataProvider;
            this.AddEmployeeCommand = new Command.Command(this.AddEmployee);
            this.DeleteEmployeeCommand = new Command.Command(this.DeleteEmployee, this.CanDeleteEmployee);
            this.UpdateEmployeeCommand = new Command.Command(this.UpdateEmployee);
            this.AddWorkTypeCommand = new Command.Command(this.AddWorkType);
            this.DeleteWorkTypeCommand = new Command.Command(this.DeleteWorkType, this.CanDeleteWorkType);
            this.UpdateWorkTypeCommand = new Command.Command(this.UpdateWorkType);
            this.SetVisibilityCommand = new Command.Command(this.SetNewEmployeeVisibility, this.CanDeleteEmployee);
            this.SetWorkTypeVisibilityCommand = new Command.Command(this.SetNewWorkTypeVisibility, this.CanDeleteWorkType);
        }

        /// <summary>
        /// Visibility absorbable value.
        /// </summary>
        public enum SetVisibility
        {
            /// <summary>
            /// Hide field.
            /// </summary>
            False,

            /// <summary>
            /// Show field.
            /// </summary>
            True,
        }

        /// <summary>
        /// Gets visibility for new employee field.
        /// </summary>
        public SetVisibility SetVisibilityE
        {
            get => this._setVisibilityE;
            private set
            {
                this._setVisibilityE = value;
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets visibility for new worktype field.
        /// </summary>
        public SetVisibility SetVisibilityWT
        {
            get => this._setVisibilityWT;
            private set
            {
                this._setVisibilityWT = value;
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets SelectedEmployee object.
        /// </summary>
        public EmployeeDO SelectedEmployee
        {
            get => this._selectedEmployee;
            set
            {
                this._selectedEmployee = value;
                this.RaisePropertyChanged();
                this.DeleteEmployeeCommand.RaiseCanExecuteChanged();
                this.SetVisibilityCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets SelectedWorkType object.
        /// </summary>
        public WorkTypeDO SelectedWorkType
        {
            get => this._selectedWorkType;
            set
            {
                this._selectedWorkType = value;
                this.RaisePropertyChanged();
                this.DeleteWorkTypeCommand.RaiseCanExecuteChanged();
                this.SetWorkTypeVisibilityCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets AddEmployee void, bindid to AdminView.
        /// </summary>
        public Command.Command AddEmployeeCommand { get; }

        /// <summary>
        /// Gets DeleteEmployee void, bindid to AdminView.
        /// </summary>
        public Command.Command DeleteEmployeeCommand { get; }

        /// <summary>
        /// Gets UpdateEmployee void, bindid to AdminView.
        /// </summary>
        public Command.Command UpdateEmployeeCommand { get; }

        /// <summary>
        /// Gets AddWorkType void, bindid to AdminView.
        /// </summary>
        public Command.Command AddWorkTypeCommand { get; }

        /// <summary>
        /// Gets DeleteWorkType void, bindid to AdminView.
        /// </summary>
        public Command.Command DeleteWorkTypeCommand { get; }

        /// <summary>
        /// Gets UpdateWorkType void, bindid to AdminView.
        /// </summary>
        public Command.Command UpdateWorkTypeCommand { get; }

        /// <summary>
        /// Gets SetVisibility void, bindid to AdminView.
        /// </summary>
        public Command.Command SetVisibilityCommand { get; }

        /// <summary>
        /// Gets SetVisibilityWT void, bindid to AdminView.
        /// </summary>
        public Command.Command SetWorkTypeVisibilityCommand { get; }

        /// <summary>
        /// Gets observable Collection for Employees.
        /// </summary>
        public ObservableCollection<EmployeeDO> Employees { get; } = new ObservableCollection<EmployeeDO>();

        /// <summary>
        /// Gets Observable Collection for WorkTypes.
        /// </summary>
        public ObservableCollection<WorkTypeDO> WorkTypes { get; } = new ObservableCollection<WorkTypeDO>();

        /// <summary>
        /// Load data from database into a list.
        /// </summary>
        /// <returns>List with employees from database.</returns>
        public async override Task LoadAsync()
        {
            try
            {
                this.Employees.Clear();
                var employees = await this._employeeDataProvider.GetAllAsync();
                if (employees != null)
                {
                    foreach (var employee in employees)
                    {
                        this.Employees.Add(employee);
                    }
                }

                this.WorkTypes.Clear();
                var worktypes = await this._workTypeDataProvider.GetAllAsync();
                if (worktypes != null)
                {
                    foreach (var workytpe in worktypes)
                    {
                        this.WorkTypes.Add(workytpe);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nem sikerült kapcsolódni az adatbázishoz!", "Hiba");
            }
        }

        private void UpdateWorkType(object parameter)
        {
            try
            {
                var worktype = new WorkTypeDO { Id = this.SelectedWorkType.Id, Type = this.SelectedWorkType.Type };
                this._workTypeDataProvider.Update(worktype);
                this.SetNewWorkTypeVisibility(worktype);
                MessageBox.Show("Sikeres módosítás!", "Információ!");
            }
            catch (Exception)
            {
                MessageBox.Show("A módosítás sikertelen!", "Hiba!");
            }
        }

        private void UpdateEmployee(object parameter)
        {
            try
            {
                var employee = new EmployeeDO { Id = this.SelectedEmployee.Id, Name = this.SelectedEmployee.Name };
                this._employeeDataProvider.Update(employee);
                this.SetNewEmployeeVisibility(employee);
                MessageBox.Show("Sikeres módosítás", "Információ!");
            }
            catch (Exception)
            {
                MessageBox.Show("A név módosítása sikertelen!", "Hiba!");
            }
        }

        private void DeleteEmployee(object parameter)
        {
            if (this.SelectedEmployee != null)
            {
                try
                {
                    this._employeeDataProvider.Remove(this.SelectedEmployee);
                    this.Employees.Remove(this.SelectedEmployee);
                    this.SelectedEmployee = null;
                    if (this.SetVisibilityE == SetVisibility.True)
                    {
                        this.SetVisibilityE = SetVisibility.False;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Eltávolítás sikertelen!", "Hiba!");
                }
            }
        }

        private void DeleteWorkType(object parameter)
        {
            if (this.SelectedWorkType != null)
            {
                try
                {
                    this._workTypeDataProvider.Remove(this._selectedWorkType);
                    this.WorkTypes.Remove(this.SelectedWorkType);
                    this.SelectedWorkType = null;
                    if (this.SetVisibilityWT == SetVisibility.True)
                    {
                        this.SetVisibilityWT = SetVisibility.False;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Eltávolítás sikertelen!", "Hiba!");
                }
            }
        }

        private bool CanDeleteEmployee(object parameter) => this.SelectedEmployee != null;

        private bool CanDeleteWorkType(object parameter) => this.SelectedWorkType != null;

        private void AddEmployee(object parameter)
        {
            try
            {
                var employee = new EmployeeDO { Name = "New" };
                var viewModel = new EmployeeItemViewModel(employee);
                this._employeeDataProvider.Add(employee);
                this.Employees.Add(employee);
                this.SetVisibilityE = SetVisibility.True;
                this.LoadAsync();
                this.SelectedEmployee = this.Employees[this.Employees.Count - 1];
                this.RaisePropertyChanged(nameof(this.SelectedEmployee));
            }
            catch (Exception)
            {
                MessageBox.Show("A dolgozó hozzáadása sikertelen!", "Hiba!");
            }
        }

        private void AddWorkType(object parameter)
        {
            try
            {
                var worktype = new WorkTypeDO { Type = "NewType" };
                var viewModel = new WorkTypeItemViewModel(worktype);
                this._workTypeDataProvider.Add(worktype);
                this.WorkTypes.Add(worktype);
                this.SetVisibilityWT = SetVisibility.True;
                this.LoadAsync();
                this.SelectedWorkType = this.WorkTypes[this.WorkTypes.Count - 1];
                this.RaisePropertyChanged(nameof(this.SelectedWorkType));
            }
            catch (Exception)
            {
                MessageBox.Show("A munkafolyamat hozzáadása sikertelen!", "Hiba!");
            }
        }

        private void SetNewEmployeeVisibility(object parameter)
        {
            this.SetVisibilityE = this.SetVisibilityE == SetVisibility.True
            ? SetVisibility.False
              : SetVisibility.True;
        }

        private void SetNewWorkTypeVisibility(object parameter)
        {
            this.SetVisibilityWT = this.SetVisibilityWT == SetVisibility.True
            ? SetVisibility.False
              : SetVisibility.True;
        }
    }
}