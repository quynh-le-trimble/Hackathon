using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using System.Linq;
using System;

namespace Hackathon
{
    public class GameManager : NetworkStaticInstance<GameManager>
    {

        public TextMeshProUGUI m_notificationText;

        [SyncVar]
        public int m_playerCount = 0;

        private NetworkRoomManagerExt m_networkManager;
        private List<PlayerController> allPlayers;
        private PlayerController currentPlayer;
        private GameMode m_currentGameMode = new Classic();
        private TurnManager turnManager = new TurnManager();
        private WordManager wordManager;
        private int roundNumber = 1;
        private int MaxRounds = 5;

        private void Start()
        {
            m_currentGameMode.Start(this);
            wordManager = WordManager.Instance;

            m_networkManager = FindObjectOfType<NetworkRoomManagerExt>();
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


            yield return new WaitForSeconds(5);

            allPlayers = GameObject.FindObjectsOfType<PlayerController>(true).ToList();
            
            // m_playerCount = allPlayers.Length;

            yield return new WaitForSeconds(5);
        }

        private void SetActivePlayer(PlayerController currentPlayer)
        {
            allPlayers.ForEach(p => p._isActiveDrawer = false);
            currentPlayer._isActiveDrawer = true;
        }

        IEnumerator PlayGame()
        {
            if (m_notificationText != null)
            {
                m_notificationText.gameObject.SetActive(false);
            }

            while (roundNumber != MaxRounds)
            {
                Debug.Log("Round " + roundNumber);
                for (int i = 0; i < allPlayers.Count(); i++)
                {
                    currentPlayer = turnManager.GetNextPlayer(allPlayers);
                    SetActivePlayer(currentPlayer);

                    Debug.Log("Current Player is: " + currentPlayer.connectionToClient.connectionId);
                    yield return new WaitForSeconds(2);
                }
                roundNumber++;

                yield return new WaitForSeconds(5);
            }

            SetPlayerState(true);
            yield return new WaitForSeconds(10);
        }
        IEnumerator EndGame()
        {
            Debug.Log("Game Over");
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
            // foreach (var player in allPlayers)
            // {
            //     player.enabled = state;
            // }
        }

        public void SwitchModes(GameMode mode)
        {
            m_currentGameMode = mode;
            m_currentGameMode.Start(this);
        }
    }
}
