using UnityEngine;
using GameTools;
using TMPro;

public class UtilityCardTemplate : MonoBehaviour
{

    [SerializeField] private TMP_Text utilityTypeText;
    [SerializeField] private TMP_Text utilityValueText;

    private string typePrefix = string.Empty;
    private string valuePrefix = string.Empty;

    private void Awake()
    {
        typePrefix = utilityTypeText.text;
        valuePrefix = utilityValueText.text;
    }

    public void FixUtilityCardUI(StructUtilityCardData utilityData)
    {
        utilityTypeText.text = $"{typePrefix} {utilityData.utilityType.ToString()}";
        utilityValueText.text = $"{valuePrefix} {utilityData.utilityValue.ToString()}";
    }
}
