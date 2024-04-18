// <copyright file="EmployeeViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WorkHoursApp.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows;
    using WorkHoursApp.Data;
    using WorkHoursApp.Model;

    /// <summary>
    /// Class for EmployeeView Model.
    /// </summary>
    public class EmployeeViewModel : ViewModelBase
    {
        private readonly IEmployeeDataProvider _employeeDataProvider;
        private readonly IWorkTypeDataProvider _workTypeDataProvider;
        private readonly ITaskDataProvider _taskDataProvider;
        private EmployeeDO _selectedEmployee;
        private WorkTypeDO _selectedWorkType;

        /// <summary>
        /// Using for Numeric Up-Down hour's element.
        /// </summary>
        private string _hourCounter = "0";

        /// <summary>
        /// Using for Numeric Up-Dowb min's element.
        /// </summary>
        private string _minCounter = "0";

        /// <summary>
        /// Using for comment element.
        /// </summary>
        private string _comment = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeViewModel"/> class.
        /// </summary>
        /// <param name="employeeDataProvider">Database operations for Employee.</param>
        /// <param name="workTypeDataProvider">Database operations for WorkType.</param>
        /// <param name="taskDataProvider">Database operations for Tasks.</param>
        public EmployeeViewModel(IEmployeeDataProvider employeeDataProvider, IWorkTypeDataProvider workTypeDataProvider, ITaskDataProvider taskDataProvider)
        {
            this._taskDataProvider = taskDataProvider;
            this._employeeDataProvider = employeeDataProvider;
            this._workTypeDataProvider = workTypeDataProvider;
            this.AddRecordCommand = new Command.Command(this.AddRecord, this.CanAddRecord);
            this.IncreaseHourCommand = new Command.Command(this.IncreaseHour, this.CanIncreaseHour);
            this.IncreaseMinCommand = new Command.Command(this.IncreaseMin, this.CanIncreaseMin);
            this.DecreaseHourCommand = new Command.Command(this.DecreaseHour, this.CanDecreaseHour);
            this.DecreaseMinCommand = new Command.Command(this.DecreaseMin, this.CanDecreaseMin);
        }

        /// <summary>
        /// Gets AddRecord void, bindid to EmployeeView.
        /// </summary>
        public Command.Command AddRecordCommand { get; }

        /// <summary>
        /// Gets IncreaseHour void, bindid to EmployeeView.
        /// </summary>
        public Command.Command IncreaseHourCommand { get; }

        /// <summary>
        /// Gets IncreaseMin void, bindid to EmployeeView.
        /// </summary>
        public Command.Command IncreaseMinCommand { get; }

        /// <summary>
        /// Gets DecreaseHour void, bindid to EmployeeView.
        /// </summary>
        public Command.Command DecreaseHourCommand { get; }

        /// <summary>
        /// Gets DecreaseMin void, bindid to EmployeeView.
        /// </summary>
        public Command.Command DecreaseMinCommand { get; }

        /// <summary>
        /// Gets or sets hourcounter.
        /// </summary>
        public string HourCounter
        {
            get => this._hourCounter;
            set
            {
                this._hourCounter = value;
                this.RaisePropertyChanged(nameof(this.HourCounter));
                this.AddRecordCommand.RaiseCanExecuteChanged();
                this.IncreaseHourCommand.RaiseCanExecuteChanged();
                this.DecreaseHourCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets mincounter.
        /// </summary>
        public string MinCounter
        {
            get => this._minCounter;
            set
            {
                this._minCounter = value;
                this.RaisePropertyChanged(nameof(this.MinCounter));
                this.AddRecordCommand.RaiseCanExecuteChanged();
                this.IncreaseMinCommand.RaiseCanExecuteChanged();
                this.DecreaseMinCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets comment.
        /// </summary>
        public string Comment
        {
            get => this._comment;
            set
            {
                this._comment = value;
                this.RaisePropertyChanged(nameof(this.Comment));
            }
        }

        /// <summary>
        /// Gets Observable Collection about Employees.
        /// </summary>
        public ObservableCollection<EmployeeDO> Employees { get; } = new ObservableCollection<EmployeeDO>();

        /// <summary>
        /// Gets Observable Collection about WorkTypes.
        /// </summary>
        public ObservableCollection<WorkTypeDO> WorkTypes { get; } = new ObservableCollection<WorkTypeDO>();

        /// <summary>
        /// Gets Observable Collection about Task Records.
        /// </summary>
        public ObservableCollection<RecordDO> Records { get; } = new ObservableCollection<RecordDO>();

        /// <summary>
        /// Gets or sets SelectedEmployee.
        /// </summary>
        public EmployeeDO SelectedEmployee
        {
            get => this._selectedEmployee;
            set
            {
                this._selectedEmployee = value;
                this.RaisePropertyChanged();
                this.AddRecordCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets SelectedWorkType.
        /// </summary>
        public WorkTypeDO SelectedWorkType
        {
            get => this._selectedWorkType;
            set
            {
                this._selectedWorkType = value;
                this.RaisePropertyChanged();
                this.AddRecordCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Load datas from database.
        /// </summary>
        /// <returns>List from database records.</returns>
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

                this.HourCounter = "0";
                this.MinCounter = "0";
            }
            catch (Exception)
            {
                MessageBox.Show("Nem sikerült kapcsolódni az adatbázishoz!", "Hiba");
            }
        }

        /// <summary>
        /// Increase hour counter, bindid to EmployeeView button.
        /// </summary>
        public void IncreaseHour(object parameter)
        {
            int temphour = int.Parse(this._hourCounter);
            temphour++;
            this._hourCounter = temphour.ToString();
            this.RaisePropertyChanged(nameof(this.HourCounter));
            this.AddRecordCommand.RaiseCanExecuteChanged();
            this.IncreaseHourCommand.RaiseCanExecuteChanged();
            this.DecreaseHourCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Increase min counter, bindid to EmployeeView button.
        /// </summary>
        public void IncreaseMin(object parameter)
        {
            int tempmin = int.Parse(this._minCounter);
            tempmin++;
            this._minCounter = tempmin.ToString();
            this.RaisePropertyChanged(nameof(this.MinCounter));
            this.AddRecordCommand.RaiseCanExecuteChanged();
            this.IncreaseMinCommand.RaiseCanExecuteChanged();
            this.DecreaseMinCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Decrease hour counter, bindid to EmployeeView button.
        /// </summary>
        public void DecreaseHour(object parameter)
        {
            int temphour = int.Parse(this._hourCounter);
            temphour--;
            this._hourCounter = temphour.ToString();
            this.RaisePropertyChanged(nameof(this.HourCounter));
            this.AddRecordCommand.RaiseCanExecuteChanged();
            this.DecreaseHourCommand.RaiseCanExecuteChanged();
            this.IncreaseHourCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Decrease min counter, bindid to EmployeeView button.
        /// </summary>
        public void DecreaseMin(object parameter)
        {
            int tempmin = int.Parse(this._minCounter);
            tempmin--;
            this._minCounter = tempmin.ToString();
            this.RaisePropertyChanged(nameof(this.MinCounter));
            this.AddRecordCommand.RaiseCanExecuteChanged();
            this.DecreaseMinCommand.RaiseCanExecuteChanged();
            this.IncreaseMinCommand.RaiseCanExecuteChanged();
        }

        private bool CanAddRecord(object parameter)
        {
            try
            {
                return this.SelectedWorkType != null && this.SelectedEmployee != null && int.TryParse(this.HourCounter, out int b) && int.TryParse(this.MinCounter, out int c) && (int.Parse(this.HourCounter) + int.Parse(this.MinCounter)) >0;
            }
            catch (Exception)
            {
                MessageBox.Show("Hibás érték szerepel a beviteli mezőben!", "Figyelmeztetés!");
                this.MinCounter = "0";
                this.HourCounter = "0";
                return false;
            }
        }

        private void AddRecord(object parameter)
        {
            try
            {
                var record = new RecordDO
                {
                    Name = this.SelectedEmployee.Name,
                    Type = this.SelectedWorkType.Type,
                    Time = (int.Parse(this.HourCounter) * 60) + int.Parse(this.MinCounter),
                    Comment = this.Comment,
                    Date = DateTime.Now,
                };
                this._taskDataProvider.Add(record);
                this.Records.Add(record);
                this.SelectedWorkType = null;
                this.SelectedEmployee = null;
                this.HourCounter = "0";
                this.MinCounter = "0";
                this.Comment = string.Empty;
                MessageBox.Show("Sikeres rögzítés", "Információ");
            }
            catch (Exception)
            {
                MessageBox.Show("Sikertelen rögzítés!", "Hiba");
            }
        }

        private bool CanIncreaseHour(object parameter)
        {
            try
            {
                if (int.Parse(this.HourCounter) > 23)
                {
                    this.HourCounter = "0";
                }

                return int.Parse(this.HourCounter) < 23;
            }
            catch (Exception)
            {
                MessageBox.Show("Hibás érték szerepel a beviteli mezőben!", "Figyelmeztetés!");
                this.HourCounter = "0";
                return true;
            }
        }

        private bool CanIncreaseMin(object parameter)
        {
            try
            {
                if (int.Parse(this.MinCounter) > 59)
                {
                    this.MinCounter = "0";
                }

                return int.Parse(this.MinCounter) < 59;
            }
            catch (Exception)
            {
                MessageBox.Show("Hibás érték szerepel a beviteli mezőben!", "Figyelmeztetés!");
                this.MinCounter = "0";
                return true;
            }
        }

        private bool CanDecreaseHour(object parameter) => int.Parse(this.HourCounter) > 0;

        private bool CanDecreaseMin(object parameter) => int.Parse(this.MinCounter) > 0;
    }
}