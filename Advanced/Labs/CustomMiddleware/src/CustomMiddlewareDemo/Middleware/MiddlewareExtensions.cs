using CustomMiddlewareDemo.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{

  /// <summary>
  /// This is a utility container class that provides the simple "Use" syntax
  /// for the middleware in this project
  /// </summary>
  public static class MiddlewareExtensions
  {

    public static IApplicationBuilder UseEnvironmentDisplay(this IApplicationBuilder builder)
    {

      return builder.UseMiddleware<EnvironmentDisplay>();

    }

  }

}
