using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mutantes.API.Controllers;
using Mutantes.Core.Entities;
using Mutantes.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class StatsControllerTests
    {
        Mock<IStatsService> _statsService = new Mock<IStatsService>();
       



        [TestMethod]
        public async Task Test001StatsVacioDevuelveTodo0Async()
        {

            StatsEntitie entitieTest01 = new StatsEntitie
            {
                count_human_dna = 0,
                count_mutant_dna = 0,
                ratio = 0,
            };


            _statsService.Setup(x => x.GetStats()).Returns(Task.FromResult(entitieTest01));

            StatsController controller = new StatsController(_statsService.Object);

            OkObjectResult response = (OkObjectResult)await controller.GetStats();

            var result = response.Value;

            Assert.AreSame(result, entitieTest01);

        }


        [TestMethod]
        public async Task Test002Inserto2MutantesYDosHumanosElControladorDevuelveCorrectamenteLosResultados()
        {


        }


    }
}
