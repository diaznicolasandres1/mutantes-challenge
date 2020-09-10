using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mutantes.API;
using Mutantes.Core.Entities;
using Mutantes.Core.Utilities;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mutantes.IntegrationTest
{
    [TestClass]
    public class MutantControllerTest
    {
        private readonly HttpClient _client;
        public MutantControllerTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
        }

        [TestMethod]
        public async Task Test001AnalizarUnAdnMutanteRetornaHttpStatus200Async()
        {

            var apiResponse = await makeRequest(DnaListGenerator.DnaMutantMatrix());

            Assert.AreEqual(apiResponse.StatusCode, System.Net.HttpStatusCode.OK);
        }


        [TestMethod]
        public async Task Test002AnalizarUnAdnHumanoRetornaHttpStatus403()
        {

            var apiResponse = await makeRequest(DnaListGenerator.DnaHumanMatriz());

            Assert.AreEqual(apiResponse.StatusCode, System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod]

        public async Task Test003AnalizarMatrizConCharInvalidoRetornaHttpStatus400()
        {

            var apiResponse = await makeRequest(DnaListGenerator.InvalidCharMatrix());         

           
            Assert.AreEqual(apiResponse.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }


        [TestMethod]
        public async Task Test004RecibirUnParametroNuloRetornaUnsupportedMediaType()
        {
            var apiResponse =  await _client.PostAsync("api/mutant", null);
            Assert.AreEqual(apiResponse.StatusCode, System.Net.HttpStatusCode.UnsupportedMediaType);

        }

        [TestMethod]
        public async Task Test005RecibirMatrixVaciaRetornaBadRequest()
        {
            
            var apiResponse = await makeRequest(DnaListGenerator.EmptyMatrix());

            Assert.AreEqual(apiResponse.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }







        private async Task<HttpResponseMessage> makeRequest(string[] dnaList)
        {
            var dnaEntitie= new DnaEntitie()
            {
                Dna = dnaList
            };
            var serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(dnaEntitie);
            var requestString = new StringContent(serializedObject, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/mutant", requestString);

            return response;
        }



    }
}
