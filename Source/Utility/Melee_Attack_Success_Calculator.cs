using System;
using System.Threading.Tasks;

using UnityEngine;
using Verse;
using Verse.Sound;
using RimWorld;

namespace RaddusX.Demons.Utility
{
    public class Melee_Attack_Success_Calculator
    {
        /**
        * The maximum skill level in RimWorld.
        * @param float
        */
        private readonly float maxSkillLevel = 20f;

        /**
        * The default success chance (50%)
        * @param float
        */
        private readonly float defaultSuccessChance = 50f;

        /**
        * The success chance maximum (95%)
        * @param float
        */
        private float successChanceMax;

        /**
        * The success chance minimum (5%)
        * @param float
        */
        private float successChanceMin;

        /**
        * The difference between the min/max success chance and the baseline (50%), which would be 45%
        * @param float
        */
        private float differenceBetweenMinMaxAndBaseline;

        /**
        * The success chance increase per level. By default this is 2.25%.
        * @param float
        */
        private float successChanceIncreasePerLevel;

        /**
        * The attacker's melee skill level.
        * @param int
        */
        private int attackerMeleeSkillLevel;

        /**
        * The defender's melee skill level.
        * @param int
        */
        private int defenderMeleeSkillLevel;

        /**
        * Whether the attacker is incapable of violence
        * @param bool
        */
        private bool attackerIncapableOfViolence;

        /**
        * Whether the defender is incapable of violence
        * @param bool
        */
        private bool defenderIncapableOfViolence;

        /**
        * Whether the defender is downed
        * @param bool
        */
        private bool defenderIsDowned;

        /**
        * The attacker's melee skill XP.
        * @param float
        */
        private float attackerMeleeSkillXP;

        /**
        * The defender's melee skill XP.
        * @param float
        */
        private float defenderMeleeSkillXP;

        /**
        * The amount of XP required to level up for the attacker's melee skill.
        * @param float
        */
        private float attackerMeleeSkillXPRequiredToLevelUp;

        /**
        * The amount of XP required to level up for the defender's melee skill.
        * @param float
        */
        private float defenderMeleeSkillXPRequiredToLevelUp;

        /**
        * The difference between the attacker & defender's melee skill level (-20 to 20).
        * @param int
        */
        private int levelDifference;

        /**
        * Constructor
        *
        * @param Pawn  attacker           The attacker
        * @param Pawn  defender           The defender
        * @param float successChanceMin   The minimum success chance. Default 5%.
        * @param float successChanceMax   The maximum success chance. Default 95%.
        */
        public Melee_Attack_Success_Calculator(Pawn attacker, Pawn defender, float successChanceMin = 5f, float successChanceMax = 95f)
        {
            // Success Chance Minimum & Maximum
            this.successChanceMin = successChanceMin;
            this.successChanceMax = successChanceMax;

            // Success Chance Increase Per Level
            this.successChanceIncreasePerLevel = (100 - (100 - this.successChanceMax + this.successChanceMin)) / this.maxSkillLevel;

            // Difference between Min & Max Success Chance and Baseline
            this.differenceBetweenMinMaxAndBaseline = this.successChanceMax - 50; // Difference between the baseline (50%) and the caps (5% & 95%)

            // Attacker & Defender Skill Records
            SkillRecord attackerMeleeSkillRecord = attacker.skills.GetSkill(SkillDefOf.Melee);
            SkillRecord defenderMeleeSkillRecord = defender.skills.GetSkill(SkillDefOf.Melee);

            // Attacker & Defender Skill Levels
            this.attackerMeleeSkillLevel = attackerMeleeSkillRecord.GetLevel(includeAptitudes: true);
            this.defenderMeleeSkillLevel = defenderMeleeSkillRecord.GetLevel(includeAptitudes: true);

            // Attacker & Defender - Incapable of violence
            this.attackerIncapableOfViolence = attackerMeleeSkillRecord.TotallyDisabled;
            this.defenderIncapableOfViolence = defenderMeleeSkillRecord.TotallyDisabled;

            // Defender - Downed?
            this.defenderIsDowned = defender.Downed;

            // Attacker & Defender Melee Skill XP
            // XP can be negative. We treat any negative value as 0 for calculations.
            this.attackerMeleeSkillXP = Mathf.Max(0f, attackerMeleeSkillRecord.xpSinceLastLevel);
            this.defenderMeleeSkillXP = Mathf.Max(0f, defenderMeleeSkillRecord.xpSinceLastLevel);

            // Attacker & Defender Melee Skill XP Required To Level Up
            this.attackerMeleeSkillXPRequiredToLevelUp = attackerMeleeSkillRecord.XpRequiredForLevelUp;
            this.defenderMeleeSkillXPRequiredToLevelUp = defenderMeleeSkillRecord.XpRequiredForLevelUp;

            // Level Difference
            this.levelDifference = this.attackerMeleeSkillLevel - this.defenderMeleeSkillLevel; // from -20 to 20

            // Debug
            Logging_Utility.LogMessage($"Success Chance Variables: [Min: {this.successChanceMin}% | Max: {this.successChanceMax}% | Increase Per Level: {this.successChanceIncreasePerLevel}%]");
            Logging_Utility.LogMessage($"Attacker Melee Skill: [Level: {this.attackerMeleeSkillLevel} | XP: {this.attackerMeleeSkillXP} | XP To Level Up: {this.attackerMeleeSkillXPRequiredToLevelUp}]");
            Logging_Utility.LogMessage($"Defender Melee Skill: [Level: {this.defenderMeleeSkillLevel} | XP: {this.defenderMeleeSkillXP} | XP To Level Up: {this.defenderMeleeSkillXPRequiredToLevelUp}]");
            Logging_Utility.LogMessage($"Level Difference: {Math.Abs(this.levelDifference)} (Internal: {this.levelDifference})");
        }

