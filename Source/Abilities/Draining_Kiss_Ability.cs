using System;
using System.Threading.Tasks;

using UnityEngine;
using Verse;
using Verse.Sound;
using RimWorld;

using RaddusX.Demons.Utility;
using RaddusX.Demons.ModSettings;

namespace RaddusX.Demons.Abilities
{
    public class Draining_Kiss_Ability : CompProperties_AbilityEffect
    {
        public Draining_Kiss_Ability()
        {
            compClass = typeof(Draining_Kiss_Ability_Effect);
        }
    }

    public class Draining_Kiss_Ability_Effect : CompAbilityEffect
    {
        private new Draining_Kiss_Ability Props => (Draining_Kiss_Ability)props;

        /**
        * Called when checking if the Ability can be used on the target
        *
        * @param LocalTargetInfo   target         The target of the ability
        * @param bool              throwMessages  (Default true) Whether to display messages at the top left of the screen, when the ability cannot be used for example.
        *
        * @return bool
       */
        public override bool Valid(LocalTargetInfo localTargetInfo, bool throwMessages = true)
        {
            Logging_Utility.LogMessage("RaddusX.Demons.Draining_Kiss_Ability_Effect.Valid() Called");

            // Perform parent checks
            if (!base.Valid(localTargetInfo, throwMessages))
            {
                Logging_Utility.LogMessage("Parent returned false for valid check.");
                return false;
            }

            Pawn target = localTargetInfo.Pawn;

            // Target is a Sanguophage (or has Hemogenic gene), or is a demon (succubus or incubus)
            if (Pawn_Utility.IsHemogenic(target) || Pawn_Utility.IsSanguophageXenotype(target) || Pawn_Utility.IsDemonXenotype(target))
            {
                if (throwMessages)
                {
                    Messages.Message("RaddusX.Demons.DrainingKissAbility.InvalidTarget".Translate(parent.def.label), target, MessageTypeDefOf.RejectInput, historical: false);
                }
                return false;
            }

            return true;
        }

        /**
         * Called when the Ability is used (after Valid() checks above)
         *
         * @param LocalTargetInfo       localTargetInfo    The target of the ability's info
         * @param LocalTargetInfo|null  _unused            NULL
         *
         * @return void
        */
        public override void Apply(LocalTargetInfo localTargetInfo, LocalTargetInfo _unused)
        {
            Pawn target = localTargetInfo.Pawn;
            Pawn caster = parent.pawn;

            string casterFullName = caster.Name.ToStringFull;
            string targetFullName = target.Name.ToStringFull;

            Logging_Utility.LogMessage($"RaddusX.Demons.Draining_Kiss_Ability_Effect.Apply() Called [Caster: {casterFullName} | Target: {targetFullName}]");

            DetermineDrainingKissOutcome(caster, target);
        }

        /**
         * Determine the outcome of the Draining Kiss ability between the caster & target (success, failure, etc)
         *
         * @param Pawn  caster  The caster
         * @param Pawn  target  The target
         *
         * @return void
        */
        private void DetermineDrainingKissOutcome(Pawn caster, Pawn target)
        {
            Logging_Utility.LogMessage("RaddusX.Demons.Draining_Kiss_Ability_Effect.DetermineDrainingKissOutcome() Called");

            Melee_Attack_Success_Calculator meleeAttackSuccessCalculator = new Melee_Attack_Success_Calculator(caster, target);

            float successChance = meleeAttackSuccessCalculator.GetSuccessChance();

            Logging_Utility.LogMessage("Final Success Chance: " + successChance);

            System.Random random = new System.Random();

            double roll = random.NextDouble() * 100f;

            Logging_Utility.LogMessage("Roll out of 100: " + roll);

            //Logging_Utility.LogMessage("TEST.");
            //DrainCaster(caster, target);
            //DrainTarget(target, caster);
            //return;

            if (roll <= successChance)
            {
                Logging_Utility.LogMessage("SUCCESS");

                roll = random.NextDouble() * 100f;

                Logging_Utility.LogMessage("New Roll: " + roll);

                if (roll <= 25f)
                {
                    Logging_Utility.LogMessage("Roll <= 25f. Transforming target into a Sanguophage.");
                    TransformTarget(target, caster);
                }
                else
                {
                    Logging_Utility.LogMessage("Roll > 25f. Draining target.");
                    DrainTarget(target, caster);
                }
            }
            else
            {
                Logging_Utility.LogMessage("FAILURE. Draining caster.");
                DrainCaster(caster, target);
            }
        }

