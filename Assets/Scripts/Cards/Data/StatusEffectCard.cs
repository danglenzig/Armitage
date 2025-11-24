using UnityEngine;
using GameTools;

public enum StatusEffectTarget
{
    SELF,
    OPPONENT
}
public struct StructStatusEffectCardData
{
    public EnumStatusEffects effect;
    public int value;
    public int turnsDuration;
    public StatusEffectTarget target;

    public StructStatusEffectCardData
        (
            EnumStatusEffects _effect,
            int _value,
            int _turnsDuration,
            StatusEffectTarget _target
        )
    {
        effect = _effect;
        value = _value;
        turnsDuration = _turnsDuration;
        target = _target;
    }
}


[CreateAssetMenu(fileName = "StatusEffectCard", menuName = "Cards/Status Effect Card")]
public class StatusEffectCard : CardSO
{
    [SerializeField] private EnumStatusEffects effect;
    [SerializeField] private int value = 0;
    [Min(1)] [SerializeField] private int turnsDuration = 1;
    [SerializeField] private StatusEffectTarget target;

    private void OnValidate()
    {
        CardType = EnumCardType.STATUS_EFFECT;
    }

    public StructStatusEffectCardData GetStatusEffectCardData()
    {
        StructStatusEffectCardData data = new StructStatusEffectCardData
            (
                effect,
                value,
                turnsDuration,
                target
            );
        return data;
    }


}
