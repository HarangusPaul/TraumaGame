using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeBLE : MonoBehaviour
{
    
    public Transform zone;
    
    void Update()
    {
        float dist = Vector3.Distance(zone.position, transform.position);
        if (dist < 1)
        {
            GameManager.instance.removeObject();
        }
    }
}
