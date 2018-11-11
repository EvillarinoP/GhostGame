using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace GhostGame.Models
{
    /// <summary>
    /// Represents the back-end player
    /// </summary>
    public class ComputerPlayer : GhostPlayer
    {
        private string _name = "Computer Player";
        private GhostDictionary _dictionary;

        // Public accessors to class variables
        public GhostDictionary Dictionary { get { return _dictionary; }set { _dictionary = value; } }
        public string Name { get { return _name; } set { _name = value; } }

        /// <summary>
        /// Executes the play of the computer
        /// </summary>
        /// <param name="wordInPlay"></param>
        /// <returns></returns>
        public string play(string wordInPlay)
        {
            string nextLetter = "$"; // Random character to return. It will be replace with a valid one, in case there is one

            LetterNode node = _dictionary.terminalNode(wordInPlay);
            if (node != null)
            {
                LetterNode forcedWinNode = forcedWin(node);
                if (forcedWinNode != null)
                    nextLetter = forcedWinNode.Letter; // If a winning child is found...
                else
                    nextLetter = longestWord(node).Letter;  // If there is no winning child, it chooses the node that has the longest path ahead, to extend the game as long as possible
            }
            return nextLetter;
        }

        /// <summary>
        /// This method tries to find a child from the current node that will ensure the computers victory. If it is not found, returns null
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public LetterNode forcedWin(LetterNode node)
        {
            LetterNode winningChild = null;

            foreach (LetterNode child in node.Children.Values)
            {
                if (!child.isLeafNode())
                {
                    winningChild = child;
                    foreach (LetterNode grandChild in child.Children.Values)
                    {
                        if (!grandChild.isLeafNode() || forcedWin(grandChild) != null)
                        {
                            winningChild = null;
                            break;
                        }
                    }
                }
                if (winningChild != null)
                    break;
            }
            return winningChild;
        }

        /// <summary>
        /// Finds the longest path ahead
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public LetterNode longestWord(LetterNode node)
        {
            LetterNode longestChild = null;

            foreach (LetterNode child in node.Children.Values)
            {
                if (longestChild == null || child.maximumLength() > longestChild.maximumLength())
                    longestChild = child;
            }
            return longestChild;
        }
    }
}