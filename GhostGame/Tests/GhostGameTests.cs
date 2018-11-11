using GhostGame.Controllers;
using GhostGame.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;

namespace GhostGame.Tests
{
    [TestFixture]
    public class GhostGameTests
    {
        // Arrange

        // Act

        // Assert
        private string _path = @"C:\GhostGame\ghostGameDict.txt";

        [Test]
        public void GhostGame_LoadsDictionary()
        {
            // Arrange
            string dictionaryPath = @"C:\GhostGame\ghostGameDict.txt";
            // Act
            GhostDictionary dict = new GhostDictionary(dictionaryPath);
            // Assert
            Assert.AreNotEqual(dict.size(), 0);
        }

        [Test]
        public void PlaysLetter()
        {
            // Arrange
            string letter = "a";
            string dictionaryPath = @"C:\GhostGame\ghostGameDict.txt";

            // Act
            Ghost_Game game = new Ghost_Game(dictionaryPath,new HumanPlayer(), new ComputerPlayer());
            game.play(letter);

            // Assert
            Assert.AreEqual(game.WordInPlay.Length, 2);
        }

        [Test]
        public void CreatesLetterNode()
        {
            string word = "rand";

            LetterNode node = new LetterNode(word);

            Assert.AreEqual("r", node.Letter);
        }

        //public void WordAdded()
        //{
        //    // Arrange
        //    string word = "rand";
            

        //    // Act
        //    GhostDictionary dict = new GhostDictionary(_path);
        //    dict.addWord(word);

        //    // Assert
        //    Assert.Contains(KeyValuePair<string, LetterNode>(){ })
        //}

        //[Test]
        //    public void ()
        //    {
        //        // Arrange

        //        // Act

        //        // Assert
        //    }
    }
}