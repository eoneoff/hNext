﻿using hNext.Infrastructure;
using hNext.Model;
using hNext.WebClient.Components;
using hNext.WebClient.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.WebClient.Tests
{
    [TestClass]
    public class CaseHistoryDiagnosysEditorViewComponentTests
    {
        private CaseHistoryDiagnosysEditorViewComponent component = new CaseHistoryDiagnosysEditorViewComponent();
        private UniqueList<string> modules = new UniqueList<string>();

        [TestMethod]
        public void InvokeReturnsView()
        {
            //Arrange

            //Act
            var result = component.Invoke(modules);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewViewComponentResult));
        }

        [TestMethod]
        public void InvokeAddsNecessaryModules()
        {
            // Arrange
            var mod = new List<string>
            {
                nameof(ConfirmationDialogViewComponent).ViewComponentName(),
                nameof(PatientDiagnosysEditorViewComponent).ViewComponentName()
            };

            //Act
            var result = component.Invoke(modules);

            //Assert
            CollectionAssert.AreEquivalent(mod, modules);
        }

        [TestMethod]
        public void InvokeReturnsCorrectModel()
        {
            //Arrange

            //Act
            var result = (component.Invoke(modules) as ViewViewComponentResult)?.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(CaseHistoryDiagnosys));
        }
    }
}
