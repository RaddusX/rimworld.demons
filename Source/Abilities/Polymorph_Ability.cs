using System;
using UnityEngine;
using Verse;
using RimWorld;

using RaddusX.Demons.Utility;

namespace RaddusX.Demons.Abilities
{
    public class Polymorph_Ability : CompProperties_AbilityEffect
    {
        public Polymorph_Ability()
        {
            compClass = typeof(Polymorph_Ability_Effect);
        }
    }

    public class Polymorph_Ability_Effect : CompAbilityEffect
    {
        private new Polymorph_Ability Props => (Polymorph_Ability)props;

        /**
         * Called when the ability is used.
         *
         * @param LocalTargetInfo caster    The caster of the ability
         * @param LocalTargetInfo _unused   Unused
         *
         * @return void
        */
        public override void Apply(LocalTargetInfo caster, LocalTargetInfo _unused)
        {
            Logging_Utility.LogMessage("RaddusX.Demons.Polymorph_Ability_Effect.Apply() Called");

            // Invalid pawn
            if (caster.Pawn == null)
            {
                Logging_Utility.LogWarning("Caster pawn is null. Skipping.");
                return;
            }

            Pawn pawn = caster.Pawn;

            // Not Succubus or Incubus (other xenotypes using this ability not supported at this point)
            if (!(Pawn_Utility.IsIncubusXenotype(pawn) || Pawn_Utility.IsSuccubusXenotype(pawn)))
            {
                Logging_Utility.LogWarning("Pawn's xenotype is not Incubus or Succubus. Skipping.");
                return;
            }

            /*
            * Draining Kiss ability cooldown resets, since the gene that gives this ability is removed and replaced with a similar one, 
            * (human form vs demon form), so we need to update it accordingly (it's updated below after switching forms)
            */
            Ability drainingKiss = pawn.abilities.GetAbility(Defs.RaddusX_Demons_Draining_Kiss_Ability);
            int cooldownTicksRemaining = 0;
            // By default, only Succubi have this ability, so it could be null
            if (drainingKiss != null)
            {
                cooldownTicksRemaining = drainingKiss.CooldownTicksRemaining;
                Logging_Utility.LogMessage("Cooldown ticks remaining: " + cooldownTicksRemaining);
            }

            /*
            * Switch form
            */

            // Demon Form - Switch to Human Form
            if (Pawn_Utility.IsInDemonForm(pawn))
            {
                Logging_Utility.LogMessage("Caster is in DEMON form. Changing to HUMAN form...");

                // Remove Demon Form gene
                Gene pawnDemonFormGene = Pawn_Utility.GetDemonFormXenogene(pawn);
                pawn.genes.RemoveGene(pawnDemonFormGene);

                // Add Human Form gene
                GeneDef pawnHumanFormGeneDef = Pawn_Utility.GetHumanFormXenogeneDef(pawn);
                pawn.genes.AddGene(pawnHumanFormGeneDef, xenogene: true);

                // Override Horns, Claws, Wings, Tail, and Skin Color
                AddHumanFormGeneOverrides(pawn);
            }
            else // Human form - Switch to Demon form
            {
                Logging_Utility.LogMessage("Pawn is in HUMAN form. Changing to DEMON form...");

                RemoveHumanFormGeneOverrides(pawn);
            }

            /*
            * Update Draining Kiss ability cooldown (if necessary)
            */

            // We need to get the ability again as it won't be the same one since the genes changed
            drainingKiss = pawn.abilities.GetAbility(Defs.RaddusX_Demons_Draining_Kiss_Ability);
            if (drainingKiss != null)
            {
                Logging_Utility.LogMessage("Updating Draining Kiss ability cooldown.");
                drainingKiss.StartCooldown(cooldownTicksRemaining);
            }

            /*
            * Update pawn gfx
            */

            // Appearance doesn't update automatically unless f.e. changing/removing clothes, loading a save, thus we need to do this:
            pawn.Drawer.renderer.SetAllGraphicsDirty();
        }

