using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderMoveFirst : MonoBehaviour
{
    public float rotationSpeed = 2f;
    public float orbitRadius = 0.2f;
    public float orbitSpeed = 0.01f;

    public Vector3 orbitCenter = new Vector3(-0.866f, 4.913f, -3.739f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotation movement so the spider faces the direction it travels
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * -1);

        //Move the spider in a circular orbit
        float angle = Time.time * orbitSpeed;
        float x = orbitCenter.x + Mathf.Cos(angle) * orbitRadius;
        float z = orbitCenter.z + Mathf.Sin(angle) * orbitRadius;
        transform.position = new Vector3(x, 4.913f, z);
    }
}
