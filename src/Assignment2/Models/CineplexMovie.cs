using System;
using System.Collections.Generic;

namespace Assignment2.Models
{
    public partial class CineplexMovie
    {
        public int CineplexId { get; set; }
        public int MovieId { get; set; }
        public int? SessionId { get; set; }

        public virtual Cineplex Cineplex { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual SessionTime Session { get; set; }
    }
}
