using EmployeeRecords.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace EmployeeRecords.API.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter, IDisposable
    {
        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }

        public void OnException(ExceptionContext context)
        {
			if (context == null)
			{
				return;
			}

			var data = new Dictionary<string, string>();
			var statusCode = HttpStatusCode.InternalServerError;

			var ex = context.Exception;

			TypeSwitch.Do(
					ex,
					TypeSwitch.Case<ArgumentException>(() => { statusCode = HttpStatusCode.BadRequest; data.Add("ExceptionType", "ArgumentException"); }),
					TypeSwitch.Case<ArgumentNullException>(() => { statusCode = HttpStatusCode.BadRequest; data.Add("ExceptionType", "ArgumentNullException"); }),					
					TypeSwitch.Case<Exception>(() => { statusCode = HttpStatusCode.BadRequest; data.Add("ExceptionType", "Exception"); })
			);

			var request = context.HttpContext.Request;

			var response = context.HttpContext.Response;
			response.StatusCode = (int)statusCode;
			response.ContentType = "application/json";

			response.WriteAsync(JsonConvert.SerializeObject(ex));
		}
    }
}
