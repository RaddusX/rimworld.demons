using System;
using System.Threading.Tasks;

using UnityEngine;
using Verse;
using Verse.Sound;
using RimWorld;

using RaddusX.Demons.Utility;

namespace RaddusX.Demons.ModSettings
{
    public static class Mod_Settings_Utility
    {
        private static Mod_Settings modSettings;

        static Mod_Settings_Utility()
        {
            modSettings = LoadedModManager.GetMod<Mod>().GetSettings<Mod_Settings>();
        }

        /**
         * Whether logging is enabled.
         * @return bool
        */
        public static bool IsLoggingEnabled()
        {
            return modSettings.loggingEnabled;
        }
    }
}