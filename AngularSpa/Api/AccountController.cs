using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngularSpa.ViewModels;
using AspNet.Security.OAuth.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaDatasource.Interfaces;

namespace AngularSpa.Api
{
    [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
    public class AccountController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel newUser)
        {
            if (this.ModelState.IsValid)
            {
                IUserManager userManager = this.HttpContext.RequestServices.GetService(typeof(IUserManager)) as IUserManager;
                if (userManager == null)
                    throw new Exception("User Manager object not configured.");

                SpaDatasource.Entitites.User user = new SpaDatasource.Entitites.User
                {
                    Login = newUser.Login,
                    Email = newUser.Email,
                    FullName = newUser.FullName,
                    Status = SpaDatasource.SystemRoles.Customer
                };

                try
                {
                    SpaDatasource.Entitites.User u = await userManager.FindByNameAsync(newUser.Login);
                    if(u != null)
                    {
                        // alrady exists
                        ApiErrorResult apiResultExistingUser = new ApiErrorResult
                        {
                            Messages = new List<string> () { "Пользователь с таким именем уже существует." }
                        };

                        return new JsonResult(apiResultExistingUser) { StatusCode = 409};
                    }

                    await userManager.CreateAsync(user, newUser.Password);

                    return Ok();
                }
                catch(Exception ex)
                {
                    this.ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            ApiErrorResult apiResultError = new ApiErrorResult
            {
                Messages = ModelState.Keys.SelectMany(key => this.ModelState[key].Errors).Select(x => x.ErrorMessage).ToList<string>()
            };

            return new JsonResult(apiResultError) { StatusCode = 500 };
        }
    }
}