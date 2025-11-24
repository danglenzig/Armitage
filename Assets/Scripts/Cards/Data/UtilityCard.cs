using UnityEngine;
using GameTools;


public struct StructUtilityCardData
{
    public EnumUtilityType utilityType;
    public int utilityValue;

    public StructUtilityCardData
        (
            EnumUtilityType _utilityType,
            int _utilityValue
        )
    {
        utilityType = _utilityType;
        utilityValue = _utilityValue;
    }
}


[CreateAssetMenu(fileName = "UtilityCard", menuName = "Cards/Utility Card")]
public class UtilityCard : CardSO
{

    [SerializeField] private EnumUtilityType utilityType;
    [Range(0, 10)][SerializeField] private int utilityValue;
    // for HEAL cards this amount is added to the 1xD6 heal roll
    // for RESOLVE cards. this amount is subtracted from hit count
    private void OnValidate()
    {
        CardType = EnumCardType.UTILITY;
    }

    public StructUtilityCardData GetUtilityCardData()
    {
        StructUtilityCardData data = new StructUtilityCardData
            (
                utilityType,
                utilityValue
            );
        return data;
    }

}
