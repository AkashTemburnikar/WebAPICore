using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Models.Middlewares
{
    public class ExceptionInfo
    {
        public int ExceptionId { get; set; }
        public string ExceptionMessage { get; set; }
    }
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate request;
        public ExceptionMiddleware(RequestDelegate request)
        {
            this.request = request;
        }

        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await request(httpcontext);
            }
            catch(Exception ex)
            {
                await ExceptionLogic(httpcontext, ex);
            }
        }

        private async Task ExceptionLogic(HttpContext ctx, Exception ex)
        {
            ctx.Response.StatusCode = 500;
            string message = ex.Message;
            var exceptionInfo = new ExceptionInfo()
            {
                ExceptionId = ctx.Response.StatusCode,
                ExceptionMessage = message
            };

            var responseMessage = JsonConvert.SerializeObject(exceptionInfo);
            await ctx.Response.WriteAsync(responseMessage);

        }
    }

    public static class ApplyMiddleWare
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
