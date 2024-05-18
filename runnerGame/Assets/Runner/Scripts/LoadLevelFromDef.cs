using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HyperCasual.Runner
{
    public class LoadLevelFromDef : AbstractState
    {
        public readonly LevelDefinition m_LevelDefinition;
        readonly SceneController m_SceneController;
        readonly GameObject[] m_ManagerPrefabs;

        public LoadLevelFromDef(SceneController sceneController, AbstractLevelData levelData, GameObject[] managerPrefabs)
        {
            if (levelData is LevelDefinition levelDefinition)
                m_LevelDefinition = levelDefinition;

            m_ManagerPrefabs = managerPrefabs;
            m_SceneController = sceneController;
        }
        
        public override IEnumerator Execute()
        {
            if (m_LevelDefinition == null)
                throw new Exception($"{nameof(m_LevelDefinition)} is null!");

            yield return m_SceneController.LoadNewScene(nameof(m_LevelDefinition));
            RenderSettings.skybox = m_LevelDefinition.skyBoxMat;

            RenderSettings.subtractiveShadowColor = Color.white;

            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientLight = new Color32(195, 195, 195, 255);


            RenderSettings.fogColor = Color.white;
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 250;
            RenderSettings.fogEndDistance = 300;

            // Load managers specific to the level
            foreach (var prefab in m_ManagerPrefabs)
            {
                GameObject obj =  Object.Instantiate(prefab);
                //var player = obj.GetComponent<PlayerController>();
                //if(player != null)
                //{
                //    CineCameraContrller.Instance.CMvcam1.Follow = player.CameraTarget.transform;
                //    //CineCameraContrller.Instance.CMvcam1.LookAt = player.CameraTarget.transform;
                //}
            }

            GameManager.Instance.LoadLevel(m_LevelDefinition);
        }
    }
}