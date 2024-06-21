using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Formula1_new.Models
{
    public class Racetrack
    {
        [Key]
        public int TrackId { get; set; }
        public string TrackName { get; set; }

        public int TrackLength { get; set; }

        public string Country { get; set; }

      
    }

    public class RaceTrackDTO
    {
        [Key]
        public int TrackId { get; set; }
        public string TrackName { get; set; }

        public int TrackLength { get; set; }

        public string Country { get; set; }
    }
}
