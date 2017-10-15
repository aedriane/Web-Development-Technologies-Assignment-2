using System;
using System.Collections.Generic;

namespace Assignment2.Models
{
    public partial class SessionTime
    {
        public SessionTime()
        {
            CineplexMovie = new HashSet<CineplexMovie>();
        }

        public int SessionTimeId { get; set; }
        public DateTime SessionTime1 { get; set; }
        public DateTime SessionTime2 { get; set; }

        public virtual ICollection<CineplexMovie> CineplexMovie { get; set; }
    }
}
