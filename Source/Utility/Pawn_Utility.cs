using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace RaddusX.Demons.Utility
{
    public static class Pawn_Utility
    {
        #nullable enable
        /**
        * Get the specified gene from the specified pawn's xenotype.
        *
        * @param string defName The name of the gene definition
        * @param Pawn   pawn    The pawn
        *
        * @return Gene|null
        **/
        public static Gene? GetXenotypeGene(GeneDef defName, Pawn pawn)
        {
            foreach (var xenogene in pawn.genes.Xenogenes)
            {
                if (xenogene.def == defName)
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