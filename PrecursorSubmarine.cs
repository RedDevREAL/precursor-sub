using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VehicleFramework;
using VehicleFramework.VehicleParts;
using VehicleFramework.VehicleTypes;
using System.IO;
using System.Reflection;

namespace PrecursorSubmarine
{
    public class PrecursorSubmarine : Submersible
    {
        public static GameObject model = null;

        public static void getAssets()
        {
            string modPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(modPath, "precursorsub"));
            if(myLoadedAssetBundle == null)
            {
                // Error: Failed to load AssetBundle!
                return;
            }

            System.Object[] are = myLoadedAssetBundle.LoadAllAssets();
            foreach (System.Object obj in are)
            {
                if (obj.ToString().Contains("PrecursorSubsmall"))
                {
                    model = (GameObject)obj;
                }
            }
        }

        public override VehiclePilotSeat PilotSeat
        {
            get
            {
                VehiclePilotSeat vps = new VehiclePilotSeat();
                Transform mainSeat = transform.Find("PilotSeat");
                vps.Seat = mainSeat.gameObject;
                vps.SitLocation = mainSeat.gameObject;
                vps.LeftHandLocation = mainSeat;
                vps.RightHandLocation = mainSeat;

                return vps;
            }
        }

        public override List<VehicleHatchStruct> Hatches
        {
            get
            {
                var list = new List<VehicleHatchStruct>();

                VehicleHatchStruct interior_vhs = new VehicleHatchStruct();
                Transform intHatch = transform.Find("Hatch");
                interior_vhs.Hatch = intHatch.gameObject;
                interior_vhs.ExitLocation = intHatch.Find("ExitLocation");
                interior_vhs.SurfaceExitLocation = intHatch.Find("ExitLocation");
                list.Add(interior_vhs);
                return list;
            }
        }

        public override GameObject VehicleModel
        {
            get
            {
                return model;
            }
        }

        public override GameObject CollisionModel
        {
            get
            {
                return transform.Find("CollisionModel").gameObject;
            }
        }

        public override GameObject StorageRootObject
        {
            get
            {
                return transform.Find("StorageRoot").gameObject;
            }
        }

        public override GameObject ModulesRootObject
        {
            get
            {
                return transform.Find("ModulesRoot").gameObject;
            }
        }

        public override List<VehicleStorage> InnateStorages
        {
            get
            {
                var list = new List<VehicleStorage>();
                VehicleStorage thisVS = new VehicleStorage();
                Transform thisStorage = transform.Find("Storage/pack1");
                thisVS.Container = thisStorage.gameObject;
                thisVS.Height = 8;
                thisVS.Width = 6;
                list.Add(thisVS);

                VehicleStorage thisVS2 = new VehicleStorage();
                Transform thisStorage2 = transform.Find("Storage/pack2");
                thisVS2.Container = thisStorage2.gameObject;
                thisVS2.Height = 8;
                thisVS2.Width = 6;
                list.Add(thisVS2);

                return list;
            }
        }

        public override List<VehicleStorage> ModularStorages
        {
            get
            {
                var list = new List<VehicleStorage>();
                return list;
            }
        }

        public override List<VehicleUpgrades> Upgrades
        {
            get
            {
                var list = new List<VehicleUpgrades>();
                VehicleUpgrades vu = new VehicleUpgrades();
                vu.Interface = transform.Find("UpgradesInterface").gameObject;
                vu.Flap = vu.Interface;
                list.Add(vu);

                return list;
            }
        }

        public override List<VehicleBattery> Batteries 
        {
            get
            {
                var list = new List<VehicleBattery>();

                VehicleBattery vb = new VehicleBattery();
                vb.BatterySlot = transform.Find("Batteries/Battery1").gameObject;
                list.Add(vb);

                VehicleBattery vb2 = new VehicleBattery();
                vb2.BatterySlot = transform.Find("Batteries/Battery2").gameObject;
                list.Add(vb2);

                return list;
            }
        }

        public override List<VehicleFloodLight> HeadLights
        {
            get
            {
                var list = new List<VehicleFloodLight>();

                VehicleFloodLight left = new VehicleFloodLight
                {
                    Light = transform.Find("lights_parent/head_light1").gameObject,
                    Angle = 21.2f,
                    Color = Color.white,
                    Intensity = 1.2f,
                    Range = 13.6f
                };
                list.Add(left);


                VehicleFloodLight right = new VehicleFloodLight
                {
                    Light = transform.Find("lights_parent/head_light2").gameObject,
                    Angle = 21.2f,
                    Color = Color.white,
                    Intensity = 1.2f,
                    Range = 13.6f
                };
                list.Add(right);

                return list;
            }
        }

        public override List<GameObject> WaterClipProxies
        {
            get
            {
                var list = new List<GameObject>();
                foreach(Transform child in transform.Find("WaterClipProxy"))
                {
                    list.Add(child.gameObject);
                }
                return list;
            }
        }

        public override List<GameObject> CanopyWindows
        {
            get
            {
                var list = new List<GameObject>();
                list.Add(transform.Find("model/precursor_deco_props_01").gameObject);
                return list;
            }
        }

        public override GameObject BoundingBox
        {
            get
            {
                return transform.Find("BoundingBox").gameObject;
            }
}

        public override Dictionary<TechType, int> Recipe
        {
            get
            {
                Dictionary<TechType, int> list = new Dictionary<TechType, int>();
                list.Add(TechType.PrecursorIonCrystal, 3);
                list.Add(TechType.PlasteelIngot, 1);
                list.Add(TechType.TitaniumIngot, 2);
                list.Add(TechType.EnameledGlass, 1);
                list.Add(TechType.Lead, 2);
                list.Add(TechType.AdvancedWiringKit, 1);
                list.Add(TechType.ComputerChip, 1);
                list.Add(TechType.PowerCell, 2);
                return list;
            }
        }

        public override Atlas.Sprite PingSprite => null;

        public override string Description => "A small but strong ion-powered submarine made for exploration.";

        public override string EncyclopediaEntry => "A small but strong ion-powered submarine made for exploration. Be aware of the dangers in the depths...";

        public override int BaseCrushDepth => 300;

        public override int CrushDepthUpgrade1 => 200;

        public override int CrushDepthUpgrade2 => 200;

        public override int CrushDepthUpgrade3 => 300;

        public override int MaxHealth => 1000;

        public override int Mass => 800;

        public override int NumModules => 4;

        public override bool HasArms => false;
    }
}
