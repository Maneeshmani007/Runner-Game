using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using HyperCasual.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace HyperCasual.Runner
{
    /// <summary>
    /// This View contains Game-over Screen functionalities
    /// </summary>
    public class GameoverScreen : View
    {
        [SerializeField]
        HyperCasualButton m_PlayAgainButton;
        [SerializeField]
        HyperCasualButton m_GoToMainMenuButton;
        [SerializeField]
        AbstractGameEvent m_PlayAgainEvent;
        [SerializeField]
        AbstractGameEvent m_GoToMainMenuEvent;

        void OnEnable()
        {
            m_PlayAgainButton.AddListener(OnPlayAgainButtonClick);
            m_GoToMainMenuButton.AddListener(OnGoToMainMenuButtonClick);
        }

        void OnDisable()
        {
            m_PlayAgainButton.RemoveListener(OnPlayAgainButtonClick);
            m_GoToMainMenuButton.RemoveListener(OnGoToMainMenuButtonClick);
        }

        void OnPlayAgainButtonClick()
        {
            m_PlayAgainEvent.Raise();
            Invoke("tapToUpdateDelay", .5f);
        }
        void tapToUpdateDelay()
        {
            FindObjectOfType<Hud>().TaptoPlay?.SetActive(true);
        }

        void OnGoToMainMenuButtonClick()
        {
            m_GoToMainMenuEvent.Raise();
        }
    }
}