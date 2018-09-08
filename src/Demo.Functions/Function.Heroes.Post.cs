using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Demo.Core.Exceptions;
using Demo.Core.Models.Heroes;
using Demo.Core.Models.Heroes.Commands;
using Demo.Core.Services;
using Demo.Functions.Framework;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Demo.Functions
{
    public static class HeroesPost
    {
        [FunctionName("Heroes_Post")]
        public static async Task<HttpResponseMessage> Run
        (
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "heroes")] CreateHeroCommand command,
            HttpRequestMessage request,
            [Inject] IHeroService heroService,
            ILogger logger
        )
        {
            try
            {
                Hero hero = await heroService.Create(command);
                return request.CreateResponse(HttpStatusCode.Created, hero);
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is FailedValidationException)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (DuplicateException)
            {
                return request.CreateResponse(HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}