        /**
         * Called when the Caster failed using the Draining Kiss ability.
         *
         * @param Pawn  caster  The caster
         *
         * @return void
        */
        private void DrainCaster(Pawn caster, Pawn target)
        {
            Logging_Utility.LogMessage("RaddusX.Demons.Draining_Kiss_Ability_Effect.DrainCaster() Called [Caster: " + caster.Name.ToStringFull + "]");

            // Remove Drained hediff if it already exists (to reset timer)
            Hediff firstHediffOfDef = caster.health.hediffSet.GetFirstHediffOfDef(Defs.RaddusX_Demons_Draining_Kiss_Caster_Drained_Hediff);
            if (firstHediffOfDef != null)
            {
                Logging_Utility.LogWarning("Caster already has the Drained hediff. Removing.");
                caster.health.RemoveHediff(firstHediffOfDef);
            }

            // Add Drained hediff
            caster.health.AddHediff(Defs.RaddusX_Demons_Draining_Kiss_Caster_Drained_Hediff);

            // Give target a negative thought towards the caster since they tried to drain them
            Thought_Memory thought = ThoughtMaker.MakeThought(Defs.RaddusX_Demons_Tried_To_Drain_Me_Negative_Thought, 0);
            if (thought == null)
            {
                Logging_Utility.LogMessage("thought is null");
            }
            else
            {
                Logging_Utility.LogMessage("thought is not null");
                target.needs.mood.thoughts.memories.TryGainMemory(thought, caster);
            }

            // Make victim hostile & attack the caster (if capable)
            if (target.Downed)
            {
                Logging_Utility.LogMessage("Target is downed. Skipping giving them the berserk mental state, as it will cause an error.");
                return;
            }
            bool defenderIncapableOfViolence = target.skills.GetSkill(SkillDefOf.Melee).TotallyDisabled;
            Logging_Utility.LogMessage($"defender incapable of violence: {defenderIncapableOfViolence}");
            if (!defenderIncapableOfViolence)
            {
                Logging_Utility.LogMessage("defender is not incapable of violence. Giving mental state berserk with the caster as the target.");
                MentalStateDef stateDef = MentalStateDefOf.Berserk;
                string reason = "RaddusX.Demons.DrainingKissAbility.TargetBerserkReason".Translate(caster.Name.ToStringFull, parent.def.label);
                bool forced = true;
                bool forceWake = true;
                bool causedByMood = false;
                Pawn otherPawn = caster;
                bool transitionSilently = false;
                bool causedByDamage = true;
                bool causedByPsycast = true;
                bool result = target.mindState.mentalStateHandler.TryStartMentalState(stateDef, reason, forced, forceWake, causedByMood, otherPawn, transitionSilently, causedByDamage, causedByPsycast);
                Logging_Utility.LogMessage($"result: {result}");
            }
        }

