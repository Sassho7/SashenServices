using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SmartGarage.Controllers.API
{
    internal class ErrorResponse : ModelStateDictionary
    {
        private IEnumerable<string> errorMessages;

        public ErrorResponse(IEnumerable<string> errorMessages)
        {
            this.errorMessages = errorMessages;
        }
    }
}