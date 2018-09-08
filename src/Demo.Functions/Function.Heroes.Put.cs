using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Demo.Core.Exceptions;
using Demo.Core.Models.Heroes.Commands;
using Demo.Core.Services;
using Demo.Functions.Framework;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Demo.Functions
{
    public static class HeroesPut
    {
        [FunctionName("Heroes_Put")]
        public static async Task<HttpResponseMessage> Run
        (
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "heroes/{id}")] ChangeHeroNameCommand command,
            HttpRequestMessage request,
            string id,
            [Inject] IHeroService heroService,
            ILogger logger
        )
        {
            try
            {
                await heroService.ChangeName(id, command);
                return request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is FailedValidationException || ex is InvalidOperationException)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (NotFoundException)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
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