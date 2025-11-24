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
| **Initiative** | Determines turn order. |

**Transient Values**

- **hitCounter:** Number of successful hits sustained this encounter.
- **deck, hand, discardPile:** Collections of cards.
- **AP:** Current action points (resets each turn).

## **Card System**

Each Combatant has a *deck* that defines their capabilities.

### **Deck Management Rules**

- Start of encounter: shuffle full deck, draw 5 cards.
- At end of turn: discard all played cards, then draw until hand = 5.
- If draw pile is empty, reshuffle discard pile into a new draw pile.

### **Card Types**

1. **Attack Cards:** Perform offensive actions that require a hit roll.
2. **Utility Cards:** Perform non-offensive actions (healing, recovery, buffs).

Each card defines:

- **Name / ID**
- **Action Point Cost**
- **Effect Function** (logic executed when played)
- **Optional Modifiers** (to hit chance, damage roll, status effect, etc.)

## **Turn Economy**

Each turn, a combatant:

1. Starts with 3 AP.
2. May play cards in any order, respecting AP limits.
3. May not exceed 3 AP spent.
4. End of turn: discard played cards, draw up to 5.

Turns alternate between combatants until one is defeated.

---

## **Attack Resolution**

When an attack card is played:

1. **Spend AP** according to card cost.
2. **Roll Attack:** roll 3×D6 → sum = *attackRoll*.
3. **Compare:** if *attackRoll ≥ target.EVA* → hit, else miss.
4. **On Hit:**
    - if *target.hitCounter < target.TUF*:
        - Roll 1×D6 → *damageRoll*.
        - *damage = max(0, damageRoll − target.RES)*.
        - Increment *hitCounter*.
    - else (RES expired):
        - Roll 1×D6 → *damage = damageRoll*.
5. **Apply Damage:** subtract *damage* from target.HTH.
6. **Check Death:** if target.HTH ≤ 0 → target defeated.

## **Critical Rolls**

Criticals are determined during the 3×D6 attack roll.

| Roll Type | Condition | Notes |
| --- | --- | --- |
| **Critical Hit (CRIT)** | All three dice show the same value. | Roughly 3% probability. |
| **Critical-Lite (CRIT-L)** | Exactly two dice show the same value. | Roughly 14% probability. |

**Effect Handling**

- *CRIT:* triggers major effect (e.g. bonus damage, status, free action).
- *CRIT-L:* triggers lesser effect (e.g. +1 damage, minor buff).
- Effect behavior to be defined in v0.2 design pass.

## **Utility Resolution**

Examples (baseline implementations):

**Basic Heal**

- Cost: 1 AP
- Effect: Roll 1×D6 → recover that many HTH (capped at max).

**Basic Resolve**

- Cost: 2 AP
- Effect: Reduce hitCounter by 1, restoring partial RES protection.

## **Combat Loop Pseudocode**

```
initialize encounter
while (both combatants alive):
    for each combatant in turn order:
        reset AP to 3
        draw until hand = 5 (reshuffle if needed)
        while (AP > 0 and player has playable cards):
            choose card to play
            resolve card effect
            deduct AP
        discard played cards
        check victory conditions
end loop

```

---

## **Randomization**

All randomness derives from D6 dice. Each roll event is independent.

Dice results are discrete integers [1–6].

System should expose a deterministic seedable random source for debugging or replay.

---

## **Failure & Victory Conditions**

A combatant is **defeated** when `HTH ≤ 0`.

Encounter ends immediately when only one combatant remains alive.

---

## **Future Expansion Hooks**

(Not in v0.1 implementation)

- Additional dice manipulation mechanics (advantage/disadvantage, rerolls).
- Card synergies (combos, chain effects).
- Buff/debuff status systems.
- Enemy AI heuristics.
- Deck construction or loadout customization.
