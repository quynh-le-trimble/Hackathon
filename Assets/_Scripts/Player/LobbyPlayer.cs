using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hackathon
{
    public class LobbyPlayer : NetworkRoomPlayer
    {

        public GameObject m_LobbyActions;
        public Button m_ButtonReady;
        public Button m_ButtonStart;
        public Button m_ButtonStop;

        private NetworkRoomManagerExt manager;

        public void OnReadyPressed()
        {
            if (isLocalPlayer)
            {
                CmdChangeReadyState(true);
            }

            if (manager.allPlayersReady && NetworkServer.active && NetworkClient.isConnected)
            {
                m_ButtonStart.gameObject.SetActive(true);
            }
        }

        public void OnStartPressed()
        {
            if (isLocalPlayer)
            {
                manager.ServerChangeScene(manager.GameplayScene);
            }
        }

        public void OnStopPressed()
        {
            // stop host if host mode
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                manager.StopHost();
            }
            // stop client if client-only
            else if (NetworkClient.isConnected)
            {
                manager.StopClient();
            }
            // stop server if server-only
            else if (NetworkServer.active)
            {
                manager.StopServer();
            }

            MainMenu.Open();
        }

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            manager = GameObject.FindObjectOfType<NetworkRoomManagerExt>();
            m_LobbyActions.SetActive(true);
            m_ButtonStart.gameObject.SetActive(false);

            // stop host if host mode
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                m_ButtonStop.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Stop Host";

            }
            // stop client if client-only
            else if (NetworkClient.isConnected)
            {
                m_ButtonStop.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Stop Client";
            }
            // stop server if server-only
            else if (NetworkServer.active)
            {
                m_ButtonStop.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Stop Server";
            }


            if (NetworkServer.active && NetworkClient.active)
            {
                Debug.Log($"<b>Host</b>: running via {Transport.activeTransport}");
            }
            // server only
            else if (NetworkServer.active)
            {
                Debug.Log($"<b>Server</b>: running via {Transport.activeTransport}");
            }
            // client only
            else if (NetworkClient.isConnected)
            {
                Debug.Log($"<b>Client</b>: connected to {manager.networkAddress} via {Transport.activeTransport}");
            }
        }

        void OnAllPlayersReady()
        {

        }

        public override void OnStartClient()
        {
            //Debug.Log($"OnStartClient {gameObject}");
        }

        public override void OnClientEnterRoom()
        {
            //Debug.Log($"OnClientEnterRoom {SceneManager.GetActiveScene().path}");


        }

        public override void OnClientExitRoom()
        {
            //Debug.Log($"OnClientExitRoom {SceneManager.GetActiveScene().path}");
            m_LobbyActions.SetActive(false);
        }

        public override void IndexChanged(int oldIndex, int newIndex)
        {
            //Debug.Log($"IndexChanged {newIndex}");
        }

        public override void ReadyStateChanged(bool oldReadyState, bool newReadyState)
        {
            //Debug.Log($"ReadyStateChanged {newReadyState}");
        }

        public override void OnGUI()
        {
            base.OnGUI();
        }
    }
}
