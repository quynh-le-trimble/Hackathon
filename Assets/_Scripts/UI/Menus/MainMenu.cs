using Mirror;
using UnityEngine;

namespace Hackathon
{
    public class MainMenu : Menu<MainMenu>
    {

        public void StartHost()
        {
            var manager = GameObject.FindObjectOfType<NetworkRoomManagerExt>();
            manager.StartHost();

            LobbyMenu.Open();

        }

        public void StartClient()
        {
            var manager = GameObject.FindObjectOfType<NetworkRoomManagerExt>();
            manager.StartClient();

            LobbyMenu.Open();
        }


    }
}
