using HarmonyLib;
using BepInEx;
using System.IO;
using System.Reflection;
using VehicleFramework.VehicleTypes;
using VehicleFramework;
using System.Collections;

namespace PrecursorSubmarine
{

    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency(VehicleFramework.PluginInfo.PLUGIN_GUID)]
    class MainPatcher : BaseUnityPlugin
    {
        public void Awake()
        {
            PrecursorSubmarine.getAssets();
        }

        public void Start()
        {
            var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
            UWE.CoroutineHost.StartCoroutine(Register());
        }

        public static IEnumerator Register()
        {
            Submersible sb = PrecursorSubmarine.model.EnsureComponent<PrecursorSubmarine>() as Submersible;
            yield return UWE.CoroutineHost.StartCoroutine(VehicleRegistrar.RegisterVehicle(sb));
        }
    }
}
