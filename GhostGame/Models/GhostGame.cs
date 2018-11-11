using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhostGame.Models
{
    /// <summary>
    /// Represents a game instance, that finishes only when there is a winner.
    /// </summary>
    public class Ghost_Game
    {
        private GhostDictionary _dictionary = null;
        private GhostPlayer[] _players = new GhostPlayer[2];
        private int _currentPlayerIndex = 0;
        private string _wordInPlay = "";
        private bool _gameOver = false;
        private GhostPlayer _winner = null;
        private string _resultMsg = "";
        private string _id;

        // Public accessors to class variables
        public string WordInPlay { get { return _wordInPlay; } set { _wordInPlay = value; } }
        public GhostPlayer[] Players { get { return _players; } set { _players = value; } }
        public bool GameOver { get { return _gameOver; } set { _gameOver = value; } }
        public GhostPlayer Winner { get { return _winner; } set { _winner = value; } }
        public string ResultMsg { get { return _resultMsg; } set { _resultMsg = value; } }
        public string ID { get { return _id; } set { _id = value; } }

        /// <summary>
        /// Creates a new GhostGame instance
        /// </summary>
        /// <param name="fileName">The txt file from where the dictionary should be loaded</param>
        /// <param name="playerOne"></param>
        /// <param name="playerTwo"></param>
        public Ghost_Game( string fileName, GhostPlayer playerOne, GhostPlayer playerTwo)
        {
            _id = ID;
            _dictionary = new GhostDictionary(fileName); // Loads the dictionary from the txt file.
            playerTwo.Dictionary = _dictionary; // Copies the dictionary to the computer player class, so it doesn't have to load it again

            if (_dictionary.size() == 0)
                throw new Exception("Unable to load dictionary.");

            _players[0] = playerOne;
            _players[1] = playerTwo;
            
        }

        /// <summary>
        /// Changes the player index from the _players array.
        /// </summary>
        /// <returns></returns>
        public int switchPlayer()
        {
            return (_currentPlayerIndex = ++_currentPlayerIndex % 2); // Always toggles between 0 and 1
        }


        /// <summary>
        /// Adds a new letter to the word in play and checks if it is a "loser" letter
        /// </summary>
        /// <param name="letter"></param>
        public void addLetter(string letter)
        {
            _wordInPlay += letter;

            if (_dictionary.isFullWord(_wordInPlay) || !_dictionary.isWordStem(_wordInPlay))
            {
                _gameOver = true;
                _winner = _players[switchPlayer()]; // The other player is the winner.

                if (_dictionary.isFullWord(_wordInPlay)) // Depending on the reason why the letter is a loser, the return message will be different
                    _resultMsg = _wordInPlay + " is a complete word. " + _winner.Name + " wins the game!";
                else if(!_dictionary.isWordStem(_wordInPlay))
                    _resultMsg = _wordInPlay + " is not a word. " + _winner.Name + " wins the game!";
            }
        }

        /// <summary>
        /// Executes the play from the human and the computer.
        /// </summary>
        /// <param name="newLetter"></param>
        public void play(string newLetter)
        {
            // The human always plays first
            addLetter(_players[_currentPlayerIndex].play(newLetter));
            switchPlayer();

            if (_gameOver) // If the human has lost, the computer must not play anymore
                return;

            addLetter(_players[_currentPlayerIndex].play(_wordInPlay));
            switchPlayer();
        }

        
    }
}