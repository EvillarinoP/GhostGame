using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;

namespace GhostGame.Models
{
    /// <summary>
    /// Represents a tree-like dictionary where each node is a letter and a list of subnodes
    /// </summary>
    public class GhostDictionary
    {
        private static readonly int MIN_WORD_LENGHT = 4;
        private Dictionary<string, LetterNode> _words = new Dictionary<string, LetterNode>(); // This is the whole dictionary
        
        /// <summary>
        /// Creates a new instance of a GhostDictionary
        /// </summary>
        /// <param name="path"></param>
        public GhostDictionary(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line = null;
                    while ((line = sr.ReadLine()) != null) // Reads the source file line by line and adds each word
                    {
                        if (Regex.Match(line, @"[^a-zA-Z]").Success) // This makes sure that no stringcters other than letters are used
                            continue;
                        addWord(line);
                    }
                }
            }
            catch (FileNotFoundException)
            {

            }
            catch (IOException ex)
            {

            }
        }

        /// <summary>
        /// Adds a new word to the dictionary, creating all the nodes and subnodes based on the stringacters of the string
        /// </summary>
        /// <param name="word"></param>
        public void addWord(string word) // TODO: PRIVATE!!
        {
            if (word.Length >= MIN_WORD_LENGHT)
            {

                if (!_words.ContainsKey(word[0].ToString())) // If there isn't any node that starts with the first letter, it creates a new one
                    _words.Add(word[0].ToString(), new LetterNode(word));
                else
                {
                    LetterNode node = _words[word[0].ToString()];
                    node.addWord(word);
                }
            }
        }

        /// <summary>
        /// Counts all the nodes in the dictionary
        /// </summary>
        /// <returns></returns>
        public int size()
        {
            int count = 0;
            foreach (LetterNode node in _words.Values)
                count += node.leafNodeCount();
            return count;
        }

        /// <summary>
        /// Obtains the last node (letter) of the current word
        /// </summary>
        /// <param name="thisWord"></param>
        /// <returns></returns>
        public LetterNode terminalNode(string thisWord)
        {
            LetterNode startNode = _words[thisWord[0].ToString()];
            LetterNode node = startNode;

            if (startNode != null)
            {
                for (int i = 1; i < thisWord.Length; i++)
                {
                    node = node.child(thisWord[i].ToString());
                    if (node == null)
                        break;
                }
            }

            return node;
        }

        /// <summary>
        /// Checks if the word is completed and there is no other longer word that can be constructed
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool isFullWord(string word)
        {
            bool isFullWord = false;
            LetterNode node = terminalNode(word);
            if (node != null && node.isLeafNode())
                isFullWord = true;

            return isFullWord;
        }

        /// <summary>
        /// Checks that the word is a valid prefix or stem to a longer word
        /// </summary>
        /// <param name="stem"></param>
        /// <returns></returns>
        public bool isWordStem(string stem)
        {
            bool isWordStem = false;
            LetterNode node = terminalNode(stem);
            if (node != null)
                isWordStem = true;

            return isWordStem;
        }
    }
}