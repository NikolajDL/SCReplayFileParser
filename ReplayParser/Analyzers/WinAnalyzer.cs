using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplayParser.Interfaces;
using ReplayParser.Entities;
using ReplayParser.Actions;

namespace ReplayParser.Analyzers
{
    public static class WinAnalyzer
    {
        public static IPlayer ExtractWinner(IReplay replay) 
        {
            IList<IPlayer> players = new List<IPlayer>(replay.Players);

            foreach (IAction action in replay.Actions) {

                if (action.ActionType == ActionType.LeaveGame) {

                    LeaveGameAction a = (LeaveGameAction)action;
                    switch (a.LeaveGameType) {
                        case LeaveGameType.Dropped:
                            return null;

                        case LeaveGameType.Quit:
                            players.Remove(a.Player);
                            break;
                        default:
                            throw new InvalidOperationException();
                    }
                }
            }

            if (players.Count() == 1) {
                return players[0];
            }

            return null;
        }
    }
}
