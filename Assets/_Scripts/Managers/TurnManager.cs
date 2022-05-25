using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;

namespace Hackathon
{
    public class TurnManager : Singleton<TurnManager>
    {
        [SerializeField] private List<PlayerController> _players;
        public PlayerController _currentPlayer;
        [SerializeField] private TimerSystem _timer;
        public NetworkRoomManager roomManager;

        void Start()
        {
            roomManager = (NetworkRoomManager)GameObject.FindObjectOfType(typeof(NetworkRoomManager));
            if (roomManager == null) return;
        }

        // Update is called once per frame
        void Update()
        {
            if (_players.Count != roomManager.numPlayers)
            {
                _players = roomManager.gamePlayers.Select(player => player.GetComponent<PlayerController>()).ToList();
            }
            else if (!_timer.timerIsRunning && _players.Count == roomManager.numPlayers)
            {
                GetNextPlayer(_players);
                //Do preround setup here
                //current player is given word choices and picks word

                _timer.CmdStartTimer();
            }
        }

        public void GetNextPlayer(List<PlayerController> players)
        {
            if (players == null) return;
            _currentPlayer = players.First();
            players.Add(players.First());
            players.Remove(players.First());

            Debug.Log("Current Player: " + _currentPlayer._playerName);
        }
    }
}
