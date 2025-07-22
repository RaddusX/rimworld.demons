using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
//using System.Reflection;
//using HarmonyLib;

using RaddusX.Demons;
using RaddusX.Demons.Utility;

namespace RaddusX.Demons
{
    [StaticConstructorOnStartup]
    public static class Demons
    {
        static Demons()
        {
            Logging_Utility.LogMessage("RaddusX: Demons mod loaded.");

            //Harmony.DEBUG = true;
            //Harmony harmony = new Harmony("RaddusX_Demons");
            //harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}