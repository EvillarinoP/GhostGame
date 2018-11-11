using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhostGame.Models
{
    /// <summary>
    /// Represents a human player
    /// </summary>
    public class HumanPlayer : GhostPlayer
    {
        // This is a very simple class, because the human play is just returning the word that has been updated in the fron-end
        private string _name = "Human Player";

        // Public accessor for class variables
        public GhostDictionary Dictionary { get; set; } // In the ccase of the human player, the dictionary is empty because it is on the human player's mind

        public HumanPlayer() {}

        public string Name { get { return _name; } set { _name = value; } }

        public string play(string wordInPlay)
        {
            return wordInPlay;
        }
    }
}