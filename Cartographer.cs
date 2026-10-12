using BepInEx;
using HarmonyLib;

namespace Cartographer
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Cartographer : BaseUnityPlugin
    {
        private const string PluginGuid = "ewigl." + PluginName;
        private const string PluginName = "Cartographer";
        private const string PluginVersion = "1.0.0";

        private void Awake()
        {
            Harmony.CreateAndPatchAll(typeof(Cartographer).Assembly, PluginGuid);

            Logger.LogInfo($"Plugin {PluginName} is loaded!");
        }

        [HarmonyPatch(typeof(MapTable), "OnRead", typeof(Switch), typeof(Humanoid), typeof(ItemDrop.ItemData), typeof(bool))]
        public static class MapTableOnReadPatch
        {
            private static void Postfix()
            {
                if (Game.m_noMap)
                {
                    Game.m_noMap = false;
                    Minimap.instance.SetMapMode(Minimap.MapMode.Large);
                    Game.m_noMap = true;
                }
            }
        }
    }
}