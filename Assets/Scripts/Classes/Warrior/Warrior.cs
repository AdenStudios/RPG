using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : ICharacterClass
{
    public int baseMaxHealth { get { return 150; } }
    public int baseMaxStamina { get { return 100; }}

    public void Attack(int id)
    {
        
    }
}
