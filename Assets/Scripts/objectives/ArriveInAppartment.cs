using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveInAppartment : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;

    public int task;

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        if (dist < 1)
        {
            GameManager.instance.updateTask(task);
        }
    }
}
