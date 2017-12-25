using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AngularSpa.Controllers
{
    public class PartialController : Controller
    {
        public IActionResult AboutComponent() => PartialView();

        public IActionResult AppComponent() => PartialView();

        public IActionResult ContactComponent() => PartialView();

        public IActionResult IndexComponent() => PartialView();

        public IActionResult LoginComponent() => PartialView();

        public IActionResult RegisterComponent() => PartialView();

        public IActionResult ProfileComponent() => PartialView();

        public IActionResult CartComponent() => PartialView();
    }
}