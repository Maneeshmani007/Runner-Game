using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimChanger : MonoBehaviour
{
    public Animator charaAnim;
    public bool LeftSide;
    public bool RigthSide;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!LeftSide && RigthSide)
        {
           // charaAnim.SetTrigger("mixamo.com (1)");
            Debug.Log("leftSideWorked");
        }
        else if(LeftSide && !RigthSide)
        {
            Debug.Log("RightSideWorked");
        }
    }
}
