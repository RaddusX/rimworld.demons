using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;

namespace RaddusX.Demons.Utility
{
    public static class Pawn_Utility
    {
        #nullable enable
        
        /**
        * Remove the specified Gene as an override on all endogenes on the specified pawn
        *
        * @param Gene  gene  The gene
        * @param Pawn  pawn  The pawn
        *
        * @return bool True if at least one gene had this override and it was removed.
        **/
        public static bool RemoveGeneOverrideFromAllEndogenes(Gene gene, Pawn pawn)
        {
            bool removedOverrideAtLeastOnce = false;
            foreach (var endogene in pawn.genes.Endogenes)
            {
                if (endogene.overriddenByGene == gene)
                {
                    removedOverrideAtLeastOnce = true;
                    endogene.OverrideBy(null);
                }
            }
            return removedOverrideAtLeastOnce;
        }

        /**
        * Remove all xenogenes of the specified GeneDef on the specified Pawn.
        *
        * @param GeneDef geneDef  The GeneDef
        * @param Pawn    pawn     The pawn
        *
        * @return void
        **/
        public static void RemoveAllXenogenesOfGeneDef(GeneDef geneDef, Pawn pawn)
        {
            for (int i = pawn.genes.Xenogenes.Count - 1; i >= 0; --i)
            {   
                if (pawn.genes.Xenogenes[i].def == geneDef)
                {
                    pawn.genes.RemoveGene(pawn.genes.Xenogenes[i]);
                }
            }
        }

        /**
        * Get the specified gene from the specified pawn's xenotype.
        *
        * @param GeneDef geneDef  The GeneDef
        * @param Pawn    pawn     The pawn
        *
        * @return Gene|null
        **/
        public static Gene? GetXenotypeGene(GeneDef geneDef, Pawn pawn)
        {
            foreach (var xenogene in pawn.genes.Xenogenes)
            {
                if (xenogene.def == geneDef)
                {
                    return xenogene;
                }
            }
            return null;
        }

         /**
         * Get the pawn's HUMAN form Xenogene Gene
         *
         * <Gene> types are required for REMOVING genes, amongst other things.
         *
         * @param Pawn  pawn  The pawn
         *
         * @return Gene|null
        */
        public static Gene? GetDemonFormXenogene(Pawn pawn)
        {
            return IsIncubusXenotype(pawn) ?
                GetXenotypeGene(Defs.RaddusX_Demons_Incubus_Demon_Form_Gene, pawn) :
                GetXenotypeGene(Defs.RaddusX_Demons_Succubus_Demon_Form_Gene, pawn);
        }

         /**
         * Get the pawn's DEMON form Xenogene GeneDef
         *
         * <GeneDef> types are required for ADDING genes, amongst other things.
         *
         * @param Pawn  pawn  The pawn
         *
         * @return GeneDef
        */
        public static GeneDef GetDemonFormXenogeneDef(Pawn pawn)
        {
            return IsIncubusXenotype(pawn) ?
                Defs.RaddusX_Demons_Incubus_Demon_Form_Gene :
                Defs.RaddusX_Demons_Succubus_Demon_Form_Gene;
        }

        /**
         * Get the pawn's HUMAN form Xenogene Gene
         *
         * <Gene> types are required for REMOVING genes, amongst other things.
         *
         * @param Pawn  pawn  The pawn
         *
         * @return Gene|null
        */
        public static Gene? GetHumanFormXenogene(Pawn pawn)
        {
            return IsIncubusXenotype(pawn) ?
                GetXenotypeGene(Defs.RaddusX_Demons_Incubus_Human_Form_Gene, pawn) :
                GetXenotypeGene(Defs.RaddusX_Demons_Succubus_Human_Form_Gene, pawn);
        }

        /**
         * Get the pawn's HUMAN form xenogene GeneDef
         *
         * <GeneDef> types are required for ADDING genes, amongst other things.
         *
         * @param Pawn  pawn  The pawn
         *
         * @return GeneDef
        */
        public static GeneDef GetHumanFormXenogeneDef(Pawn pawn)
        {
            return IsIncubusXenotype(pawn) ?
                Defs.RaddusX_Demons_Incubus_Human_Form_Gene :
                Defs.RaddusX_Demons_Succubus_Human_Form_Gene;
        }

       /**
         * Whether the pawn has the Hemogenic gene
         *
         * @param Pawn  pawn  The pawn
         *
         * @return bool
        */
        public static bool IsHemogenic(Pawn pawn)
        {
            return pawn.genes.HasActiveGene(GeneDefOf.Hemogenic);
        }

        /**
         * Whether the pawn has Sanguophage xenotype
         *
         * @param Pawn  pawn  The pawn
         *
         * @return bool
        */
        public static bool IsSanguophageXenotype(Pawn pawn)
        {
            return pawn.genes.Xenotype == XenotypeDefOf.Sanguophage;
        }

        /**
         * Whether the pawn is a demon xenotype (currently, succubus or incubus)
         *
         * @param Pawn  pawn  The pawn
         *
         * @return bool
        */
        public static bool IsDemonXenotype(Pawn pawn)
        {
            return IsIncubusXenotype(pawn) || IsSuccubusXenotype(pawn);
        }

        /**
         * Whether the pawn has the Incubus Xenotype.
         *
         * @param Pawn  pawn  The pawn
         *
         * @return bool
        */
        public static bool IsIncubusXenotype(Pawn pawn)
        {
            return pawn.genes.Xenotype == Defs.RaddusX_Demons_Incubus_Xenotype;
        }

        /**
         * Whether the pawn has the Succubus Xenotype.
         *
         * @param Pawn  pawn  The pawn
         *
         * @return bool
        */
        public static bool IsSuccubusXenotype(Pawn pawn)
        {
            return pawn.genes.Xenotype == Defs.RaddusX_Demons_Succubus_Xenotype;
        }
        
        /**
         * Whether the pawn is in their DEMON form or not
         *
         * @param Pawn  pawn  The pawn
         *
         * @return bool
        */
        public static bool IsInDemonForm(Pawn pawn)
        {
            return IsIncubusXenotype(pawn) ?
                pawn.genes.HasActiveGene(Defs.RaddusX_Demons_Incubus_Demon_Form_Gene) :
                pawn.genes.HasActiveGene(Defs.RaddusX_Demons_Succubus_Demon_Form_Gene);
        }
    }
}