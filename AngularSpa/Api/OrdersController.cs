using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngularSpa.ViewModels;
using SpaDatasource.Interfaces;
using SpaDatasource.Entitites;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularSpa.Api
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        ISpaDatasource _SpaDatasource = null;

        public OrdersController(ISpaDatasource ds)
        {
            _SpaDatasource = ds;
        }

        [HttpGet]
        public OrderInfo Get()
        {
            OrderInfo order = null;

            try
            {
                _SpaDatasource.Open();
                Order o = _SpaDatasource.Orders().LastOrDefault();

                if(o != null)
                {
                    order = new OrderInfo
                    {
                        Id = o.Id,
                        Time = o.Time,
                        UserId = o.UserId,
                        EmailAddress = o.EmailAddress,
                        Description = o.OrderDescription,
                        Currency = o.Currency
                    };
                }
            }
            finally
            {
                _SpaDatasource.Close();
            }

            return order;
        }

        [HttpPost]
        public OrderInfo Post([FromBody]OrderInfo order)
        {
            try
            {
                Order o = new Order
                {
                    Time = DateTime.Now,
                    UserId = order.UserId,
                    EmailAddress = order.EmailAddress,
                    OrderDescription = order.Description,
                    Currency = order.Currency
                };

                _SpaDatasource.Open();
                _SpaDatasource.InsertOrder(o);

                order.Id = o.Id;
            }
            finally
            {
                _SpaDatasource.Close();
            }

            return order;
        }
    }
}