        /**
         * Add overrides for horns/claws/wings/tail/skin color when transforming into the human form.
         *
         * Overrides are added so that specific genes are disabled, for example so that horns are no longer displayed when in human form.
         *
         * @param Pawn  pawn  The pawn
         *
         * @return void
        */
        private void AddHumanFormGeneOverrides(Pawn pawn)
        {
            Gene pawnHumanFormGene = Pawn_Utility.GetHumanFormXenogene(pawn);

            if (pawnHumanFormGene == null)
            {
                Logging_Utility.LogMessage("Pawn doesn't have the HUMAN form gene. SKIPPED ADDING OVERRIDES for horns/claws/wings/tail/skin color genes.");
                return;
            }

            // Add Override for Horns, Claws, Wings, Tail, and Skin Color
            if (Pawn_Utility.IsIncubusXenotype(pawn))
            {
                Pawn_Utility.GetXenotypeGene(Defs.RaddusX_Demons_Incubus_Horns_Gene, pawn)?.OverrideBy(pawnHumanFormGene);
            }
            else
            {
                Pawn_Utility.GetXenotypeGene(Defs.RaddusX_Demons_Succubus_Horns_Gene, pawn)?.OverrideBy(pawnHumanFormGene);
            }
            Pawn_Utility.GetXenotypeGene(Defs.Eyes_Red, pawn)?.OverrideBy(pawnHumanFormGene);
            Pawn_Utility.GetXenotypeGene(Defs.RaddusX_Demons_Claws_Gene, pawn)?.OverrideBy(pawnHumanFormGene);
            Pawn_Utility.GetXenotypeGene(Defs.RaddusX_Demons_Tail_Gene, pawn)?.OverrideBy(pawnHumanFormGene);

            // Skin doesn't update even if it's being overwritten, so we'll remove it instead.
            Pawn_Utility.GetXenotypeGene(Defs.Skin_PaleRed, pawn)?.OverrideBy(pawnHumanFormGene);
            Gene pawnPaleRedSkinGene = Pawn_Utility.GetXenotypeGene(Defs.Skin_PaleRed, pawn);
            if (pawnPaleRedSkinGene != null)
            {
                pawn.genes.RemoveGene(pawnPaleRedSkinGene);
            }

            // Wings doesn't get overridden once the skin color gene above is removed, so we'll add the overwrite here:
            Pawn_Utility.GetXenotypeGene(Defs.RaddusX_Demons_Wings_Gene, pawn)?.OverrideBy(pawnHumanFormGene);
        }

        /**
         * Remove the overrides for horns/claws/wings/tail/skin color when transforming into the demon form.
         *
         * @param Pawn  pawn  The pawn
         *
         * @return void
        */
        private void RemoveHumanFormGeneOverrides(Pawn pawn)
        {
            Gene pawnHumanFormGene = Pawn_Utility.GetHumanFormXenogene(pawn);

            if (pawnHumanFormGene == null)
            {
                Logging_Utility.LogMessage("Pawn doesn't have the HUMAN form gene. SKIPPED REMOVING OVERRIDES for horns/claws/wings/tail/skin color genes.");
                return;
            }

            // Remove Human Form gene
            pawn.genes.RemoveGene(pawnHumanFormGene);

            // Add Demon Form gene
            GeneDef demonFormGeneDef = Pawn_Utility.GetDemonFormXenogeneDef(pawn);
            pawn.genes.AddGene(demonFormGeneDef, xenogene: true);

            // Remove Override for Horns, Claws, Wings, Tail, and Skin Color
            if (Pawn_Utility.IsIncubusXenotype(pawn))
            {
                Pawn_Utility.GetXenotypeGene(Defs.RaddusX_Demons_Incubus_Horns_Gene, pawn)?.OverrideBy(null);
            }
            else
            {
                Pawn_Utility.GetXenotypeGene(Defs.RaddusX_Demons_Succubus_Horns_Gene, pawn)?.OverrideBy(null);
            }

            Pawn_Utility.GetXenotypeGene(Defs.Eyes_Red, pawn)?.OverrideBy(null);
            Pawn_Utility.GetXenotypeGene(Defs.RaddusX_Demons_Claws_Gene, pawn)?.OverrideBy(null);
            Pawn_Utility.GetXenotypeGene(Defs.RaddusX_Demons_Wings_Gene, pawn)?.OverrideBy(null);
            Pawn_Utility.GetXenotypeGene(Defs.RaddusX_Demons_Tail_Gene, pawn)?.OverrideBy(null);

            // Skin doesn't update even if being overwritten, so we'll need to add it back now:
            pawn.genes.AddGene(Defs.Skin_PaleRed, xenogene: true);
        }
    }
}