// <copyright file="SetNewWorkTypeVisibilityConv.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WorkHoursApp.Converter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using static WorkHoursApp.ViewModel.AdminViewModel;

    /// <summary>
    /// Class for SetNewWorkTypeVisibility-Converter.
    /// </summary>
    public class SetNewWorkTypeVisibilityConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (SetVisibility)value;
            return visibility == SetVisibility.True
                ? "Visible"
                : "Hidden";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
