using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

using Mutantes.Core.Services;
using Mutantes.Infraestructura.Data;
using Mutantes.Infraestructura.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutantes.IntegrationTest
{
        
    class RepositoriesTests
    {
        private MutantsDbContext _context;
        private Infraestructura.Repositories.StatsRepository _statsRepository;
        private DnaAnalyzedRepository _dnaAnalyzed;

        [SetUp]
        public void Setup()
        {
            var dbContextOptions = new DbContextOptionsBuilder<MutantsDbContext>().UseInMemoryDatabase("TestRepositories");
            _context = new MutantsDbContext(dbContextOptions.Options);
            _context.Database.EnsureCreated();
            

            _statsRepository = new Infraestructura.Repositories.StatsRepository(_context);
            _dnaAnalyzed = new DnaAnalyzedRepository(_context);
        }


        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task Test001CalcularStatsSinRegistrosDevuelveTodosLosValoresCero()
        {
          
            var result = await  _statsRepository.GetStats();
            Assert.AreEqual(result.HumansFound, 0);
            Assert.AreEqual(result.MutantsFound, 0);
           
        }

        [Test]
               
        public async Task Test002UpdateoLaCantidadDeMutanteEncontradosYLosStantsDevuelvenCantMutantes1()
        {
            var result = await _statsRepository.GetStats();        
            Assert.AreEqual(result.MutantsFound, 0);


            DnaAnalyzed dnaAnalizded = new DnaAnalyzed()
            {
                Dna = "DNATEST_MUTANTE",
                IsMutant = true,
                DateAnalyzed = DateTime.Now
            };
             
            await _statsRepository.UpdateStatsAsync(dnaAnalizded);

            var resultAfterUpdate = await _statsRepository.GetStats();
            Assert.AreEqual(1, resultAfterUpdate.MutantsFound);

        }

        [Test]
        public async Task Test003UpdateoLaCantidadDeHumanosEncontradosYLosStatsDevuelveCantHumanos1()
        {
            var result = await _statsRepository.GetStats();

            Assert.AreEqual(result.HumansFound, 0);

            DnaAnalyzed dnaAnalizded = new DnaAnalyzed()
            {
                Dna = "DNATES_THUMANO",
                IsMutant = false,
                DateAnalyzed = DateTime.Now
            };

            await _statsRepository.UpdateStatsAsync(dnaAnalizded);

            var resultAfterUpdate = await _statsRepository.GetStats();
            Assert.AreEqual(1, resultAfterUpdate.HumansFound);

        }

        [Test]

        public async Task Test004SiCreoAsyncGuardaCorrectamente()
        {
            DnaAnalyzed dnaAnalyzed = new DnaAnalyzed
            {
                Dna = "DNA_TEST",
                IsMutant = true,
                DateAnalyzed = DateTime.Now
            };
            await _dnaAnalyzed.CreateAsync(dnaAnalyzed);

            var dnaGuardado =  _context.DnaAnalyzed.Where(dna => dna.Dna.Equals(dnaAnalyzed.Dna)).Count();

            Assert.AreEqual(1, dnaGuardado);
        }








    }
}
