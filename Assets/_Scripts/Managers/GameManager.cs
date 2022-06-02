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
        public List<PlayerController> players;
        public Dictionary<uint, PlayerController> allPlayers = new Dictionary<uint, PlayerController>();
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
            yield return StartCoroutine("JoiningGame");
            yield return StartCoroutine("EnterGame");
            yield return StartCoroutine("PlayGame");
            yield return StartCoroutine("EndGame");
        }

        IEnumerator JoiningGame()
        {
            GameMenu.Instance.m_notificationText.gameObject.SetActive(true);
            GameMenu.Instance.m_notificationText.text = "Waiting for everyone to join...";

            GameMenu.Instance.m_Timer.m_TimeRemaining = 3f;
            GameMenu.Instance.m_Timer.StartTimer();
            yield return new WaitForSeconds(3);
            players = GameObject.FindObjectsOfType<PlayerController>(true).ToList();

            foreach (var player in players)
            {
                allPlayers.Add(player.netId, player);
            }

            m_playerCount = players.Count;
        }

        IEnumerator EnterGame()
        {
            GameMenu.Instance.m_notificationText.text = "All Players joined, Entering Game";

            GameMenu.Instance.m_Timer.m_TimeRemaining = 3f;
            GameMenu.Instance.m_Timer.StartTimer();
            yield return new WaitForSeconds(3);
        }

        private void SetActivePlayer(PlayerController currentPlayer)
        {
            players.ForEach(p => 
            {
                p._isActiveDrawer = false;
                p._isSelectingWord = false;
            });
            currentPlayer._isActiveDrawer = true;
            currentPlayer._isSelectingWord = true;
        }

        IEnumerator PlayGame()
        {
            GameMenu.Instance.m_notificationText.gameObject.SetActive(false);

            while (roundNumber != MaxRounds)
            {
                if (GameMenu.Instance != null)
                {
                    GameMenu.Instance.m_SelectedWordText.text = roundNumber.ToString();
                }

                for (int i = 0; i < allPlayers.Count(); i++)
                {
                    currentPlayer = turnManager.GetNextPlayer(players);
                    SetActivePlayer(currentPlayer);
                  

                    // Wait until word is selected
                    GameMenu.Instance.m_Timer.m_TimeRemaining = 5f;
                    GameMenu.Instance.m_Timer.StartTimer();
                    yield return new WaitForSeconds(5);
                    currentPlayer._isSelectingWord = false;

                    GameMenu.Instance.m_Timer.m_TimeRemaining = 5f;
                    GameMenu.Instance.m_Timer.StartTimer();
                    yield return new WaitForSeconds(5);

                    //Debug.Log(GameMenu.Instance.m_Timer.timerIsRunning);
                    // Wait until timer is over
                    // while (GameMenu.Instance.m_Timer.timerIsRunning)
                    // {
                    //     //play game
                    //     Debug.Log("Playing game");
                    // }
                }
                roundNumber++;
            }

            SetPlayerState(true);
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

        public void TargetWordChoices(NetworkConnection conn, string[] words) {

        }

    }
}
