using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplayParser.Interfaces;
using ReplayParser.Clusterer.Clusterers;
using ReplayParser.Clusterer.BuildorderTree;
using ReplayParser.Actions;

namespace ReplayParser.Clusterer
{
    class Kmeans : IClusterer
    {
        public void Cluster()
        {
            // TODO!
        }

        private List<Entities.ObjectType> terranBuildings = new List<Entities.ObjectType>() {   
                Entities.ObjectType.Barracks, Entities.ObjectType.SupplyDepot,
                Entities.ObjectType.CommandCenter, Entities.ObjectType.Refinery,
                Entities.ObjectType.EngineeringBay, Entities.ObjectType.Bunker,
                Entities.ObjectType.Academy, Entities.ObjectType.Factory,
                Entities.ObjectType.MissileTurret, Entities.ObjectType.ComsatStation,
                Entities.ObjectType.MachineShop, Entities.ObjectType.Starport,
                Entities.ObjectType.Armory, Entities.ObjectType.ScienceFacility,
                Entities.ObjectType.ControlTower, Entities.ObjectType.PhysicsLab,
                Entities.ObjectType.CovertOps, Entities.ObjectType.NuclearSilo
            };

        private List<Entities.ObjectType> protossBuildings = new List<Entities.ObjectType>() {   
                Entities.ObjectType.ArbiterTribunal, Entities.ObjectType.Assimilator,
                Entities.ObjectType.CitadelOfAdun, Entities.ObjectType.CyberneticsCore,
                Entities.ObjectType.FleetBeacon, Entities.ObjectType.Forge,
                Entities.ObjectType.Gateway, Entities.ObjectType.Nexus,
                Entities.ObjectType.Observatory, Entities.ObjectType.PhotonCannon,
                Entities.ObjectType.Pylon, Entities.ObjectType.RoboticsFacility,
                Entities.ObjectType.RoboticsSupportBay, Entities.ObjectType.ShieldBattery,
                Entities.ObjectType.Stargate, Entities.ObjectType.TemplarArchives
            };

        private List<Entities.ObjectType> zergBuildings = new List<Entities.ObjectType>() {   
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
