using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhostGame.Models
{
    /// <summary>
    /// Represents a player of the game
    /// </summary>
    public interface GhostPlayer
    {
        // Each player must have a name, a play method or "move", and a Dictionary.
        string Name { get; set; }
        string play(string wordInPlay);
        GhostDictionary Dictionary { get; set; }
    }
}