using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A class used to manage camera movement
    /// in a Runner game.
    /// </summary>
    [ExecuteInEditMode]
    public class CameraManager : MonoBehaviour
    {
        /// <summary>
        /// Returns the CameraManager.
        /// </summary>
        public static CameraManager Instance => s_Instance;
        static CameraManager s_Instance;

        [SerializeField]
        public CameraAnglePreset m_CameraAnglePreset = CameraAnglePreset.Behind;

        [SerializeField]
        Vector3 m_Offset;

        [SerializeField]
        Vector3 m_LookAtOffset;

        [SerializeField]
        bool m_LockCameraPosition;

        [SerializeField]
        bool m_SmoothCameraFollow;

        [SerializeField]
        float m_SmoothCameraFollowStrength = 10.0f;

         public enum CameraAnglePreset
        {
            Behind,
            Overhead,
            Side,
            FirstPerson,
            Custom,
            //Finishpos
        }

        Vector3[] m_PresetOffsets = new Vector3[]
        {
            new Vector3(0.0f, 5.0f, -9.0f), // Behind
            new Vector3(0.0f, 9.0f, -5.0f), // Overhead
            new Vector3(5.0f, 5.0f, -8.0f), // Side
            new Vector3(0.0f, 1.0f, 0.0f),  // FirstPerson
            new Vector3(-0.56f, 7.09f, -8.03f)       // Custom
            //new Vector3(0.10f,9.0f,-5.0f)
        };

        Vector3[] m_PresetLookAtOffsets = new Vector3[]
        {
            new Vector3(0.0f, 2.0f, 6.0f),  // Behind
            new Vector3(0.0f, 0.0f, 4.0f),  // Overhead
            new Vector3(-0.5f, 1.0f, 2.0f), // Side
            new Vector4(0.0f, 1.0f, 2.0f),  // FirstPerson
            new Vector3(-1.13f, 2.44f, 6.82f)   // Custom
            //new Vector3(0.10f, 0.0f, 4.0f)
        };

        bool[] m_PresetLockCameraPosition = new bool[]
        {
            false, // Behind
            false, // Overhead
            true,  // Side
            false, // FirstPerson
            false,  // Custom
            //false  // Finishcampos
        };

        Transform m_Transform;
        Vector3 m_PrevLookAtOffset;

        static readonly Vector3 k_CenteredScale = new Vector3(0.0f, 1.0f, 1.0f);

        void Awake()
        {
            SetupInstance();
            RenderSettings.fog = true;
           // startOffset();
        }
        public void startOffset()
        {
            m_Offset = new Vector3(0f, 0.35f, -1.9f);
            m_LookAtOffset = new Vector3(0f, 1.0f, -10f);
            //isTransitioningToFinishOffset = false;
        }
        /// <summary>
        /// /smooth  cam flow add
        /// </summary>

        //private bool isTransitioningToFinishOffset = false; // Flag to track if we're transitioning to the finish offset

        //void SetCameraPositionAndOrientation1(bool smoothCameraFollow)
        //{
        //    Vector3 playerPosition = GetPlayerPosition();

        //    int presetIndex = (int)m_CameraAnglePreset;
        //    if (presetIndex >= 0 && presetIndex < m_PresetOffsets.Length)
        //    {
        //        Vector3 offset = playerPosition + m_PresetOffsets[presetIndex] + m_Offset;
        //        Vector3 lookAtOffset = playerPosition + m_PresetLookAtOffsets[presetIndex] + m_LookAtOffset;

        //        if (isTransitioningToFinishOffset)
        //        {
        //            // Smoothly transition to the finish offset
        //            float lerpAmount = Time.deltaTime * m_SmoothCameraFollowStrength;
        //            m_Transform.position = Vector3.Lerp(m_Transform.position, offset, lerpAmount);
        //            m_Transform.LookAt(Vector3.Lerp(m_Transform.position + m_Transform.forward, lookAtOffset, lerpAmount));
        //        }
        //        else
        //        {
        //            // Normal camera movement
        //            if (smoothCameraFollow)
        //            {
        //                float lerpAmount = Time.deltaTime * m_SmoothCameraFollowStrength;
        //                m_Transform.position = Vector3.Lerp(m_Transform.position, offset, lerpAmount);
        //                m_Transform.LookAt(Vector3.Lerp(m_Transform.position + m_Transform.forward, lookAtOffset, lerpAmount));
        //            }
        //            else
        //            {
        //                m_Transform.position = playerPosition + m_PresetOffsets[presetIndex] + m_Offset;
        //                m_Transform.LookAt(lookAtOffset);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogError("Invalid Camera Angle Preset selected.");
        //    }
        //}

        public void finishoffset()
        {
            m_Offset = new Vector3(5.0f, 5.0f, -4.12f);
            m_LookAtOffset = new Vector3(-0.5f, 1.0f, 2.0f);
            //isTransitioningToFinishOffset = true; // Start the transition to the finish offset
        }




        ///////
        //public void finishoffset()
        //{
        //    m_Offset = new Vector3(5.0f, 5.0f, -8.0f); 
        //    m_LookAtOffset = new Vector3(-0.5f, 1.0f, 2.0f);
        //   // m_Offset = new Vector3(-0.56f, 7.09f, -8.03f);
        //   // m_LookAtOffset = new Vector3(-1.13f, 2.44f, 6.82f);
        //}
        void OnEnable()
        {
            SetupInstance();
        }

        void SetupInstance()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_Instance = this;
            m_Transform = transform;
        }

        /// <summary>
        /// Reset the camera to its starting position relative
        /// to the player.
        /// </summary>
        public void ResetCamera()
        {
            SetCameraPositionAndOrientation(false);
        }

        Vector3 GetCameraOffset()
        {
            return m_PresetOffsets[(int)m_CameraAnglePreset] + m_Offset;
        }

        Vector3 GetCameraLookAtOffset()
        {
            return m_PresetLookAtOffsets[(int)m_CameraAnglePreset] + m_LookAtOffset;
        }

        bool GetCameraLockStatus()
        {
            if (m_LockCameraPosition)
            {
                return true;
            }

            return m_PresetLockCameraPosition[(int)m_CameraAnglePreset];
        }

        Vector3 GetPlayerPosition()
        {
            Vector3 playerPosition = Vector3.up;
            if (PlayerController.Instance != null) 
            {
                playerPosition = PlayerController.Instance.GetPlayerTop();
            }

            if (GetCameraLockStatus())
            {
                playerPosition = Vector3.Scale(playerPosition, k_CenteredScale);
            }

            return playerPosition;
        }
        Vector3 GetEnemyPosition()
        {
            Vector3 playerPosition = Vector3.up;
            if (PlayerController.Instance != null)
            {
                //playerPosition = AnimChanger.Instance.GetPlayerTop();
            }

            if (GetCameraLockStatus())
            {
                playerPosition = Vector3.Scale(playerPosition, k_CenteredScale);
            }

            return playerPosition;
        }

        void LateUpdate()
        {
            if (m_Transform == null)
            {
                return;
            }

            SetCameraPositionAndOrientation(m_SmoothCameraFollow);
            //SetCameraPositionAndOrientation1(isTransitioningToFinishOffset);        ///new added
        }

        void SetCameraPositionAndOrientation(bool smoothCameraFollow)
        {
            //if (PlayerController.Instance.lastEnemy.GetComponent<AnimChanger>().camforward == true)
            //{
            //    Debug.Log("smooth flow entered");
            //    Vector3 playerPosition = GetEnemyPosition();
            //    Vector3 offset = playerPosition + GetCameraOffset();
            //    Vector3 lookAtOffset = playerPosition + GetCameraLookAtOffset();

            //    if (smoothCameraFollow)
            //    {
            //        float lerpAmound = Time.deltaTime * m_SmoothCameraFollowStrength;

            //        m_Transform.position = Vector3.Lerp(m_Transform.position, offset, lerpAmound);
            //        m_Transform.LookAt(Vector3.Lerp(m_Transform.position + m_Transform.forward, lookAtOffset, lerpAmound));

            //        m_Transform.position = new Vector3(m_Transform.position.x, m_Transform.position.y, offset.z);
            //    }
            //    else
            //    {
            //        m_Transform.position = playerPosition + GetCameraOffset();
            //        m_Transform.LookAt(lookAtOffset);
            //    }
            //}
            //else
            {
                Vector3 playerPosition = GetPlayerPosition();
                Vector3 enemypo = GetEnemyPosition();

                Vector3 offset = playerPosition + GetCameraOffset();
                Vector3 lookAtOffset = playerPosition + GetCameraLookAtOffset();

                if (smoothCameraFollow)
                {
                    float lerpAmound = Time.deltaTime * m_SmoothCameraFollowStrength;

                    m_Transform.position = Vector3.Lerp(m_Transform.position, offset, lerpAmound);
                    m_Transform.LookAt(Vector3.Lerp(m_Transform.position + m_Transform.forward, lookAtOffset, lerpAmound));

                    m_Transform.position = new Vector3(m_Transform.position.x, m_Transform.position.y, offset.z);
                }
                else
                {
                    m_Transform.position = playerPosition + GetCameraOffset();
                    m_Transform.LookAt(lookAtOffset);
                }
            }

            
        }
    }
}

