using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace RaddusX.Demons
{
    [DefOf]
    public static class Defs
    {
        // Incubus & Succubus
        public static GeneDef Eyes_Red;
        public static GeneDef RaddusX_Demons_Claws_Gene;
        public static GeneDef RaddusX_Demons_Wings_Gene;
        public static GeneDef RaddusX_Demons_Tail_Gene;
        public static GeneDef Skin_PaleRed;

        // Incubus
        public static XenotypeDef RaddusX_Demons_Incubus_Xenotype;
        public static GeneDef RaddusX_Demons_Incubus_Human_Form_Gene;
        public static GeneDef RaddusX_Demons_Incubus_Demon_Form_Gene;
        public static GeneDef RaddusX_Demons_Incubus_Horns_Gene;

        // Succubus
        public static XenotypeDef RaddusX_Demons_Succubus_Xenotype;
        public static GeneDef RaddusX_Demons_Succubus_Human_Form_Gene;
        public static GeneDef RaddusX_Demons_Succubus_Demon_Form_Gene;
        public static GeneDef RaddusX_Demons_Succubus_Horns_Gene;

        // Draining Kiss Ability
        public static AbilityDef RaddusX_Demons_Draining_Kiss_Ability;

        // Draining Kiss Ability Hediffs
        public static HediffDef RaddusX_Demons_Draining_Kiss_Victim_Drained_Hediff;
        public static HediffDef RaddusX_Demons_Draining_Kiss_Caster_Drained_Hediff;
        public static HediffDef RaddusX_Demons_Draining_Kiss_Caster_Ecstasy_Hediff;

        // Draining Kiss Negative Thoughts
        public static ThoughtDef RaddusX_Demons_Tried_To_Drain_Me_Negative_Thought;

    }
}