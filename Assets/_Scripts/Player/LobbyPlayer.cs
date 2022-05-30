using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace Hackathon
{
    public class LobbyPlayer : NetworkRoomPlayer
    {

        public GameObject m_LobbyActions;
        public Button m_ButtonReady;
        public Button m_ButtonStart;

        public void OnReadyPressed()
        {

        }

        public void OnStartPressed()
        {

        }

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();

            m_LobbyActions.SetActive(true);
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