        /**
        * Get the success chance of an attack by the attacker against the defender.
        *
        * @return float
        */
        public float GetSuccessChance()
        {
            // Attacker is incapable of violence. 
            // By default, this ability can't be used by violence-incapable pawns. However, if a user wants to change this on the AbilityDef xml, they can.
            // Thus, I'll keep this commented out to allow that.
            //if (this.attackerIncapableOfViolence)
            //{
            //    return 0f;
            //}

            // Defender is incapable of violence, thus incapable of defending themselves? However, there is still a small chance it might fail for some reason.
            if (this.defenderIncapableOfViolence)
            {
                Logging_Utility.LogMessage("Defender is incapable of violence. Success chance = success chance max.");
                return this.successChanceMax;
            }

            // Defender is downed
            if (this.defenderIsDowned)
            {
                Logging_Utility.LogMessage("Defender is downed. Success chance = success chance max.");
                return this.successChanceMax;
            }

            float successChance = this.defaultSuccessChance;

            /*
            * Calculate success chance based on level difference
            * When the levels are the same, success chance will be 50% (see above)
            */
            float successChanceFromLevelDifference = 0f;
            if (this.levelDifference > 0)
            {
                successChanceFromLevelDifference = this.differenceBetweenMinMaxAndBaseline / this.maxSkillLevel * this.levelDifference;
                successChance += successChanceFromLevelDifference;
                Logging_Utility.LogMessage($"Success Chance From Level Diff: {this.differenceBetweenMinMaxAndBaseline} (baseline) / {this.maxSkillLevel} (max skill lvl) * {this.levelDifference} (level diff) = {successChanceFromLevelDifference}");
                Logging_Utility.LogMessage($"{this.defaultSuccessChance} (default success chance) + {successChanceFromLevelDifference} (success chance from lvl diff) = {successChance} (success chance)");
            }
            else if (this.levelDifference < 0)
            {
                successChanceFromLevelDifference = this.differenceBetweenMinMaxAndBaseline / this.maxSkillLevel * Math.Abs(this.levelDifference);
                successChance -= successChanceFromLevelDifference;
                Logging_Utility.LogMessage($"Success Chance From Level Diff: {this.differenceBetweenMinMaxAndBaseline} (baseline) / {this.maxSkillLevel} (max skill lvl) * {Math.Abs(this.levelDifference)} (level diff) = {successChanceFromLevelDifference}");
                Logging_Utility.LogMessage($"{this.defaultSuccessChance} (default success chance) - {successChanceFromLevelDifference} (success chance from lvl diff) = {successChance} (success chance)");
            }

            /* 
            * Calculate additional success chance based on XP
            */

            // XP is the same
            if (this.attackerMeleeSkillXP == this.defenderMeleeSkillXP)
            {
                Logging_Utility.LogMessage("Attacker & Defender XP is the same.");

                return Mathf.Clamp(successChance, this.successChanceMin, this.successChanceMax);
            }

            // XP is greater or lesser than the target's XP
            Logging_Utility.LogMessage("Attacker & Defender XP is NOT the same.");

            Logging_Utility.LogMessage("Success Chance Before Adding XP: " + successChance);

            float xpDifference = 0f;
            float successChanceFromXP = 0f;

            // Attacker's XP is higher than the defender's
            if (this.attackerMeleeSkillXP > this.defenderMeleeSkillXP)
            {
                Logging_Utility.LogMessage("Attacker XP is higher than target XP.");

                xpDifference        = this.attackerMeleeSkillXP - this.defenderMeleeSkillXP;
                successChanceFromXP = xpDifference / this.attackerMeleeSkillXPRequiredToLevelUp * this.successChanceIncreasePerLevel;
                successChance      += successChanceFromXP;

                Logging_Utility.LogMessage($"{xpDifference} / {this.attackerMeleeSkillXPRequiredToLevelUp} * {this.successChanceIncreasePerLevel}");
            }
            else // Attacker's XP is lower than the defender's.
            {
                Logging_Utility.LogMessage("Attacker XP is lower than target XP.");

                xpDifference        = this.defenderMeleeSkillXP - this.attackerMeleeSkillXP;
                successChanceFromXP = xpDifference / this.defenderMeleeSkillXPRequiredToLevelUp * this.successChanceIncreasePerLevel;
                successChance      -= successChanceFromXP;

                Logging_Utility.LogMessage($"{xpDifference} / {this.defenderMeleeSkillXPRequiredToLevelUp} * {this.successChanceIncreasePerLevel}");
            }

            Logging_Utility.LogMessage("Success Chance From XP: " + successChanceFromXP);

            return Mathf.Clamp(successChance, this.successChanceMin, this.successChanceMax);
        }
    }
}