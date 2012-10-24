using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ReplayParser.Parser;
using ReplayParser.Entities;
using ReplayParser.Interfaces;

namespace ReplayParser.Loader
{
    public class ReplayLoader
    {

        public ReplayLoader()
        { }

        public static IReplay LoadReplay(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException("Filen '" + filename + "' blev ikke fundet.");

            

            return LoadReplay(new BinaryReader(new FileStream(filename, FileMode.Open)));
        }
        public static IReplay LoadReplay(BinaryReader reader)
        {
            Unpacker unpacker = new Unpacker();
            UnpackResult result = unpacker.Unpack(reader);

            return ParseReplay(result);
        }

        private static IReplay ParseReplay(UnpackResult result) 
        {

            HeaderParser headerParser = new HeaderParser(result.Header);
            Header header = headerParser.ParseHeader();

            ActionParser actionsParser = new ActionParser(result.Actions, header.Players.ToList<IPlayer>());
            List<IAction> actions = actionsParser.ParseActions();

            IReplay replay = new Replay(header, actions);

            return replay;
        }
    }
}
