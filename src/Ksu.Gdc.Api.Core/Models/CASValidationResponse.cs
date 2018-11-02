using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using Ksu.Gdc.Api.Core.Configurations;

namespace Ksu.Gdc.Api.Core.Models
{
    public class CASValidationResponse
    {
        public CASServiceResponse ServiceResponse { get; set; }

        public CASValidationResponse(dynamic response)
        {
            ServiceResponse = new CASServiceResponse(response["serviceResponse"]);
        }

        public bool Validated => ServiceResponse.AuthenticationSuccess != null;
    }

    public class CASServiceResponse
    {
        public CASAuthenticationSuccess AuthenticationSuccess { get; set; }

        public CASAuthenticationFailure AuthenticationFailure { get; set; }

        public CASServiceResponse(dynamic response)
        {
            var success = response["authenticationSuccess"];
            if (success != null)
            {
                AuthenticationSuccess = new CASAuthenticationSuccess(success);
            }
            else
            {
                AuthenticationFailure = new CASAuthenticationFailure(response["authenticationFailure"]);
            }
        }
    }

    public class CASAuthenticationSuccess
    {
        public CASAttributes Attributes { get; set; }

        public CASAuthenticationSuccess(dynamic response)
        {
            Attributes = new CASAttributes(response["attributes"]);
        }
    }

    public class CASAuthenticationFailure
    {
        public string Code { get; set; }

        public string Description { get; set; }

        public CASAuthenticationFailure(dynamic response)
        {
            Code = response["code"];
            Description = response["description"];
        }
    }

    public class CASAttributes
    {
        public List<string> Uid { get; set; }

        public List<int> KsuPersonWildcatId { get; set; }

        public List<string> AuthenticationDate { get; set; }

        public CASAttributes(dynamic response)
        {
            Uid = response["uid"].ToObject<List<string>>();
            KsuPersonWildcatId = response["ksuPersonWildcatId"].ToObject<List<int>>();
            AuthenticationDate = response["authenticationDate"].ToObject<List<string>>();
        }
    }
}
