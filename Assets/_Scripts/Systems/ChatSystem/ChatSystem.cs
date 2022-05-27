using System;
using Mirror;
using TMPro;
using UnityEngine;

namespace Hackathon
{
    public class ChatSystem : NetworkBehaviour
    {
        [SerializeField] GameObject m_ChatSystemUI;
        [SerializeField] TextMeshProUGUI m_ChatBoxDisplay;
        [SerializeField] TMP_InputField m_ChatInput;

        private static event Action<string> OnMessage;

        public override void OnStartAuthority()
        {
            m_ChatSystemUI.SetActive(true);
            OnMessage += HandleNewMessage;
        }

        private void HandleNewMessage(string message)
        {
            m_ChatBoxDisplay.text += message;
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

            CmdSendMessage(message);
            m_ChatInput.text = string.Empty;
        }

        [Command]
        private void CmdSendMessage(string message)
        {
            // Validation 

            RpcHandleMessage($"[{connectionToClient.connectionId}]: {message}");
        }

        [ClientRpc]
        private void RpcHandleMessage(string message)
        {
            OnMessage?.Invoke(message);
        }
    }
}
