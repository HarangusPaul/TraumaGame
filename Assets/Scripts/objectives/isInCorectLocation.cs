using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isInCorectLocation : MonoBehaviour
{
    public Transform position;
    public int task;

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(position.position, transform.position);
        if (dist < 1)
        {
            GameManager.instance.updateTask(task);
        }
    }
}
