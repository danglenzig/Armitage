using UnityEngine;
using UnityEngine.UI;
using GameTools;
using GameEvents;
using TMPro;

public class CardUI : MonoBehaviour //, ICardable
{
    [SerializeField] private RawImage faceImage;
    [SerializeField] private TMP_Text cardTypeText;
    [SerializeField] private TMP_Text cardNameText;
    [SerializeField] private TMP_Text actionCostText;

    [SerializeField] private AttackCardTemplate attackCardUI;
    [SerializeField] private UtilityCardTemplate utilityCardUI;
    [SerializeField] private StatusEffectCardTemplate statusEffectCardUI;

    [SerializeField] private InputDetector inputDetector;
    [SerializeField] private StringPayloadEvent cardSelectedEvent;

    [HideInInspector] public AttackCardTemplate AttackCardUI { get => attackCardUI; }
    [HideInInspector] public UtilityCardTemplate UtilityCardUI { get => utilityCardUI; }
    [HideInInspector] public StatusEffectCardTemplate StatusEffectUI { get => statusEffectCardUI; }

    private StructCardData currentCardData;
    private string currentCardID = string.Empty;
    private bool selectable = true;

    public string CurrentCardID { get => currentCardID; }

    [HideInInspector] public bool Selectable
    {
        get => selectable;
        set
        {
            if (value != selectable)
            {
                selectable = value;
            }
        }
    }

    public void SetIsSubdued(bool isSubdued)
    {
        if (isSubdued) { faceImage.color = Color.gray; }
        else { faceImage.color = Color.white; }
    }

    private void Awake()
    {
        HideTemplates();
    }

    private void OnEnable()
    {
        inputDetector.MouseEntered += HandleOnMouseEnter;
        inputDetector.MouseExited += HandleOnMouseExit;
        inputDetector.MouseClicked += HandleOnMouseClicked;
    }
    private void OnDisable()
    {
        inputDetector.MouseEntered -= HandleOnMouseEnter;
        inputDetector.MouseExited -= HandleOnMouseExit;
        inputDetector.MouseClicked -= HandleOnMouseClicked;
    }

    private void HandleOnMouseEnter()
    {
        if (!selectable) { return; }
        transform.localScale = Vector3.one * 1.2f;
    }
    private void HandleOnMouseExit()
    {
        if (!selectable) { return; }
        transform.localScale = Vector3.one;
    }

    private void HandleOnMouseClicked()
    {
        if (!selectable) { return; }
        if (currentCardID == string.Empty) { Debug.LogError("Something weird happend"); return; }
        cardSelectedEvent.TriggerEvent(currentCardID);
    }

    public void FixCardUI(StructCardData cardData, string cardID)
    {
        HideTemplates();
        currentCardID = cardID;
        currentCardData = cardData;

        // TODO: fix image
        cardNameText.text = cardData.cardName;
        cardTypeText.text = cardData.cardType.ToString();
        actionCostText.text = $"AP: {cardData.actionCost.ToString()}";

        switch (cardData.cardType)
        {
            case EnumCardType.ATTACK:
                attackCardUI.gameObject.SetActive(true);
                break;
            case EnumCardType.UTILITY:
                utilityCardUI.gameObject.SetActive(true);
                break;
            case EnumCardType.STATUS_EFFECT:
                statusEffectCardUI.gameObject.SetActive(true);
                break;
        }
    }

    

    private void HideTemplates()
    {
        attackCardUI.gameObject.SetActive(false);
        utilityCardUI.gameObject.SetActive(false);
        statusEffectCardUI.gameObject.SetActive(false);
    }
}
