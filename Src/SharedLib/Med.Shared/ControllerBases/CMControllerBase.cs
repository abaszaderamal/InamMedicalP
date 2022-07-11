using Med.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Med.Shared.ControllerBases
{
    public class CMControllerBase : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
