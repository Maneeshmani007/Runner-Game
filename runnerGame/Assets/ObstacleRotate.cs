using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotate : MonoBehaviour
{
    public float z=0;
    public float y=0;
    public float x=0;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(x* speed * Time.deltaTime, y * speed * Time.deltaTime, z* speed * Time.deltaTime);
    }
}
