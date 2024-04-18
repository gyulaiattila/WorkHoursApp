// <copyright file="TaskViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WorkHoursApp.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows;
    using Microsoft.Office.Interop.Excel;
    using WorkHoursApp.Command;
    using WorkHoursApp.Data;
    using WorkHoursApp.Model;

    /// <summary>
    /// Class for TaskViewModel.
    /// </summary>
    public class TaskViewModel : ViewModelBase
    {
        private readonly ITaskDataProvider _taskDataProvider;

        private RecordDO _selectedRecord;
        private DateTime _startDate = DateTime.Today.AddDays(-30);
        private DateTime _endDate = DateTime.Now.AddHours(23 - DateTime.Now.Hour).AddMinutes(58 - DateTime.Now.Minute);

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskViewModel"/> class.
        /// </summary>
        /// <param name="taskDataProvider">Database operations for tasks.</param>
        public TaskViewModel(ITaskDataProvider taskDataProvider)
        {
            this._taskDataProvider = taskDataProvider;
            this.DeleteRecordCommand = new Command(this.DeleteRecord, this.CanDeleteRecord);
            this.ExportCommand = new Command(this.Export);
            this.ReloadRecordCommand = new Command(this.ReloadRecord);
        }

        /// <summary>
        /// Gets DeleteRecordCommand.
        /// </summary>
        public Command DeleteRecordCommand { get; }

        /// <summary>
        /// Gets ExportCommand.
        /// </summary>
        public Command ExportCommand { get; }

        /// <summary>
        /// Gets ReloadRecordCommand.
        /// </summary>
        public Command ReloadRecordCommand { get; }

        /// <summary>
        /// Gets observable collections about Records.
        /// </summary>
        public ObservableCollection<RecordDO> Records { get; } = new ObservableCollection<RecordDO>();

        /// <summary>
        /// Gets or sets SelectedRecord.
        /// </summary>
        public RecordDO SelectedRecord
        {
            get => this._selectedRecord;
            set
            {
                this._selectedRecord = value;
                this.RaisePropertyChanged();
                this.DeleteRecordCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets StartDate for date filtering.
        /// </summary>
        public DateTime StartDate
        {
            get => this._startDate;
            set
            {
                this._startDate = value;
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets EndDate for date filtering.
        /// </summary>
        public DateTime EndDate
        {
            get => this._endDate;
            set
            {
                this._endDate = value;
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Load records from database.
        /// </summary>
        /// <returns>List about records from database.</returns>
        public async override Task LoadAsync()
        {
            try
            {
                this.Records.Clear();
                var tasks = await this._taskDataProvider.GetAllAsync();
                if (tasks != null)
                {
                    foreach (var task in tasks)
                    {
                        if (task.Date > this.StartDate && task.Date < this.EndDate.AddHours(23).AddMinutes(59))
                        {
                            this.Records.Add(task);
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nem sikerült kapcsolódni az adatbázishoz!", "Hiba");
            }
        }

        private void DeleteRecord(object parameter)
        {
            if (this.SelectedRecord != null)
            {
                try
                {
                    this._taskDataProvider.Remove(this.SelectedRecord);
                    this.Records.Remove(this.SelectedRecord);
                    this.SelectedRecord = null;
                }
                catch (Exception)
                {
                    MessageBox.Show("Az eltávolítás sikertelen!", "Hiba!");
                }
            }
        }

        private bool CanDeleteRecord(object parameter) => this.SelectedRecord != null;

        private void ReloadRecord(object parameter)
        {
            this.LoadAsync();
        }

        private void Export(object parameter)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
                Workbook workbook = application.Workbooks.Add(string.Empty);
                _Worksheet worksheet = workbook.ActiveSheet;
                worksheet.Cells[1, 1] = "Név";
                worksheet.Cells[1, 2] = "Munkafolyamat";
                worksheet.Cells[1, 3] = "Időtartam";
                worksheet.Cells[1, 4] = "Dátum";
                worksheet.Cells[1, 5] = "Megjegyzés";
                int a = this.Records.Count;
                int stepper = 0;
                for (int i = 2; stepper < a; i++)
                {
                    worksheet.Cells[i, 1] = this.Records[stepper].Name;
                    worksheet.Cells[i, 2] = this.Records[stepper].Type;
                    worksheet.Cells[i, 3] = this.Records[stepper].Time;
                    worksheet.Cells[i, 4] = this.Records[stepper].Date;
                    worksheet.Cells[i, 5] = this.Records[stepper].Comment;
                    stepper++;
                }

                workbook.SaveAs("Export.xlsx");
                workbook.Close();
                application.Quit();
                MessageBox.Show("Az exportálás sikeres! A táblázat a dokumentumok mappában található!", "Információ!");
            }
            catch (Exception)
            {
                MessageBox.Show("Sikertelen művelet, próbálja újra!", "Figyelmeztetés!");
            }
        }
    }
}