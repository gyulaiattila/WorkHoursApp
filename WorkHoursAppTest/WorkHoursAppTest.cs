// <copyright file="WorkHoursAppTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WorkHoursAppTest
{
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.ComponentModel;
    using WorkHoursApp.Data;
    using WorkHoursApp.Model;
    using WorkHoursApp.ViewModel;

    [TestClass]
    public class WorkHoursAppTest
    {
        //private readonly AdminViewModel _sut;
        //private readonly Fixture _fixture;
        //private readonly Mock<IEmployeeDataProvider> _mockEmployeeDataProvider;
        //private readonly Mock<IWorkTypeDataProvider> _mockWorkTypeDataProvider;
        //private readonly Mock<ITaskDataProvider> _mockTaskDataProvider;

        public WorkHoursAppTest()
        {
            //this._sut = new AdminViewModel();
            //this._fixture = new Fixture();
            //this._fixture.Customize(new AutoMoqCustomization());

            //this._mockEmployeeDataProvider = new Mock<IEmployeeDataProvider>();
        }

        [TestMethod]
        public void AdminViewModel_AddEmployee_SetsVisibilityTrue()
        {
            // Arrange
            var edo = new Mock<IEmployeeDataProvider>();
            var wdo = new Mock<IWorkTypeDataProvider>();
            var newemployee = "New";
            AdminViewModel aViewModel = new AdminViewModel(edo.Object, wdo.Object);
            var exceptedvisibility = aViewModel.SetVisibilityE.Equals(true);

            // Act
            aViewModel.AddEmployee(newemployee);

            // Assert
            var actual = aViewModel.SetVisibilityE;
            actual.Equals(exceptedvisibility);
        }

        [TestMethod]
        public void AdminViewModel_AddWorkType_SetsVisibilityTrue()
        {
            // Arrange
            var edo = new Mock<IEmployeeDataProvider>();
            var wdo = new Mock<IWorkTypeDataProvider>();
            AdminViewModel aViewModel = new AdminViewModel(edo.Object, wdo.Object);
            var actual = aViewModel.SetVisibilityWT;
            var newworktype = "NewType";
            var exceptedvisibility = AdminViewModel.SetVisibility.True;

            // Act
            aViewModel.AddWorkType(newworktype);

            // Assert
            actual = aViewModel.SetVisibilityWT;
            actual.Equals(exceptedvisibility);
        }
    }
}
