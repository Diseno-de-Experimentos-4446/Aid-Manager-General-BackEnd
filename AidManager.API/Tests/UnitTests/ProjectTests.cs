using AidManager.API.Collaborate.Domain.Model.Commands;
using AidManager.API.ManageTasks.Domain.Model.Aggregates;
using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.Authentication.Domain.Model.Commands;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AidManager.Tests.CoreEntities
{
    [TestFixture]
    public class ProjectTests
    {

        [Test]
        public void UpdateRating_WithValidRating_ShouldUpdateRating()
        {
            // Arrange
            var project = new Project();

            // Act
            project.UpdateRating(4.5);

            // Assert
            Assert.That(project.Rating, Is.EqualTo(4.5));
        }

        
    }
}