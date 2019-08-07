using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    public string name;
    public int abilityID;
    public string animatorName;
    public int abilityDamage;
    public int attackDuration;

    public Ability(string Name, int ID, string AnimName, int Damage, int Duration)
    {
        name = Name;
        abilityID = ID;
        animatorName = AnimName;
        abilityDamage = Damage;
        attackDuration = Duration;
    }
}
