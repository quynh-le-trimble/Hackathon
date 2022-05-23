using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

namespace Hackathon
{
    public class GameManager : NetworkStaticInstance<GameManager>
    {

        public TextMeshProUGUI m_notificationText;

        [SyncVar]
        public int m_playerCount = 0;

        private NetworkRoomManagerExt m_networkManager;
        private PlayerController[] allPlayers;
        private GameMode m_currentGameMode = new Classic();

        private void Start()
        {
            m_currentGameMode.Start(this);


            m_networkManager = FindObjectOfType<NetworkRoomManagerExt>();
            allPlayers = GameObject.FindObjectsOfType<PlayerController>(true);
            m_playerCount = allPlayers.Length;
            StartCoroutine("GameLoop");
        }

        IEnumerator GameLoop()
        {
            yield return StartCoroutine("EnterGame");
            yield return StartCoroutine("PlayGame");
            yield return StartCoroutine("EndGame");
        }

        IEnumerator EnterGame()
        {
            if (m_notificationText != null)
            {
                m_notificationText.text = "READY?";
                m_notificationText.gameObject.SetActive(true);
            }

            SetPlayerState(false);
            yield return new WaitForSeconds(5);
        }
        IEnumerator PlayGame()
        {
            if (m_notificationText != null)
            {
                m_notificationText.gameObject.SetActive(false);
            }
            SetPlayerState(true);
            yield return new WaitForSeconds(10);
        }
        IEnumerator EndGame()
        {
            if (m_notificationText != null)
            {
                m_notificationText.text = "WINNER!";
                m_notificationText.gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(10);
            m_networkManager.ServerChangeScene(m_networkManager.RoomScene);

        }

        void SetPlayerState(bool state)
        {
            foreach (var player in allPlayers)
            {
                player.enabled = state;
            }
        }

        public void SwitchModes(GameMode mode) {
            m_currentGameMode = mode;
            m_currentGameMode.Start(this);        }
    }
}
