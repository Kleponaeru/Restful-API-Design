using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyApi.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
// public override void OnActionExecuting(ActionExecutingContext context)
//         {
//             if (!context.ModelState.IsValid)
//             {
//                 var errors = new Dictionary<string, IEnumerable<string>>();

//                 foreach (var (key, value) in context.ModelState)
//                 {
//                     var errorMessages = value.Errors.Select(e => e.ErrorMessage);
//                     errors.Add(key, errorMessages);
//                 }

//                 var errorResponse = new
//                 {
//                     Message = "The request is invalid.",
//                     ModelState = errors
//                 };
//                 var json = JsonConvert.SerializeObject(errorResponse);

//                 context.Result = new ContentResult
//                 {
//                     Content = json,
//                     ContentType = "application/json",
//                     StatusCode = (int)HttpStatusCode.BadRequest
//                 };
//             }

//         }
