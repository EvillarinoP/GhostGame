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
            LetterNode sub1 = node.Children["a"];
            LetterNode sub2 = sub1.Children["n"];
            LetterNode sub3 = sub2.Children["d"];

            Assert.AreEqual("r", node.Letter);
            Assert.AreEqual("a", sub1.Letter);
            Assert.AreEqual("n", sub2.Letter);
            Assert.AreEqual("d", sub3.Letter);
        }

        [Test]
        public void ChecksIsLeaf()
        {
            string word = "rand";

            LetterNode node = new LetterNode(word);
            LetterNode sub1 = node.Children["a"];
            LetterNode sub2 = sub1.Children["n"];
            LetterNode sub3 = sub2.Children["d"];
            bool isLeaf = sub3.isLeafNode();

            Assert.IsTrue(isLeaf);
        }

        [Test]
        public void GetsTerminalNode()
        {
            string word = "rand";

            GhostDictionary dict = new GhostDictionary(_path);
            LetterNode terminal = dict.terminalNode(word);

            Assert.AreEqual(terminal.Letter, "d");
        }

        [Test]
        public void ChecksIsWordStem()
        {
            string word = "ran";

            GhostDictionary dict = new GhostDictionary(_path);
            bool isStem = dict.isWordStem(word);

            Assert.IsTrue(isStem);
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