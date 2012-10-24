using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplayParser.Interfaces;
using ReplayParser.Analyzers;

namespace ReplayParser.Entities
{
    public class Replay : IReplay
    {
        private IEnumerable<IAction> _actions = new List<IAction>();
        public IEnumerable<IAction> Actions
        {
            get
            {
                foreach (var a in _actions)
                    yield return a;
            }
        }

        private IEnumerable<IPlayer> _players = new List<IPlayer>();
        public IEnumerable<IPlayer> Players
        {
            get
            {
                foreach (var p in _players)
                    yield return p;
            }
        }

        public string GameCreator { get; private set; }
        public EngineType EngineType { get; private set; }
        public int FrameCount { get; private set; }
        public GameType GameType { get; private set; }
        public string GameName { get; private set; }
        public DateTime Timestamp { get; private set; }

        private IPlayer winner;
        private bool winnerChecked;
        public IPlayer Winner
        {
            get
            {
                // lazy load the winner
                if (winnerChecked == false)
                {
                    winner = WinAnalyzer.ExtractWinner(this);
                    winnerChecked = true;
                }

                return winner;
            }
        }

        public Replay(Header header, IList<IAction> actions)
        {

            this._actions = actions;
            this.GameCreator = header.GameCreator;
            this.EngineType = header.EngineType;
            this.FrameCount = header.FrameCount;
            this.GameType = header.GameType;
            this.GameName = header.GameName;
            this._players = header.Players;
            this.Timestamp = header.TimeStamp;
            this.winner = null;


            // Not implemented yet
            // this.replayMap = new ReplayMap(header.getMapName(), header.getMapWidth(), header.getMapHeight());
        }
    }
}
