using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ksu.Gdc.Api.Web.Models
{
    public class ErrorResponse
    {
        public List<string> ErrorMessages { get; private set; } = new List<string>();

        public ErrorResponse(string errorMessage)
        {
            ErrorMessages.Add(errorMessage);
        }

        public ErrorResponse(List<string> errorMessages)
        {
            ErrorMessages.AddRange(errorMessages);
        }

        public ErrorResponse(Exception ex)
        {
            ErrorMessages.Add(ex.Message);
        }

        public ErrorResponse(ModelStateDictionary modelState)
        {
            var errorList = modelState
                .Where(ms => ms.Value.Errors.Count > 0)
                .Select(ms => ms.Value.Errors[0].ErrorMessage)
                .ToList();
            ErrorMessages.AddRange(errorList);
        }

        public void AddError(string errorMessage)
        {
            ErrorMessages.Add(errorMessage);
        }
    }
}
