using UnityEngine;
using GameTools;

public struct StructAttackCardData
{
    public EnumAttackType attackType;
    public int attackRollModifier;
    public int damageRollModifier;
    public int opponentHitCounterCost;

    public StructAttackCardData
        (
            EnumAttackType _attackType,
            int _attackRollModifier,
            int _damageRollModifier,
            int _opponentHitCounterCost
        )
    {
        attackType = _attackType;
        attackRollModifier = _attackRollModifier;
        damageRollModifier = _damageRollModifier;
        opponentHitCounterCost = _opponentHitCounterCost;
    }
}


[CreateAssetMenu(fileName = "AttackCard", menuName = "Cards/Attack Card")]
public class AttackCard : CardSO
{

    [SerializeField] private EnumAttackType attackType;
    [Range(-10, 10)][SerializeField] private int attackRollModifier     = 0;
    [Range(-5, 5)]  [SerializeField] private int damageRollModifier     = 0;
    [Range(0,10)]   [SerializeField] private int opponentHitCounterCost = 1;

    private void OnValidate()
    {
        CardType = EnumCardType.ATTACK;
    }

    public StructAttackCardData GetAttackCardData()
    {
        StructAttackCardData data = new StructAttackCardData
            (
                attackType,
                attackRollModifier,
                damageRollModifier,
                opponentHitCounterCost
            );
        return data;
    }
}
