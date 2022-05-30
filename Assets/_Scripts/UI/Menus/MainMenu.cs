using Mirror;
using UnityEngine;

namespace Hackathon
{
    public class MainMenu : Menu<MainMenu>
    {
        private NetworkRoomManagerExt manager;

        void Start()
        {
            manager = GameObject.FindObjectOfType<NetworkRoomManagerExt>();
        }

        public void StartHost()
        {
            manager.StartHost();
            LobbyMenu.Open();
        }

        public void StartClient()
        {
            manager.StartClient();
            LobbyMenu.Open();
        }

        
    }
}
