using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Formula1_new.Models
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public int DriverPoints { get; set; }
    }
}