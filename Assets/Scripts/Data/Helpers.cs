using UnityEngine;
using System.Collections.Generic;
namespace GameTools
{

    [System.Serializable]
    public struct StructCombatantData
    {
        public string combatantName;
        public string combatantID;
        public int maxHTH;    // How close to dead you are
        public int eVA;   // attack rolls > this number are a hit
        public int rES;   // subtracted from attacker's damage roll
        public int tUF;   // Number of hits you can sustain before RES falters to zero
        public int perTurnAP;
        public int initiative;
        public EnumCombatantControl control;

        public StructCombatantData
            (
                string _combatantName = Constants.DEFAULT_COMBATANT_NAME,
                string _combatantID = Constants.UNSET_COMBATANT_ID,
                int _maxHTH = Constants.DEFAULT_BASE_HTH,
                int _eVA = Constants.DEFAULT_BASE_EVA,
                int _rES = Constants.DEFAULT_BASE_RES,
                int _tUF = Constants.DEFAULT_BASE_TUF,
                int _perTurnAP = Constants.DEFAULT_AP,
                int _initiative = Constants.DEFAULT_INITIATIVE,
                EnumCombatantControl _control = EnumCombatantControl.PLAYER
            )
        {
            combatantName = _combatantName;
            combatantID = _combatantID;
            maxHTH = _maxHTH;
            eVA = _eVA;
            rES = _rES;
            tUF = _tUF;
            perTurnAP = _perTurnAP;
            initiative = _initiative;
            control = _control;
        }
    }
    /*
    public struct StructBadStatusEffect
    {
        public EnumBadStatusEffects effect;
        public int penalty;
        public int remainingTurns;
    }
    public struct StructGoodStatusEffect
    {
        public EnumGoodStatusEffects effect;
        public int bonus;
        public int remainingTurns;
    }
    */

    public struct StructStatusEffect
    {
        public EnumStatusEffects effect;
        public int value;
        public int remainingTurns;
    }

    public static class Constants
    {
        public const string DEFAULT_COMBATANT_NAME = "Namey Nameson";
        public const string UNSET_COMBATANT_ID = "NO_ID";
        public const int DEFAULT_BASE_HTH = 20;
        public const int DEFAULT_BASE_EVA = 11;
        public const int DEFAULT_BASE_RES = 2;
        public const int DEFAULT_BASE_TUF = 4;
        public const int DEFAULT_AP = 3;
        public const int DEFAULT_INITIATIVE = 0;

        public const string COMBATANT_TAG = "COMBATANT";
    }

    public static class AnimNames
    {
        public const string IDLE = "IDLE";
        public const string RUN = "RUN";
        public const string RUN_BACK = "RUN_BACK";
        public const string BLOCK = "BLOCK";
        public const string DIE = "DIE";
        public const string HEAL = "HEAL";
        public const string REACT = "REACT";
    }



    public enum EnumCombatantControl
    {
        PLAYER,
        CPU
    }
    public enum EnumCardType
    {
        ATTACK,
        UTILITY,
        STATUS_EFFECT
    }
    public enum EnumAttackType
    {
        LIGHT,
        HEAVY
    }
    public enum EnumUtilityType
    {
        HEAL,
        RESOLVE
    }
    /*
    public enum EnumBadStatusEffects
    {
        VULNERABLE, // - ro RES
        WEAK,       // - to TUF
        SLOW,       // - to EVA
        CONFUSED    // - to AP
    }
    public enum EnumGoodStatusEffects
    {
        PROTECTED, // + to RES
        STRONG,    // + to TUF
        QUICK,     // + to EVA
        DECISIVE   // + to AP
    }
    */

    public enum EnumStatusEffects
    {
        VULNERABLE, // - to RES
        WEAK,       // - to TUF
        SLOW,       // - to EVA
        CONFUSED,    // - to AP
        PROTECTED, // + to RES
        STRONG,    // + to TUF
        QUICK,     // + to EVA
        DECISIVE   // + to AP
    }

    public enum EnumFocus
    {
        FOCUSED, // it is your turn
        UNFOCUSED // it is not your turn
    }

    //not sure if we need EnumActionType yet
    public enum EnumActionType
    {
        ATTACK,
        UTILITY,
        REPLACE_CARD,
    }

    public class RandomTools
    {
        public static List<T> ShuffleList<T>(List<T> inList)
        {
            List<T> inListCopy = new List<T>(inList);

            // Fisher-Yates Shuffle
            for (int i = inListCopy.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (inListCopy[i], inListCopy[j]) = (inListCopy[j], inListCopy[i]);
            }
            return inListCopy;
        }

        public static List<T> GetUniqueRandomElements<T>(List<T> inList, int count)
        {
            if (count < 1)
            {
                Debug.LogWarning("Count must be > 0 -- setting to 1");
                count = 1;
            }
            List<T> outList = new List<T>();
            List<T> shuffledInListCopy = ShuffleList<T>(inList);
            outList = shuffledInListCopy.GetRange(0, count);
            return outList;
        }
    }

}


