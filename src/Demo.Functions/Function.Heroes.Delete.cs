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
    public static class HeroesDelete
    {
        [FunctionName("Heroes_Delete")]
        public static async Task<HttpResponseMessage> Run
        (
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "heroes/{id}")] HttpRequestMessage request,
            string id,
            [Inject] IHeroService heroService,
            ILogger logger
        )
        {
            try
            {
                await heroService.Delete(id);
                return request.CreateResponse(HttpStatusCode.OK);
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