using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhostGame.Models
{

    /// <summary>
    /// Represents a node in the GhostDictionary tree-like structure
    /// </summary>
    public class LetterNode
    {
        private string _letter; // This will be the key of the node
        private Dictionary<string, LetterNode> _nodes = new Dictionary<string, LetterNode>();

        // Public accessors of the class variables
        public string Letter { get { return _letter; } }
        public Dictionary<string, LetterNode> Children { get { return _nodes; } }

        /// <summary>
        /// Represents a node in the GhostDictionary tree
        /// </summary>
        /// <param name="word"></param>
        public LetterNode(string word)
        {
            if (word.Length > 0)
                _letter = word[0].ToString();

            if (word.Length > 1)
                _nodes.Add(word[1].ToString(), new LetterNode(word.Substring(1)));
        }

        /// <summary>
        /// Adds a new LetterNode to the tree, based on the word
        /// </summary>
        /// <param name="word"></param>
        public void addWord(string word)
        {
            if (word[0].ToString() == _letter)
            {
                if (_nodes.Count != 0)
                {
                    if (word.Length == 1)
                        _nodes.Clear();
                    else
                    {

                        if (!_nodes.ContainsKey(word[1].ToString())) // If no node is found with the next letter, creates a new one
                            _nodes.Add(word[1].ToString(), new LetterNode(word.Substring(1)));
                        else
                        {
                            LetterNode nextNode = _nodes[word[1].ToString()]; // If the next letter is found as a node, it selects it and calls recurively with the rest of the word
                            nextNode.addWord(word.Substring(1));
                        }
                    }
                }
            }
            else
                throw new ArgumentException("Invalid first letter.");
        }

        /// <summary>
        /// Obtains the maximum length of nodes ahead
        /// </summary>
        /// <returns></returns>
        public int maximumLength()
        {
            int length = 0;
            int maximumLength = 0;

            foreach (LetterNode node in _nodes.Values)
            {
                length = node.maximumLength();
                if (length > maximumLength)
                    maximumLength = length;
            }
            return maximumLength + 1;
        }

        /// <summary>
        /// Checks if the node is the last of the path
        /// </summary>
        /// <returns></returns>
        public bool isLeafNode()
        {
            return _nodes.Count == 0;
        }

        /// <summary>
        /// Returns the number of leaves or words that derive from this node
        /// </summary>
        /// <returns></returns>
        public int leafNodeCount()
        {
            int leaves = 0;
            if (isLeafNode())
                leaves = 1;
            else
                foreach (LetterNode node in _nodes.Values)
                    leaves += node.leafNodeCount();

            return leaves;
        }

        /// <summary>
        /// Returns the child that starts with <paramref name="letter"/>
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        public LetterNode child(string letter)
        {
            try
            {
                return _nodes[letter];
            }
            catch (Exception)
            { return null; }
        }
    }
}