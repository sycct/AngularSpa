using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AngularSpa.ViewModels;
using AspNet.Security.OAuth.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaDatasource.Interfaces;

namespace AngularSpa.Api
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
    public class ProfilesController : Controller
    {
        ISpaDatasource _SpaDatasource = null;

        public ProfilesController(ISpaDatasource ds)
        {
            _SpaDatasource = ds;
        }

        [HttpGet]
        public ProfileViewModel GetProfile()
        {
            ProfileViewModel profile = null;

            try
            {
                Claim subClaim = this.User.Claims.Where(o => o.Type == "sub").FirstOrDefault();
                if(subClaim != null)
                {
                    int id = int.Parse(subClaim.Value);

                    _SpaDatasource.Open();
                    SpaDatasource.Entitites.User u = _SpaDatasource.FindUserById(id);

                    if (u != null)
                    {
                        profile = new ProfileViewModel
                        {
                            Id = u.Id,
                            FullName = u.FullName,
                            Login = u.Login,
                            Status = u.Status,
                            EmailAddress = u.Email,
                            Country = u.Country,
                            City = u.City,
                            Zip = u.Zip,
                            Address = u.Address
                        };
                    }
                }
            }
            finally
            {
                _SpaDatasource.Close();
            }

            return profile;
        }
    }
}