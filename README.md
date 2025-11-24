# Project Armitage
## Overview
_Armitage_ (an internal project name, game title TBD) is a turn-based combat prototype that blends card play and dice mechanics. Each combatant alternates turns, spending Action Points (AP) to play cards that perform attacks or utility actions. Dice rolls determine combat outcomes.

The system is deterministic in structure but probabilistic in outcome: all randomization is driven by six-sided dice (D6).

One-versus-one, and party-versus-party encounter modes are both supported.

## **Core Concepts**

### **Combatant**

A *Combatant* represents any entity (player or AI) that participates in battle.

**Attributes**

| Attribute | Description |
| --- | --- |
| **Health (HTH)** | Current and maximum vitality. Combatant is defeated when ≤ 0. |
| **Evasion (EVA)** | Target number that incoming attack rolls must meet or exceed to hit. |
| **Resilience (RES)** | Amount subtracted from damage rolls while active. |
| **Toughness (TUF)** | Number of successful hits that benefit from RES. After this many, RES is set to 0 for the remainder of the encounter. |

**Transient Values**

- **hitCounter:** Number of successful hits sustained this encounter.
- **deck, hand, discardPile:** Collections of cards.
- **AP:** Current action points (resets each turn).


