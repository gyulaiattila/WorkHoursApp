// <copyright file="MainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WorkHoursApp.ViewModel
{
    /// <summary>
    /// Class for MainViewModel.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _selectedViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="epmloyeeViewModel">Constructor for EmployeeViewModel.</param>
        /// <param name="adminViewModel">Constructor for AdminViewModel.</param>
        /// <param name="taskViewModel">Constructor for TaskViewModel.</param>
        public MainViewModel(EmployeeViewModel epmloyeeViewModel, AdminViewModel adminViewModel, TaskViewModel taskViewModel)
        {
            this.EmployeeViewModel = epmloyeeViewModel;
            this.AdminViewModel = adminViewModel;
            this.TaskViewModel = taskViewModel;
            this.SelectedViewModel = this.EmployeeViewModel;
            this.SelectedViewModel.LoadAsync();
            this.SelectViewModelCommand = new Command.Command(this.SelectViewModel);
        }

        /// <summary>
        /// Gets or sets SelectedViewModelCommand.
        /// </summary>
        public Command.Command SelectViewModelCommand { get; set; }

        /// <summary>
        /// Gets or sets SelectedViewModel.
        /// </summary>
        public ViewModelBase SelectedViewModel
        {
            get => this._selectedViewModel;
            set
            {
                this._selectedViewModel = value;
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets EmployeeViewModel.
        /// </summary>
        public EmployeeViewModel EmployeeViewModel { get; }

        /// <summary>
        /// Gets AdminViewModel.
        /// </summary>
        public AdminViewModel AdminViewModel { get; }

        /// <summary>
        /// Gets TaskViewModel.
        /// </summary>
        public TaskViewModel TaskViewModel { get; }

        private async void SelectViewModel(object parameter)
        {
            this.SelectedViewModel = parameter as ViewModelBase;
            await this.SelectedViewModel.LoadAsync();
        }
    }
}