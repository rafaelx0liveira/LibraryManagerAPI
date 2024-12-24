using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryManagerAPI.Presentation.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(ms => ms.Value.Errors.Any())
                    .Select(ms => new
                    {
                        Field = ms.Key,
                        Messages = ms.Value.Errors.Select(e => e.ErrorMessage)
                    });

                context.Result = new BadRequestObjectResult(new
                {
                    Message = "Validation failed",
                    Errors = errors
                });
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
