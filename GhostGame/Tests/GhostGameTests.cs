﻿using GhostGame.Controllers;
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
        private string _path = @"C:\GhostGame\ghostGameDict.txt";
        private string _customPath = @"C:\GhostGame\customDict.txt";

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

        [Test]
        public void ObtainsMaximumLength()
        {
            string word = "rand";

            LetterNode node = new LetterNode(word);
            int length = node.maximumLength();

            Assert.AreEqual(length, 4);
        }

        [Test]
        public void GetsMaximumLengthNode()
        {

            GhostDictionary dict = new GhostDictionary(_customPath);
            ComputerPlayer player = new ComputerPlayer();
            player.Dictionary = dict;

            LetterNode sub1 = dict.Words["r"];
            LetterNode sub2 = sub1.Children["a"];
            LetterNode sub3 = sub2.Children["n"];
            LetterNode sub4 = sub3.Children["d"];

            LetterNode longest = player.longestWord(sub4);

            Assert.AreEqual(longest.Letter, "i");

        }

        [Test]
        public void TestsIsFullWord()
        {
            GhostDictionary dict = new GhostDictionary(_customPath);

            bool isFullWord = dict.isFullWord("random");
            bool isNotFullWord = dict.isFullWord("rand");

            Assert.IsTrue(isFullWord);
            Assert.IsFalse(isNotFullWord);
        }

        [Test]
        public void TestsIsLeafNode()
        {
            string word = "rand";

            LetterNode node = new LetterNode(word);
            LetterNode sub1 = node.Children["a"];
            LetterNode sub2 = sub1.Children["n"];
            LetterNode sub3 = sub2.Children["d"];
            bool isLeaf = sub3.isLeafNode();
            bool isNotLeaf = sub1.isLeafNode();

            Assert.IsTrue(isLeaf);
            Assert.IsFalse(isNotLeaf);
        }

        [Test]
        public void TestsLeafNodeCount()
        {
            GhostDictionary dict = new GhostDictionary(_customPath);
            LetterNode node = dict.Words["r"];
            int count = node.leafNodeCount();

            Assert.AreEqual(count, 3);
        }

        [Test]
        public void TestsChild()
        {
            GhostDictionary dict = new GhostDictionary(_customPath);
            LetterNode node = dict.Words["r"];
            LetterNode child = node.child("a");

            Assert.AreEqual(child.Letter, "a");
        }

        [Test]
        public void TestsForcedWin()
        {
            GhostDictionary dict = new GhostDictionary(_customPath);
            ComputerPlayer cp = new ComputerPlayer();
            LetterNode node = dict.Words["r"];

            LetterNode sub1 = dict.Words["r"];
            LetterNode sub2 = sub1.Children["a"];
            LetterNode sub3 = sub2.Children["n"];
            LetterNode sub4 = sub3.Children["d"];

            LetterNode winNode1 = cp.forcedWin(node);
            LetterNode winNode2 = cp.forcedWin(sub4);

            Assert.AreEqual(winNode1, null);
            Assert.AreEqual(winNode2.Letter, "o");
        }
    }
}