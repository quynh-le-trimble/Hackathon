using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;

namespace Hackathon
{
    public class TurnManager : Singleton<TurnManager>
    {
        public PlayerController _currentPlayer;
        public NetworkRoomManager roomManager;
        private List<PlayerController> _players = new List<PlayerController>();

        public PlayerController GetNextPlayer(List<PlayerController> players)
        {
            if (_players.Count == 0)
            {
                _players = players;
            }

            _currentPlayer = _players.First();
            _players.Add(_players.First());
            _players.Remove(_players.First());

            Debug.Log("Current Player: " + _currentPlayer._playerName);
            return _currentPlayer;
        }
    }
}
