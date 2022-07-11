using Med.Shared.Dtos;
using Med.Shared.HttpStatusCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Med.Shared.Helpers
{
    public static class CustomProblemDetails
    {
        public static Response<string> ErrorResponse { get; set; }
        public static List<string> Errors { get; set; } = new List<string>();
        public static IActionResult MakeValidationResponse(ActionContext context)
        {
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = CStatusCodes.Status1017ValidationProblem
            };
            foreach (var keyModelStatePair in context.ModelState)
            {
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                //    if (errors.Count == 1)
                //    {
                //        var errorMessage = GetErrorMessage(errors[0]);
                //        Errors.Add(errorMessage);

                //    }
                //    else
                //    {
                        var errorMessages = new string[errors.Count];
                        for (var i = 0; i < errors.Count; i++)
                        {
                            errorMessages[i] = GetErrorMessage(errors[i]);
                            Errors.Add(errorMessages[i]);

                        }
                //    }
                }
            }

            ErrorResponse = new Response<string>();
            ErrorResponse.Errors = Errors;
            ErrorResponse.StatusCode = CStatusCodes.Status1017ValidationProblem;
            ErrorResponse.IsSuccessful = false;


            var result = new BadRequestObjectResult(ErrorResponse);

            result.ContentTypes.Add("application/problem+json");
            Errors = new List<string>();

            return result;
        }
        static string GetErrorMessage(ModelError error)
        {
            return string.IsNullOrEmpty(error.ErrorMessage) ?
            "The input was not valid." :
            error.ErrorMessage;
        }
    }
}
