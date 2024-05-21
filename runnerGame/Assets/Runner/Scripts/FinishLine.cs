using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// Ends the game on collision, forcing a win state.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Collider))]
    public class FinishLine : Spawnable
    {
        const string k_PlayerTag = "Player";
        public bool IsLsttimeAnim = false;
        
        void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag(k_PlayerTag))
            {
                if (IsLsttimeAnim == true)
                {
                    Debug.Log("lastTimeShowsanim");
                    // FindObjectOfType<CameraManager>().m_CameraAnglePreset = CameraManager.CameraAnglePreset.Side;
                    FindObjectOfType<CameraManager>().finishoffset();
                    FindObjectOfType<PlayerController>().PlayerStop();
                    StartCoroutine(IslasttimeAnim());
                    //lastcheckanimation
                    //need to add camera angle
                    //playerspeed stop
                }
                else
                {
                    GameManager.Instance.Win();
                }
               
            }
        }


        public IEnumerator IslasttimeAnim()
        {
            yield return new WaitForSeconds(.65f);
            FindObjectOfType<PlayerController>().jumpunch();
            yield return new WaitForSeconds(5f);
            FindObjectOfType<PlayerController>().FinshStop();
            GameManager.Instance.Win();
            Debug.Log("new anim play");
        }
    }
}