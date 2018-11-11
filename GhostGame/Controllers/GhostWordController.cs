using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GhostGame.Models;
using System.Threading;
using System.Web.Hosting;
using System.Text.RegularExpressions;

namespace GhostGame.Controllers
{
    public class GhostWordController : ApiController
    {
        string ghostWord = "";
        public static Ghost_Game ghostGame; // Only one game will be run at once

        [HttpGet]
        public List<string> PostLetter(string letter)
        {
            // Executes both the human and the computer turns
            ghostGame.play(letter);

            // After the computer has played, the updated word is returned
            ghostWord = ghostGame.WordInPlay;

            if (ghostGame.GameOver)
                return new List<string> { ghostWord, "GAME OVER",ghostGame.ResultMsg};
            else
                return new List<string> { ghostWord, "",""};
        }

        [HttpGet]
        public HttpResponseMessage NewGame()
        {
            try
            {
                // Creates a new game, with a human and a computer player, and extracts the dictionary from the local resource ghostGameDict.txt
                ghostGame = new Ghost_Game(HostingEnvironment.MapPath(@"~/App_Data/ghostGameDict.txt"), new HumanPlayer(), new ComputerPlayer());
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}