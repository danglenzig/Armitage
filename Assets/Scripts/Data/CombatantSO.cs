using UnityEngine;
using GameTools;

[CreateAssetMenu(fileName = "CombatantSO", menuName = "Combatants/Combatant")]
public class CombatantSO : ScriptableObject
{
    [SerializeField] private string combatantName = Constants.DEFAULT_COMBATANT_NAME;
    [SerializeField] private int baseMaxHTH = Constants.DEFAULT_BASE_HTH;
    [SerializeField] private int baseEVA = Constants.DEFAULT_BASE_EVA;
    [SerializeField] private int baseRES = Constants.DEFAULT_BASE_RES;
    [SerializeField] private int baseTUF = Constants.DEFAULT_BASE_TUF;
    [SerializeField] private int baseAP = Constants.DEFAULT_AP;
    [Range(0, 10)] [SerializeField] private int initiative = 0;
    [SerializeField] private EnumCombatantControl control = EnumCombatantControl.PLAYER;

    private string combatantID = Constants.UNSET_COMBATANT_ID;
    private StructCombatantData myData;
    public StructCombatantData Data { get => myData; }

    private void OnValidate()
    {
        if (combatantID == Constants.UNSET_COMBATANT_ID || string.IsNullOrEmpty(combatantID))
        {
            combatantID = System.Guid.NewGuid().ToString();
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        myData = new StructCombatantData
            (
                combatantName,
                combatantID,
                baseMaxHTH,
                baseEVA,
                baseRES,
                baseTUF,
                baseAP,
                initiative,
                control
            );
    }
    public void UpdateCombatantData(StructCombatantData _data)
    {
        myData = _data;
    }
}
