using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderingApi.Authentication
{
	public class BasicAuthFilter : IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			try
			{
				string authHeader = context.HttpContext.Request.Headers["Authorization"];
				if (authHeader != null)
				{
					var authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);
					if (authHeaderValue.Scheme.Equals(AuthenticationSchemes.Basic.ToString(), StringComparison.OrdinalIgnoreCase))
					{
						var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderValue.Parameter ?? string.Empty)).Split(":", 2);
						if (credentials.Length == 2)
						{
							if (IsAuthorized(context, credentials[0], credentials[1]))
							{
								return;
							}
						}
					}
				}
				context.Result = new UnauthorizedResult();
			}
			catch (FormatException)
			{

				context.Result = new UnauthorizedResult();
			}
		}


		public bool IsAuthorized(AuthorizationFilterContext context, string userName, string password)
		{
			var userService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();
			return userService.IsValid(userName, password);
		}

	}
}
