using Verse;

using RaddusX.Demons.Utility;

namespace RaddusX.Demons.Patches
{
    class NL_Facial_Animation_Patcher
    {
        /**
         * Notify the "[NL] Facial Animation" mod of any components that are dirty and need to be refreshed.
         *
         * WARNING: Should only be called if the above mod is active.
         *
         * @public
         *
         * @static
         *
         * @param Pawn pawn
         *
         * @return void
         */
        public static void SetDirty(Pawn pawn)
        {
            string pawnName = pawn.Name.ToStringFull;
            
            Logging_Utility.LogMessage($"Notifying NL Facial Animation that pawn {pawnName} is dirty...");

            // Right now we are just concerned with the eye component as everything else appears to be working correctly.
            var comp = pawn.GetComp<FacialAnimation.EyeballControllerComp>();

            if (comp == null)
            {
                Logging_Utility.LogWarning($"FAILURE: Pawn does not have the FacialAnimation.EyeballControllerComp component. Skipping.");
                return;
            }

            comp.SetDirty();

            Logging_Utility.LogMessage("SUCCESS?");
        }
    }
}