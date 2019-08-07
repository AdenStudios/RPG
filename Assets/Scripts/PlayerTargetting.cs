using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetting : MonoBehaviour
{
    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        player.AddTaraget(other.gameObject);
    }

    private void OnTriggerExit(Collider other) 
    {
        player.RemoveTarget(other.gameObject);
    }
}
