using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapController : MonoBehaviour
{
    public Transform leftSlapPoint;
    public Transform rightSlapPoint;
    public LayerMask enemyLayer;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //DetectAndSlapEnemy();
    }

    void DetectAndSlapEnemy()
    {
        RaycastHit hit;
        // RaycastHit2D leftHit = Physics2D.Raycast(leftSlapPoint.position, this.transform.TransformDirection(Vector2.left), 3f, 6);
        // RaycastHit2D rightHit = Physics2D.Raycast(rightSlapPoint.position,this.transform.TransformDirection( Vector2.right), 3f, 6);
        bool leftHit = Physics.Raycast(leftSlapPoint.position, Vector3.left,out hit, 3f, 6);
        bool  rightHit = Physics.Raycast(rightSlapPoint.position, Vector3.right, out hit, 3f, 6);
        Debug.Log("layer left hit" + leftHit);
        Debug.Log("layer right hit" + rightHit);
        Debug.Log("layer hit" + enemyLayer);
        Debug.Log("left hit collider " + leftHit);
        Debug.Log("right hit collider " + rightHit);

        Debug.DrawRay(leftSlapPoint.position, this.transform.TransformDirection(Vector2.left) * 1f, Color.red);
        Debug.DrawRay(rightSlapPoint.position, this.transform.TransformDirection(Vector2.right) * 1f, Color.blue);

        if (leftHit&&!rightHit)
        {
            // Slap animation for left side enemy
            animator.SetBool("SlapLeft",true);
            Debug.Log("lefthit worked");
        }
        else if (rightHit&&!leftHit)
        {
            // Slap animation for right side enemy
            animator.SetBool("Slap2",true);
            Debug.Log("righthit worked");
        }

       
    }
}
