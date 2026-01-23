using Verse;

using RaddusX.Demons.ModSettings;

namespace RaddusX.Demons.Utility
{
    public static class Logging_Utility
    {
        public static void LogMessage(string message)
        {
            if (Mod_Settings_Utility.IsLoggingEnabled())
            {
                Log.Message("RaddusX Demons: " + message);
            }
        }

        public static void LogWarning(string message)
        {
            if (Mod_Settings_Utility.IsLoggingEnabled())
            {
                Log.Warning("RaddusX Demons: " + message);
            }
        }

        public static void LogError(string message)
        {
            if (Mod_Settings_Utility.IsLoggingEnabled())
            {
                Log.Error("RaddusX Demons: " + message);
            }
        }
    }
}