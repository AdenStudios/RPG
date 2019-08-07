using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IHealth
{
    private int maxHealth = 1000;
    private int health = 1000;
    public int curHealth { get { return health; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
