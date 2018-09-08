using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Demo.Core.Services;
using Demo.Functions.Framework;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Demo.Functions
{
    public static class HeroesGetAll
    {
        [FunctionName("Heroes_GetAll")]
        public static async Task<HttpResponseMessage> Run
        (
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "heroes")] HttpRequestMessage request,
            [Inject] IHeroService heroService,
            ILogger logger
        )
        {
            try
            {
                return request.CreateResponse(HttpStatusCode.OK, await heroService.GetAll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}