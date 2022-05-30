using Mirror;
using UnityEngine;

namespace Hackathon
{
    public class LobbyMenu : Menu<LobbyMenu>
    {
        private NetworkRoomManagerExt manager;

        void Start()
        {
            manager = GameObject.FindObjectOfType<NetworkRoomManagerExt>();
        }

        public void ReadyUp()
        {

        }

        public void StartGame()
        {
            manager.ServerChangeScene(manager.GameplayScene);
        
        }
    }
}
