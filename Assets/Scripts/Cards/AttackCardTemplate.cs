using UnityEngine;
using GameTools;
using TMPro;
using Unity.VisualScripting;

public class AttackCardTemplate : MonoBehaviour //, ICardable
{
    [SerializeField] private TMP_Text attackTypeText;
    [SerializeField] private TMP_Text attackRollModText;
    [SerializeField] private TMP_Text damageRollModText;
    [SerializeField] private TMP_Text opponentHitPointsText;

    private string attackTypePrefix = string.Empty;
    private string attackRollModPrefix = string.Empty;
    private string damageRollModPrefix = string.Empty;
    private string opponentHitPointsPrefix = string.Empty;

    private void Awake()
    {
        attackTypePrefix = attackTypeText.text;
        attackRollModPrefix = attackRollModText.text;
        damageRollModPrefix = damageRollModText.text;
        opponentHitPointsPrefix = opponentHitPointsText.text;
    }


    public void FixAttackCardUI(StructAttackCardData attackData)
    {
        attackTypeText.text = $"{attackTypePrefix} {attackData.attackType.ToString()}";
        attackRollModText.text = $"{attackRollModPrefix} {attackData.attackRollModifier.ToString()}";
        damageRollModText.text = $"{damageRollModPrefix} {attackData.damageRollModifier.ToString()}";
        opponentHitPointsText.text = $"{opponentHitPointsPrefix} {attackData.opponentHitCounterCost.ToString()}";
    }

}
