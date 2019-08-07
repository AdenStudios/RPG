using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 followPos = FindObjectOfType<Player>().transform.position;
        followPos.y = 10;
        transform.position = followPos;
    }
}
