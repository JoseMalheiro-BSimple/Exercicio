using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.ProjectTests
{
    public class ProjectGetAcronymTests
    {
        [Fact]
        public void WhenCallingMethod_ThenReturnsAcrynom()
        {
            // arrange
            var acronym = "TTL";
            var project = new Project(1, "title", acronym, It.IsAny<PeriodDate>());

            // act
            var result = project.GetAcronym();

            // assert
            Assert.Equal(result, acronym);
        }
    }
}