﻿using RSApp.Presentation.WebApi.Middleware;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace RSApp.Presentation.WebApi.Extensions
{
  public static class AppExtensions
  {
    public static void UseSwaggerExtension(this IApplicationBuilder app)
    {
      app.UseSwagger();
      app.UseSwaggerUI(options =>
      {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API");
        options.DefaultModelRendering(ModelRendering.Model);
      });
    }

    public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
    {
      app.UseMiddleware<ErrorHandlerMiddleware>();
    }
  }
}
