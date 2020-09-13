using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Mutantes.API.Controllers;
using Mutantes.Core.Entities;
using Mutantes.Core.Interfaces;
using Mutantes.Core.Interfaces.Dna;
using Mutantes.Core.Services;
using Mutantes.Core.Services.Dna;
using Mutantes.Core.Utilities;
using Mutantes.Infraestructura.Data;
using Mutantes.Infraestructura.Interfaces;
using Mutantes.Infraestructura.Repositories;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Mutantes.IntegrationTest
{
  
    public class StatsControllerTests
    {
        private MutantsDbContext _context;
        private StatsController _statsController;
        private IStatsRepository _statsRepository;
        private IStatsService _statsService;       
        private IDnaSaverService _dnaSaverService;
        private IDnaAnalyzedRepository _dnaAnalyzedRepository;
        private Mock<ICacheService> _cacheService;

        public StatsControllerTests()
        {
            _cacheService = new Mock<ICacheService>();
            _cacheService.Setup(x => x.CacheResponseAsync("key", "value")).Returns(Task.FromResult(default(string)));
            _cacheService.Setup(x => x.GetCachedResponseAsync("key")).Returns(Task.FromResult(default(string)));
        }
      
        [SetUp]
        public void Setup()
        {
            var dbContextOptions = new DbContextOptionsBuilder<MutantsDbContext>().UseInMemoryDatabase("TestStats");
            _context = new MutantsDbContext(dbContextOptions.Options);
            _context.Database.EnsureCreated();

            _dnaAnalyzedRepository = new DnaAnalyzedRepository(_context);
            _statsRepository = new StatsRepository(_context);
            _statsService = new StatsService(_statsRepository);
            _statsController = new StatsController(_statsService);
            _dnaSaverService = new DnaSaverService(_dnaAnalyzedRepository, _cacheService.Object, _statsRepository);


        }


        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task Test001SiNoHayNingunStatsPrevioDevuelveTodo0Async()
        {
            OkObjectResult actionResult = (OkObjectResult )await  _statsController.GetStats();
            var value = (StatsEntitie)actionResult.Value;

            Assert.AreEqual(0, value.count_human_dna);
            Assert.AreEqual(0, value.count_mutant_dna);
            Assert.AreEqual(0.00, value.ratio);

        }


        [Test]
        public async Task Test002SiAgrego1MutanteLosStatsSonCorectosYRatio0()
        {
             
             await _dnaSaverService.saveDnaResultAsync(DnaListGenerator.DnaMutantMatrix(), true);


            OkObjectResult actionResult = (OkObjectResult)await _statsController.GetStats();
            var value = (StatsEntitie)actionResult.Value;

            Assert.AreEqual(0, value.count_human_dna);
            Assert.AreEqual(1, value.count_mutant_dna);
            Assert.AreEqual(0.00, value.ratio);

        }

        [Test]
        public async Task Test003SiAgrego1HumanoLosStatsSonCorectosYRatio1()
        {

            await _dnaSaverService.saveDnaResultAsync(DnaListGenerator.DnaHumanMatriz(), false);


            OkObjectResult actionResult = (OkObjectResult)await _statsController.GetStats();
            var value = (StatsEntitie)actionResult.Value;

            Assert.AreEqual(1, value.count_human_dna);
            Assert.AreEqual(0, value.count_mutant_dna);
            Assert.AreEqual(1.00, value.ratio);

        }

        [Test]
        public async Task Test004SiAgrego2HumanosYUnMutanteLosStatsSonCorrectosrectosYRatio2()
        {

            await _dnaSaverService.saveDnaResultAsync(DnaListGenerator.DnaHumanMatriz(), false);
            await _dnaSaverService.saveDnaResultAsync(DnaListGenerator.DnaHumanMatriz(), false);
            await _dnaSaverService.saveDnaResultAsync(DnaListGenerator.DnaMutantMatrix(), true);

            OkObjectResult actionResult = (OkObjectResult)await _statsController.GetStats();
            var value = (StatsEntitie)actionResult.Value;

            Assert.AreEqual(2, value.count_human_dna);
            Assert.AreEqual(1, value.count_mutant_dna);
            Assert.AreEqual(2.00, value.ratio);

        }

        [Test]
        public async Task Test004SiAgrego2MutantesYUnHumanoLosStatsSonCorrectosrectosYRatio0_5()
        {

            await _dnaSaverService.saveDnaResultAsync(DnaListGenerator.DnaHumanMatriz(), false);
            await _dnaSaverService.saveDnaResultAsync(DnaListGenerator.DnaMutantMatrix(), true);
            await _dnaSaverService.saveDnaResultAsync(DnaListGenerator.DnaMutantMatrix(), true);

            OkObjectResult actionResult = (OkObjectResult)await _statsController.GetStats();
            var value = (StatsEntitie)actionResult.Value;

            Assert.AreEqual(1, value.count_human_dna);
            Assert.AreEqual(2, value.count_mutant_dna);
            Assert.AreEqual(0.5, value.ratio);

        }









    }
}
