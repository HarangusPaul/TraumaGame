using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openTasks : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 pos;
    public int task;
    
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != pos)
        {
            GameManager.instance.updateTask(task);
        }
    }
}
