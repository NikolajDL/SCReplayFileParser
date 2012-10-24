using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplayParser.Loader;
using ReplayParser.Interfaces;
using ReplayParser.Actions;
using System.Diagnostics;

namespace ReplayParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //var replay = ReplayLoader.LoadReplay("0022_PvT_Vanko_buralzzan.rep(281).rep");

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var replay = ReplayLoader.LoadReplay("1-bD.FlyKick vs FR.H.rep(598).rep");
            sw.Stop();

            Console.WriteLine("Parse Time: " + sw.Elapsed.TotalSeconds);
            PrintActionType<BuildAction>(replay);

            Console.ReadKey();
        }

        public static void PrintActionType<T>(IReplay replay)
        {

            var actions1 = replay.Actions.Where(x => x is T)
                .OrderBy(x => x.Frame)
                .ToList<IAction>();

            foreach (var a in actions1)
            {
                if (typeof(T) == typeof(BuildAction))
                {
                    Console.Write("{0,10} - {2,-15} - {1,10}", a.Frame, a.ActionType, a.Player.Name);
                    Console.Write(" - {0}", ((BuildAction)a).ObjectType);
                }
                else if (typeof(T) == typeof(GenericObjectTypeAction))
                {
                    Console.Write("{0,10} - {2,-15} - {1,10}", a.Frame, a.ActionType, a.Player.Name);
                    Console.Write(" - {0}", ((GenericObjectTypeAction)a).ObjectType);
                }
                else 
                {
                    Console.Write(a);
                }

                Console.WriteLine();
            }
        }
    }
}
