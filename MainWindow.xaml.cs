// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WorkHoursApp
{
    using System.Windows;
    using WorkHoursApp.Data;
    using WorkHoursApp.ViewModel;

    /// <summary>
    /// MainWindow class.
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this._viewModel = new MainViewModel(
                new EmployeeViewModel(new EmployeeDataProvider(), new WorkTypeDataProvider(), new TaskDataProvider()),
                new AdminViewModel(new EmployeeDataProvider(), new WorkTypeDataProvider()),
                new TaskViewModel(new TaskDataProvider()));
            this.DataContext = this._viewModel;
        }
    }
}
