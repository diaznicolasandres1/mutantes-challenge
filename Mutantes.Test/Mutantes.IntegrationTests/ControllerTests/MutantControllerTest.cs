using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Mutantes.API;
using Mutantes.API.Controllers;
using Mutantes.API.Utilities;
using Mutantes.Core.DTOs;
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
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mutantes.IntegrationTest
{
    // [TestClass]
    public class MutantControllerTest
    {



        private MutantsDbContext _context;
    
        private IStatsRepository _statsRepository;
        private IStatsService _statsService;
        private IDnaSaverService _dnaSaverService;
        private IDnaAnalyzedRepository _dnaAnalyzedRepository;
        private MutantController _mutantController;
        private IDnaAnalyzerService _dnaAnalyzerService;
        private MatrixUtilities _matrixUtitilities;
        private Mock<ICacheService> _cacheService;
        public MutantControllerTest()
        {
            _cacheService = new Mock<ICacheService>();
            _cacheService.Setup(x => x.CacheResponseAsync("key", "value")).Returns(Task.FromResult(default(string)));
            _cacheService.Setup(x => x.GetCachedResponseAsync("key")).Returns(Task.FromResult(default(string)));

        }

        [SetUp]
        public void Setup()
        {
            var dbContextOptions = new DbContextOptionsBuilder<MutantsDbContext>().UseInMemoryDatabase("Test");
            _context = new MutantsDbContext(dbContextOptions.Options);
            _context.Database.EnsureCreated();

            _matrixUtitilities = new MatrixUtilities();
            _dnaAnalyzedRepository = new DnaAnalyzedRepository(_context);
            _statsRepository = new StatsRepository(_context);
            _statsService = new StatsService(_statsRepository);
            _dnaSaverService = new DnaSaverService(_dnaAnalyzedRepository, _statsRepository);
            _dnaAnalyzerService = new DnaAnalyzerService(_matrixUtitilities, _dnaSaverService, _cacheService.Object);


            _mutantController = new MutantController(_dnaAnalyzerService);



        }


        [Test]
        public async Task Test001AnalizarUnAdnMutanteRetornaHttpStatus200()
        {

            ObjectResult objectResult = await makePostRequest(DnaListGenerator.DnaMutantMatrix());

            Assert.AreEqual(objectResult.StatusCode, StatusCodes.Status200OK);
            
            var resultObj =  (MessageReponse)objectResult.Value;

            Assert.AreEqual(StatusCodes.Status200OK, resultObj.status);
          

        }
        [Test]
        public async Task Test002AnalizarUnAdnHumanoRetornaHttpStatus403()
        {

            ObjectResult objectResult = await makePostRequest(DnaListGenerator.DnaHumanMatriz());


            Assert.AreEqual(objectResult.StatusCode, StatusCodes.Status403Forbidden);

            var resultObj = (MessageReponse)objectResult.Value;

            Assert.AreEqual(StatusCodes.Status403Forbidden, resultObj.status);



        }

        [Test]
        public async Task Test003AnalizarMatrizConCharInvalidoRetornaHttpStatus400()
        {
            ObjectResult objectResult = await makePostRequest(DnaListGenerator.InvalidCharMatrix());


            Assert.AreEqual(objectResult.StatusCode, StatusCodes.Status400BadRequest);

            var resultObj = (MessageReponse)objectResult.Value;

            Assert.AreEqual(StatusCodes.Status400BadRequest, resultObj.status);

        }

        [Test]
        public async Task Test004RecibirUnParametroNuloRetornaUnsupportedMediaType()
        {
            ObjectResult objectResult = await makePostRequest(null);


            Assert.AreEqual(objectResult.StatusCode, StatusCodes.Status415UnsupportedMediaType);

            var resultObj = (MessageReponse)objectResult.Value;

            Assert.AreEqual(StatusCodes.Status415UnsupportedMediaType, resultObj.status);
        }

        [Test]
        public async Task Test005RecibirMatrixVaciaRetornaBadRequest()
        {
            ObjectResult objectResult = await makePostRequest(DnaListGenerator.EmptyMatrix());


            Assert.AreEqual(objectResult.StatusCode, StatusCodes.Status400BadRequest);

            var resultObj = (MessageReponse)objectResult.Value;

            Assert.AreEqual(StatusCodes.Status400BadRequest, resultObj.status);

        }





        private async Task<ObjectResult> makePostRequest(string[] dna)
        {
            DnaDto dnaDto = new DnaDto
            {
                dna = dna,
            };

            ObjectResult objectResult = (ObjectResult)await _mutantController.Post(dnaDto);
            return objectResult;
        }








    }
}
