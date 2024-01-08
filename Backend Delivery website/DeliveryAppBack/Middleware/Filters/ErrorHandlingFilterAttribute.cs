using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryAppBack.Middleware.Filters
{
    public class ErrorHandlingFilterAttribute: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            context.Result = new ObjectResult(new { error = "An error" })
            {
                StatusCode = 500
            };
            context.ExceptionHandled=true;
        }
    }
}