        /**
         * Called when the Target failed to resist the Draining Kiss ability (but not to be turned into a Sanguophage)
         *
         * @param Pawn  target  The target
         * @param Pawn  caster  The caster
         *
         * @return void
        */
        private void DrainTarget(Pawn target, Pawn caster)
        {
            Logging_Utility.LogMessage("RaddusX.Demons.Draining_Kiss_Ability_Effect.DrainTarget() Called [Caster: " + target.Name.ToStringFull + "]");

            // Add Drained hediff if it doesn't exist
            Hediff drainedHediff = target.health.hediffSet.GetFirstHediffOfDef(Defs.RaddusX_Demons_Draining_Kiss_Victim_Drained_Hediff);
            if (drainedHediff == null)
            {
                target.health.AddHediff(Defs.RaddusX_Demons_Draining_Kiss_Victim_Drained_Hediff);
                drainedHediff = target.health.hediffSet.GetFirstHediffOfDef(Defs.RaddusX_Demons_Draining_Kiss_Victim_Drained_Hediff);
            }

            Logging_Utility.LogMessage("drainedHediff: " + drainedHediff.GetType());

            // Damage Info
            DamageDef damageDef = DamageDefOf.Crush; // DamageDefOf.Psychic requires Anomaly
            float damageAmount = 9999f;
            float armorPenetration = 0f;
            float angle = -1f;
            Thing instigator = caster;
            BodyPartRecord hitPart = null; // whole body
            ThingDef weapon = caster.def;
            DamageInfo.SourceCategory sourceCategory = DamageInfo.SourceCategory.ThingOrUnknown;
            Thing intendedTarget = target;
            bool instigatorGuilty = true; // doesn't seem to work?
            bool spawnFilth = true;
            QualityCategory weaponQuality = QualityCategory.Normal;
            bool checkForJobOverride = false; // ?

            // Kill target
            DamageInfo damageInfo = new DamageInfo(damageDef, damageAmount, armorPenetration, angle, instigator, hitPart, weapon, sourceCategory, intendedTarget,
                instigatorGuilty, spawnFilth, weaponQuality, checkForJobOverride);
            target.Kill(damageInfo, drainedHediff);

            // Add buffs to caster
            AddBuffsToCasterAfterDraining(caster);
        }

        /**
         * Called when the Target failed to resist the Draining Kiss ability AND is to be transformed into a Sanguophage.
         *
         * @param Pawn  target  The target
         * @param Pawn  caster  The caster
         *
         * @return void
        */
        private void TransformTarget(Pawn target, Pawn caster)
        {
            Logging_Utility.LogMessage("RaddusX.Demons.Draining_Kiss_Ability_Effect.TransformTarget() Called");

            // Change Xenotype
            target.genes.SetXenotype(XenotypeDefOf.Sanguophage);

            // Add XenogerminationComa hediff
            target.health.AddHediff(HediffDefOf.XenogerminationComa);

            // Set Hemogen level to 0
            Gene_Hemogen hemogenGene = target.genes?.GetFirstGeneOfType<Gene_Hemogen>();
            if (hemogenGene != null)
            {
                hemogenGene.Value = 0f;
            }

            // Wasn't able to lower the Deathrest need at the moment, so leaving it for now.
            // ...

            // Update Target visuals
            target.Drawer.renderer.SetAllGraphicsDirty();

            // Add buffs to caster
            AddBuffsToCasterAfterDraining(caster);
        }

        /**
         * Add buffs or other positive changes to the caster, since they have successfully drained a target (whether turning them into a sanguophage or not)
         *
         * @param Pawn  caster The caster
         *
         * @return void
        */
        private void AddBuffsToCasterAfterDraining(Pawn caster)
        {
            // Recharge Caster's Psyfocus
            caster.psychicEntropy.RechargePsyfocus();

            // Remove Drained hediff from caster (if exists)
            Hediff firstHediffOfDef = caster.health.hediffSet.GetFirstHediffOfDef(Defs.RaddusX_Demons_Draining_Kiss_Caster_Drained_Hediff);
            if (firstHediffOfDef != null)
            {
                caster.health.RemoveHediff(firstHediffOfDef);
            }

            // Add Ecstasy hediff to Caster
            caster.health.AddHediff(Defs.RaddusX_Demons_Draining_Kiss_Caster_Ecstasy_Hediff);
        }
    }
}