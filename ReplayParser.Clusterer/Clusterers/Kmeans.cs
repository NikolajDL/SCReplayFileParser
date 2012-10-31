using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplayParser.Interfaces;
using ReplayParser.Clusterer.BuildorderTree;
using ReplayParser.Actions;

namespace ReplayParser.Clusterer
{
    class Centroid
    {
        private Node<BuildAction> m_centroid;
        public Node<BuildAction> Value { get { return m_centroid; } }

        private List<Node<BuildAction>> m_observations = new List<Node<BuildAction>>();
        public List<Node<BuildAction>> Observations { get { return m_observations; } }

        public Centroid(Node<BuildAction> centroid)
        {
            this.m_centroid = centroid;
        }

        public void AddObservation(Node<BuildAction> obs)
        {
            m_observations.Add(obs);
        }
    }

    class Kmeans
    {
        private static Random rand = new Random();

        public void Cluster(int k, NodeList<BuildAction> observations)
        {
            List<Centroid> centroids = new List<Centroid>();
            // Use random observations as centroids
            while (k > 0)
            {
                centroids.Add(new Centroid(observations.ElementAt(rand.Next(observations.Count - 1))));
                k--;
            }

            // Suk, jeg giver op. LINQ driller :(. For-loops for everybody instead!
            //var stuffz = observations.Select(x => centroids.Select(y => calcDistance(x, y.Value)).Min());
            foreach (var o in observations)
            {
                Centroid closestCentroid = null;
                double closestDistance = 9999999999999999999;
                foreach (var c in centroids)
                {
                    double dist = calcDistance(o, c.Value);
                    if (dist < closestDistance)
                    {
                        closestCentroid = c;
                        closestDistance = dist;
                    }
                }
                closestCentroid.AddObservation(o);
            }


            var stuffz = iterate(centroids);
        }

        private List<Centroid> iterate(List<Centroid> centroids)
        {
            List<Centroid> result = new List<Centroid>();

            foreach (var c in centroids)
            {
                Centroid newCentroid = generateCentroid(c.Observations);
                foreach (var o in c.Observations)
                {
                    // Cannot really
                }
            }
            return result;
        }

        private Centroid generateCentroid(List<Node<BuildAction>> observations, int counter = 1)
        {
            // Find most common building
            var mostCommonBuilding = (from item in observations
                          group item by item.Value.ObjectType into g
                          orderby g.Count() descending
                          select g.Key).First();

            var commonObs = observations.Where(x => x.Value.ObjectType.ToString() == mostCommonBuilding.ToString());
            
            Node<BuildAction> node = new Node<BuildAction>(1, commonObs.First().Value, generateCentroidNeighbours(commonObs));
            
            return new Centroid(node);
        }

        private NodeList<BuildAction> generateCentroidNeighbours(IEnumerable<Node<BuildAction>> commonObs)
        {
            // Ensure we actually have neighbours to add
            if (commonObs == null) return null;
            foreach (var n in commonObs)
            {
                if (n.Neighbors == null) return null;
            }

            // Find the most common building among neighbours
            var mostCommonNeighbourBuilding = (from item in commonObs.SelectMany(x => x.Neighbors)
                                   group item by item.Value.ObjectType into g
                                   orderby g.Count() descending
                                   select g.Key).First();

            var commonNeighbour = commonObs.SelectMany(x => x.Neighbors)
                .Where(x => x.Value.ObjectType.ToString() == mostCommonNeighbourBuilding.ToString());

            NodeList<BuildAction> result = new NodeList<BuildAction>();
            result.Add(new Node<BuildAction>(1, commonNeighbour.First().Value, generateCentroidNeighbours(commonNeighbour)));
            return result;
        }

        

        private double calcDistance(Node<BuildAction> o, Node<BuildAction> c, int heightCounter = 1)
        {
            double result = 0;

            if (o.Value.ObjectType != c.Value.ObjectType)
                result += weight(heightCounter);

            if (o.Neighbors == null || c.Neighbors == null) return result;
            // You are supposed to pass in the raw 'games' themself, not the complete, built, tree. Thus, every node will have at most 1 child.
            result += calcDistance(o.Neighbors.ElementAt(0), c.Neighbors.ElementAt(0), ++heightCounter);
            return result;
        }

        private double weight(int n)
        {
            // Exponentially decaying, n > 20 = 0. http://www.wolframalpha.com/input/?i=lim+e^%28-n%2F5%29+as+n-%3E10
            return (Math.Pow(Math.E, (-n / 5)));
        }

        private List<Entities.ObjectType> allBuildings = new List<Entities.ObjectType>()
        {
            Entities.ObjectType.Barracks, Entities.ObjectType.SupplyDepot,
            Entities.ObjectType.CommandCenter, Entities.ObjectType.Refinery,
            Entities.ObjectType.EngineeringBay, Entities.ObjectType.Bunker,
            Entities.ObjectType.Academy, Entities.ObjectType.Factory,
            Entities.ObjectType.MissileTurret, Entities.ObjectType.ComsatStation,
            Entities.ObjectType.MachineShop, Entities.ObjectType.Starport,
            Entities.ObjectType.Armory, Entities.ObjectType.ScienceFacility,
            Entities.ObjectType.ControlTower, Entities.ObjectType.PhysicsLab,
            Entities.ObjectType.CovertOps, Entities.ObjectType.NuclearSilo,

            Entities.ObjectType.ArbiterTribunal, Entities.ObjectType.Assimilator,
            Entities.ObjectType.CitadelOfAdun, Entities.ObjectType.CyberneticsCore,
            Entities.ObjectType.FleetBeacon, Entities.ObjectType.Forge,
            Entities.ObjectType.Gateway, Entities.ObjectType.Nexus,
            Entities.ObjectType.Observatory, Entities.ObjectType.PhotonCannon,
            Entities.ObjectType.Pylon, Entities.ObjectType.RoboticsFacility,
            Entities.ObjectType.RoboticsSupportBay, Entities.ObjectType.ShieldBattery,
            Entities.ObjectType.Stargate, Entities.ObjectType.TemplarArchives,

            Entities.ObjectType.CreepColony, Entities.ObjectType.DefilerMound,
            Entities.ObjectType.EvolutionChamber, Entities.ObjectType.Extractor,
            Entities.ObjectType.GreaterSpire, Entities.ObjectType.Hatchery,
            Entities.ObjectType.Hive, Entities.ObjectType.HydraliskDen,
            Entities.ObjectType.InfestedCommandCenter, Entities.ObjectType.Lair,
            Entities.ObjectType.NydusCanal, Entities.ObjectType.QueensNest,
            Entities.ObjectType.SpawningPool, Entities.ObjectType.Spire,
            Entities.ObjectType.SporeColony, Entities.ObjectType.SunkenColony,
            Entities.ObjectType.UltraliskCavern
        };
        
    }
}
