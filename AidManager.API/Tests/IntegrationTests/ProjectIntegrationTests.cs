using AidManager.API.Collaborate.Domain.Model.Commands;
using AidManager.API.ManageTasks.Domain.Model.Aggregates;
using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.ManageTasks.Interfaces.ACL;
using AidManager.API.IAM.Interfaces.ACL;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AidManager.API.ManageTasks.Domain.Model.Commands;

namespace AidManager.Tests.Integration
{
    [TestFixture]
    public class ProjectIntegrationTests
    {
        private Mock<IManageTasksFacade> _manageTasksFacadeMock;
        private Mock<IIamContextFacade> _iamContextFacadeMock;

        [SetUp]
        public void Setup()
        {
            _manageTasksFacadeMock = new Mock<IManageTasksFacade>();
            _iamContextFacadeMock = new Mock<IIamContextFacade>();
        }

        [Test]
        public async Task GetProjectsByCompany_ShouldReturnProjectsForCompanyId()
        {
            // Arrange
            var companyId = 1;
            var expectedProjects = new List<Project>
            {
                new Project
                {
                    Id = 1,
                    Name = "Project 1",
                    Description = "Description 1",
                    CompanyId = companyId,
                    Rating = 4.5,
                    ProjectDate = DateOnly.FromDateTime(DateTime.Now),
                    ProjectLocation = "Location 1"
                },
                new Project
                {
                    Id = 2,
                    Name = "Project 2",
                    Description = "Description 2",
                    CompanyId = companyId,
                    Rating = 3.8,
                    ProjectDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                    ProjectLocation = "Location 2"
                }
            };

            _manageTasksFacadeMock.Setup(m => m.GetProjectsByCompany(companyId))
                .ReturnsAsync(expectedProjects);

            // Act
            var projects = await _manageTasksFacadeMock.Object.GetProjectsByCompany(companyId);

            // Assert
            Assert.That(projects, Is.Not.Null);
            Assert.That(projects.Count(), Is.EqualTo(2));
            Assert.That(projects.All(p => p.CompanyId == companyId), Is.True);

            // Verify the interaction between bounded contexts
            _manageTasksFacadeMock.Verify(m => m.GetProjectsByCompany(companyId), Times.Once);
        }

       
    }
}