using System.Web.Http;
using System.Web.Mvc;

namespace EPiServer.Reference.Commerce.Site.WebApi
{
    public abstract class BaseApiController : ApiController
    {
        protected void AddModelStateError(string error)
        {
            ModelState.AddModelError("ServerError",error);
        }

        protected IHttpActionResult ErrorMessageResult(string errorMessage)
        {
            AddModelStateError(errorMessage);
            return Ok(new
            {
                HasError = true,
                ModelState
            });
        }
    }
}