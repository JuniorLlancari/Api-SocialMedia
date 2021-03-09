using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using SocialMedia.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SocialMedia.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        //Filtro tenemos que registrar en el midleware
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(BusinessException))
            {
                var exception = (BusinessException)context.Exception;
                var validation = new
                {
                    status = 400,
                    Title = "Bad Request",
                    Detail = exception.Message
                };
                var json = new
                {
                    errors = new[] { validation }
                };
                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            }
        }
    }
}
