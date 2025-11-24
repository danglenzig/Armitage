using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "DeckDataSO", menuName = "Cards/Deck Data")]
public class DeckDataSO : ScriptableObject
{
    [SerializeField] private List<CardSO> cards;
    public List<CardSO> Cards { get => cards; }
}
