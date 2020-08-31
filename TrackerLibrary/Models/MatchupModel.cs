using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary.Models
{
    public class MatchupModel
    {
        /// <summary>
        /// The unique identifier for the matchups.
        /// </summary>
        public int Id { get; set; }
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();
        public TeamModel Winner { get; set; }
        public int MatchupRound { get; set; }
    }
}
