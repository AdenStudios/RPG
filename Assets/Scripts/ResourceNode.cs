using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour, IInteractable
{
    public float respawnTime;
    private float curTime = 0;
    public bool isHarvested = false;
    public int resourceID;
    public GameObject ResourceModel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHarvested)
        {
            curTime += Time.deltaTime;
            if (curTime >= respawnTime)
            {
                isHarvested = false;
                curTime = 0;
                ResourceModel.SetActive(true);
            }
        }
    }

    public void Interact()
    {
        Resource resource = ItemDatabase.instance.RetrieveItem(resourceID) as Resource;
        Inventory.instance.AddItem(resource, (Random.Range(resource.minGatherAmount, resource.maxGatherAmount)));
        isHarvested = true;
        ResourceModel.SetActive(false);
        print("added wood to inventory");
    }
}
