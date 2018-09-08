using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Demo.Core.Exceptions;
using Demo.Core.Services;
using Demo.Functions.Framework;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Demo.Functions
{
    public static class HeroesGet
    {
        [FunctionName("Heroes_Get")]
        public static async Task<HttpResponseMessage> Run
        (
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "heroes/{id}")] HttpRequestMessage request,
            string id,
            [Inject] IHeroService heroService,
            ILogger logger
        )
        {
            try
            {
                return request.CreateResponse(HttpStatusCode.OK, await heroService.GetOne(id));
            }
            catch (ArgumentNullException)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (NotFoundException)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}