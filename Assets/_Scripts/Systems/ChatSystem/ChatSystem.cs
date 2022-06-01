using System;
using System.Linq;
using Mirror;
using TMPro;
using UnityEngine;

namespace Hackathon
{
    public class ChatSystem : NetworkBehaviour
    {
        [SerializeField] GameObject m_ChatSystemUI;
        [SerializeField] TMP_InputField m_ChatInput;
        [SerializeField] GameObject m_ChatTextPrefab;
        [SerializeField] Transform m_chatTextParent;

        private PlayerController m_playerController;

        private static event Action<string> OnMessage;

        public override void OnStartAuthority()
        {
            m_playerController = GetComponent<PlayerController>();
            m_ChatSystemUI.SetActive(true);
            OnMessage += HandleNewMessage;
        }

        private void HandleNewMessage(string message)
        {


        }

        [ClientCallback]
        private void OnDestroy()
        {
            if (!hasAuthority) { return; }
            OnMessage -= HandleNewMessage;
        }

        [Client]
        public void Send(string message)
        {
            if (!Input.GetKeyDown(KeyCode.Return)) { return; }

            if (string.IsNullOrWhiteSpace(message)) { return; }

            CmdSendMessage(m_playerController._playerName, message);
            m_ChatInput.text = string.Empty;
            m_ChatInput.ActivateInputField();
        }

        [Command]
        private void CmdSendMessage(string name, string message)
        {
            // Validation 

            var chatText = Instantiate(m_ChatTextPrefab, m_chatTextParent);

            chatText.GetComponent<TextMeshProUGUI>().text = name + ": " + message;
            NetworkServer.Spawn(chatText.gameObject);
        }

        [ClientRpc]
        private void RpcHandleMessage(string message)
        {
            OnMessage?.Invoke(message);
        }
    }
}
