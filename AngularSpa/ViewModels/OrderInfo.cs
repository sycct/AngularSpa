using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularSpa.ViewModels
{
    public class OrderInfo
    {
        [Display(Description = "Record #")]
        public long Id { get; set; }

        [Display(Description = "Time when order created.", Name ="Time", Prompt="Time")]
        public DateTime Time { get; set; }

        [Required]
        [Display(Description = "User who create order", Name ="User", Prompt="User")]
        public int UserId { get; set; }

        [Display(Description = "Payment Amount (in dollars)", Name = "Amount", Prompt = "Payment Amount")]
        [DataType(DataType.Currency)]
        public decimal Currency { get; set; }

        [Required, RegularExpression(@"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})", ErrorMessage = "Please enter a valid email address.")]
        [EmailAddress]
        [Display(Description = "Email Address", Name = "EmailAddress", ShortName = "Email", Prompt = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Display(Description = "Order Description", Name = "Description")]
        public string Description { get; set; }
    }
}
