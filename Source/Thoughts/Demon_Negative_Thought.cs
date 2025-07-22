using System;
using UnityEngine;
using Verse;
using RimWorld;

using RaddusX.Demons.Utility;

namespace RaddusX.Demons.Thoughts
{
    public class Demon_Negative_Thought : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn observer, Pawn target)
        {
            Logging_Utility.LogMessage("RaddusX.Demons.Demon_Negative_Thought.CurrentSocialStateInternal() Called");

            // Observer is dead
            if (observer.Dead)
            {
                return false;
            }

            // Target is dead
            if (target.Dead)
            {
                return false;
            }

            // Target isn't humanlike or they don't know of each other
            if (!target.RaceProps.Humanlike || !RelationsUtility.PawnsKnowEachOther(observer, target))
            {
                return false;
            }

            // Observer is blind
            if (PawnUtility.IsBiologicallyOrArtificiallyBlind(observer))
            {
                return false;
            }

            // Is the pawn themselves a demon?
            if (observer.genes.HasActiveGene(Defs.RaddusX_Demons_Incubus_Demon_Form_Gene)
            || observer.genes.HasActiveGene(Defs.RaddusX_Demons_Incubus_Human_Form_Gene)
            || observer.genes.HasActiveGene(Defs.RaddusX_Demons_Succubus_Demon_Form_Gene)
            || observer.genes.HasActiveGene(Defs.RaddusX_Demons_Succubus_Human_Form_Gene))
            {
                return false;
            }

            // Other pawn is in their Demon form
            if (target.genes.HasActiveGene(Defs.RaddusX_Demons_Incubus_Demon_Form_Gene) || target.genes.HasActiveGene(Defs.RaddusX_Demons_Succubus_Demon_Form_Gene))
            {
                // Can the observer see the target?
                Map mapHeld = observer.MapHeld;
                if (mapHeld == null)
                {
                    return false;
                }
                IntVec3 observerPositionHeld = observer.PositionHeld;
                IntVec3 targetPawnPositionHeld = target.PositionHeld;

                bool canSeeTarget = GenSight.LineOfSight(observerPositionHeld, targetPawnPositionHeld, mapHeld);

                if (canSeeTarget)
                {
                    return ThoughtState.ActiveAtStage(0);
                }
            }

            return false;
        }
    }
}