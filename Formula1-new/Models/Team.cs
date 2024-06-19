using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Formula1_new.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        public string TeamName { get; set; }

        public string EngineSupplier { get; set; }

        public string TeamPoints { get; set; }

        [ForeignKey("Driver")]
        public int DriverId { get; set; }
        public virtual Driver Driver { get; set; }
    }

    public class TeamDTO
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }

        public string EngineSupplier { get; set; }

        public string TeamPoints { get; set; }

        public int DriverId { get; set; }
    }
}