
using Verse;

using RaddusX.Demons;
using RaddusX.Demons.Utility;

namespace RaddusX.Demons
{
    [StaticConstructorOnStartup]
    public static class Demons
    {
        static Demons()
        {
            Logging_Utility.LogMessage("Mod loaded.");

            //Harmony.DEBUG = true;
            //Harmony harmony = new Harmony("RaddusX_Demons");
            //harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}