using BepInEx;
using BepInEx.Bootstrap;
using Partiality.Modloader;
using System;

namespace BepInExPartialityWrapper
{
    [BepInPlugin("com.ashnal.BepInExPartialityWrapper", "Partiality Wrapper", "1.0")]
    public class PartialityWrapper : BaseUnityPlugin
    {
        public PartialityWrapper()
        {
            foreach (Type t in TypeLoader.LoadTypes<PartialityMod>(Paths.PluginPath))
            {
                //Don't try to load the base class
                if (t.Name != "PartialityMod")
                {
                    Logger.LogInfo("Partiality Wrapper - instantiating " + t.Name);
                    var mod = Activator.CreateInstance(t) as PartialityMod;
                    try
                    {
                        Logger.LogInfo("Initializing and Loading " + mod.ModID);
                        mod.Init();
                        mod.OnLoad();
                        mod.OnEnable();
                    }
                    catch (Exception e)
                    {
                        Logger.LogError("Could not load Partiality Mod: " + mod.ModID + "\r\n" + e);
                    }
                }
            }
        }
    }
}