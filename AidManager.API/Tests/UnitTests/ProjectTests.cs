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

        [Test]
        public void UpdateRating_WithInvalidRating_ShouldThrowException()
        {
            // Arrange
            var project = new Project();

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => project.UpdateRating(6.0));
            Assert.That(exception.Message, Does.Contain("Rating not valid"));
        }

        [Test]
        public void AddTeamMember_WhenMemberNotInTeam_ShouldAddToTeam()
        {
            // Arrange
            var project = new Project();
            
            // Create a CreateUserCommand to pass to the User constructor
            var createUserCommand = new CreateUserCommand(
                "John", 
                "Doe", 
                30, 
                "test@example.com", 
                "1234567890", 
                "password123", 
                "profile.jpg", 
                1, 
                "Test Company", 
                "company@test.com", 
                "USA", 
                "ABC123"
            );
            
            var user = new User(createUserCommand);
            
            // Use reflection to set Id if needed (if it's not set by constructor)
            typeof(User).GetProperty("Id")?.SetValue(user, 1);

            // Act
            project.AddTeamMember(user);

            // Assert
            Assert.That(project.TeamMembers.Count, Is.EqualTo(1));
            Assert.That(project.TeamMembers[0].Id, Is.EqualTo(1));
        }

        [Test]
        public void AddTeamMember_WhenMemberAlreadyInTeam_ShouldNotDuplicate()
        {
            // Arrange
            var project = new Project();
            
            // Create a CreateUserCommand to pass to the User constructor
            var createUserCommand = new CreateUserCommand(
                "John", 
                "Doe", 
                30, 
                "test@example.com", 
                "1234567890", 
                "password123", 
                "profile.jpg", 
                1, 
                "Test Company", 
                "company@test.com", 
                "USA", 
                "ABC123"
            );
            
            var user = new User(createUserCommand);
            
            // Use reflection to set Id if needed (if it's not set by constructor)
            typeof(User).GetProperty("Id")?.SetValue(user, 1);

            // Act
            project.AddTeamMember(user);
            project.AddTeamMember(user);

            // Assert
            Assert.That(project.TeamMembers.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddImage_WithValidCommand_ShouldAddImages()
        {
            // Arrange
            var project = new Project();
            var command = new AddProjectImageCommand(1, new List<string> { "image1.jpg", "image2.jpg" });

            // Act
            project.AddImage(command);

            // Assert
            Assert.That(project.ImageUrl.Count, Is.EqualTo(2));
            Assert.That(project.ImageUrl[0].Url, Is.EqualTo("image1.jpg"));
            Assert.That(project.ImageUrl[1].Url, Is.EqualTo("image2.jpg"));
        }
    }
}