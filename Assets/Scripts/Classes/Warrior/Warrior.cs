using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : ICharacterClass
{
    public int baseMaxHealth { get { return 150; } }
    public int baseMaxStamina { get { return 100; }}
    public ClassType classType { get { return ClassType.Warrior; } }

    public void Attack(int id)
    {
        
    }
}
