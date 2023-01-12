using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mco.Api.Controllers
{
    public class InputItem
    {
        [Required]
        public string ItemId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }

        [Required]
        public int Amount { get; set; }
    }
}