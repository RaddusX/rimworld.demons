using System;
using System.Threading.Tasks;

using UnityEngine;
using Verse;
using Verse.Sound;
using RimWorld;

using RaddusX.Demons.ModSettings;

namespace RaddusX.Demons.Utility
{
    public static class Logging_Utility
    {
        public static void LogMessage(string message)
        {
            if (Mod_Settings_Utility.IsLoggingEnabled())
            {
                Log.Message(message);
            }
        }

        public static void LogWarning(string message)
        {
            if (Mod_Settings_Utility.IsLoggingEnabled())
            {
                Log.Warning(message);
            }
        }

        public static void LogError(string message)
        {
            if (Mod_Settings_Utility.IsLoggingEnabled())
            {
                Log.Error(message);
            }
        }
    }
}