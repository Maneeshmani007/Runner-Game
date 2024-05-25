using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HyperCasual.Runner;
using UnityEngine;
using UnityEngine.UI;

public class AnimChanger : MonoBehaviour
{
    public static AnimChanger Instance => s_Instance;
    static AnimChanger s_Instance;
    public Animator charaAnim;
    public bool LeftSide;
    public bool RigthSide;
    public bool camforward;
    public bool lastmanAnim;
    Transform m_Transform;
    Vector3 m_Scale;
    float m_StartHeight;
    public Text Levlnum;
    public int EnemyLevlNum;





    public Rigidbody ragdoll; // Assign this in the Inspector
    public float forceAmount = 1000f;
    public Vector3 forceDirection = Vector3.up;

    public virtual void ResetSpawnable() { }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Invoke("RagdollDelay", .5f);
            //charaAnim.enabled = false;
            //ragdoll.AddForce(forceDirection * forceAmount);
        }
    }
    public void RagdollDelay()
    {
        charaAnim.enabled = false;
        ragdoll.AddForce(forceDirection * forceAmount);
        AudioManager.Instance.PlaySlapSound();
        if (camforward == true)
        {
            //FindObjectOfType<CameraManager>().
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        string stringValue = EnemyLevlNum.ToString();
        Levlnum.text = stringValue;
        charaAnim.enabled = true;
        camforward = false;
    }


    
    // Update is called once per frame
    void Update()
    {
        
    }
    float v = .25f;
    private void OnTriggerEnter(Collider other)
    {
       

        PlayerController playerControllerInstance = FindObjectsOfType<PlayerController>()
   .FirstOrDefault(pc => pc.gameObject.CompareTag("Player"));

        if (playerControllerInstance != null)
        {
            int playerCoinNum = playerControllerInstance.playeerCoinnum;
            // Proceed with your logic using playerCoinNum
        }
        else
        {
            Debug.LogError("PlayerController not found.");
        }

        if (other.gameObject.CompareTag("Player"))
        {
            // Corrected typo in accessing playerCoinnum
            int playerCoinNum = FindObjectOfType<PlayerController>().playeerCoinnum;
            int enemyLevelNum = EnemyLevlNum;

            if (lastmanAnim == false)
            {
                Enemypunchchecker(playerCoinNum, enemyLevelNum);
            }
            else if (lastmanAnim == true)
            {
                camforward = true;
                Invoke("RagdollDelay", 5f);
            }
        }
    }

    
    public void Enemypunchchecker(int playerCoinNum, int enemyLevelNum)
    {
        if (enemyLevelNum >= playerCoinNum)
        {
            charaAnim.SetBool("EnemySlap", true);
            charaAnim.SetBool("Slapped", false);
            Debug.Log($"Enemy Level Num: {enemyLevelNum}, Player Coin Num: {playerCoinNum}");
            DeadStop();
            //Invoke("DeadStop", .25f);

            //Debug.Log($"Enemy Level Num: {enemyLevelNum}, Player Coin Num: {playerCoinNum}");
            //Invoke("RagdollDelay", v); // Assuming 'v' is defined somewhere in your class
        }
        else
        {
            //charaAnim.SetBool("EnemySlap", true);
            //Debug.Log($"Enemy Level Num: {enemyLevelNum}, Player Coin Num: {playerCoinNum}");
            //Invoke("DeadStop", .75f);

            Debug.Log($"Enemy Level Num: {enemyLevelNum}, Player Coin Num: {playerCoinNum}");
            Invoke("RagdollDelay", v); // Assuming 'v' is defined somewhere in your class
        }
    }

    public void DeadStop()
    {
        FindObjectOfType<PlayerController>().Deadstop();
    }
    ///////
    ///
    public Vector3 GetPlayerTop()
    {
        return m_Transform.position + Vector3.up;
    }

}
