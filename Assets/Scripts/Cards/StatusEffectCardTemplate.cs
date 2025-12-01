using UnityEngine;
using TMPro;

public class StatusEffectCardTemplate : MonoBehaviour
{
    [SerializeField] private TMP_Text statusEffectText;
    [SerializeField] private TMP_Text statusEffectValueText;
    [SerializeField] private TMP_Text statusEffectTurnsText;

    string statusEffectPrefix = string.Empty;
    string valuePrefix = string.Empty;
    string turnsPrefix = string.Empty;

    private void Awake()
    {
        statusEffectPrefix = statusEffectText.text;
        valuePrefix = statusEffectValueText.text;
        turnsPrefix = statusEffectTurnsText.text;
    }

    public void FixStatusEffectUI(StructStatusEffectCardData statusEffectData)
    {
        statusEffectText.text = $"{statusEffectPrefix} {statusEffectData.effect.ToString()}";
        statusEffectValueText.text = $"{valuePrefix} {statusEffectData.value.ToString()}";
        statusEffectTurnsText.text = $"{turnsPrefix} {statusEffectData.turnsDuration.ToString()}";
    }
}
