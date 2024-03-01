using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stuff : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject object1;
    public GameObject object2;
    public int task;


    private void Start()
    {
        object1.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(object1.transform.position,object2.transform.position);
        if (dist < 1
        .5)
        {
            object1.active = true;
            object2.active = false;
            GameManager.instance.updateTask(task);
        }
    }
}
