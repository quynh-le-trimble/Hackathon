using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;

namespace Hackathon
{
    public class TurnManager : Singleton<TurnManager>
    {
        [SerializeField] private List<uint> _playerIds;
        public uint _currentPlayerId;
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
            if (_playerIds.Count != roomManager.numPlayers)
            {
                _playerIds = roomManager.gamePlayers.Select(player => player.GetComponent<NetworkIdentity>().netId).ToList();
            }
            else if (!_timer.timerIsRunning && _playerIds.Count == roomManager.numPlayers)
            {
                GetNextPlayer(_playerIds);
                //Do preround setup here

                _timer.StartTimer();
            }
        }

        public void GetNextPlayer(List<uint> players)
        {
            if (players == null) return;
            _currentPlayerId = players.First();
            players.Add(players.First());
            players.Remove(players.First());

            Debug.Log("Current Player: " + _currentPlayerId);
        }
    }
}
