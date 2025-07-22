using System;
using System.Threading.Tasks;

using UnityEngine;
using Verse;
using Verse.Sound;
using RimWorld;

using RaddusX.Demons.Utility;

namespace RaddusX.Demons.ModSettings
{
    public class Mod_Settings : Verse.ModSettings
    {
        /**
        * Whether logging is enabled
        * @param bool
        */
        public bool loggingEnabled = false;

        /**
         * Write our settings to file.
         * @return void
        */
        public override void ExposeData()
        {
            Scribe_Values.Look(ref loggingEnabled, "loggingEnabled");
            base.ExposeData();
        }
    }

    public class Mod : Verse.Mod
    {
        /**
        * Our mod settings reference
        * @param ModSettings
        */
        Mod_Settings settings;

        /**
         * Constructor
         * @return void
        */
        public Mod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<Mod_Settings>();
        }

        /**
         * Add our settings.
         * @return void
        */
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled("RaddusX.Demons.LoggingEnabled.Label".Translate(), ref settings.loggingEnabled, "RaddusX.Demons.LoggingEnabled.Tooltip".Translate());

            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        /**
         * The name of the settings in the Mod Options panel.
         * @return string
        */
        public override string SettingsCategory()
        {
            return "RaddusX - Demons";
        }
    }
}