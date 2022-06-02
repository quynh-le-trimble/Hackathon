using UnityEngine;
using Mirror;
using System;

namespace Hackathon
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NetworkTransform))]
    public class PlayerController : NetworkBehaviour
    {
        public GameObject m_LinePrefab;
        public GameObject m_WordSelectorUI;
        public GameObject m_DrawCanvas;
        private int m_orderNumber = 0;

        [SyncVar]
        public float _lineWidth = 1f;
        [SyncVar]
        public Color _lineColor = Color.white;
        [SyncVar]
        public string _playerName;

        [SyncVar(hook = nameof(SyncIsActiveDrawer))]
        public bool _isActiveDrawer;

        [SyncVar(hook = nameof(SyncIsSelectingWord))]
        public Boolean _isSelectingWord;

        public string _currentWord = "";

        private Camera m_cam;
        private RectTransform m_Background;
        private Line _currentLine;
        private Rigidbody m_RigidBody;

        private bool isDisabled;

        public override void OnStartAuthority()
        {
            m_cam = Camera.main;
            m_RigidBody = GetComponent<Rigidbody>();
            MenuManager.Instance.OpenMenu(GameMenu.Instance);
            m_Background = GameObject.FindWithTag("BG").GetComponent<RectTransform>();
            transform.position = GetCursorPosition();
            m_DrawCanvas.SetActive(true);
            m_WordSelectorUI.SetActive(false);
        }

        void Update()
        {
            if (!isLocalPlayer) return;

            var pos = GetCursorPosition();
            transform.position = pos;

            if (Input.GetMouseButtonDown(0) && !isDisabled) // On Left Click
            {
                CmdNewLine(pos);
            }

            if (Input.GetMouseButton(0) && !isDisabled)  // While Left Click is down
            {
                CmdAddToLine(pos);
            }

            if (_currentWord != "")
            {
                Debug.Log("Current Word is: " + _currentWord);
            }


        }

        private void SyncIsActiveDrawer(bool oldVal, bool newVal)
        {
            if(isLocalPlayer) {
                isDisabled = !_isActiveDrawer;
            }
        }

        private void SyncIsSelectingWord(bool oldVal, bool newVal)
        {
            if(isLocalPlayer) {
                m_WordSelectorUI.SetActive(_isSelectingWord);
            }
        }

        [Command]
        private void CmdNewLine(Vector3 position)
        {
            _currentLine = Instantiate(m_LinePrefab, position, Quaternion.identity, m_Background).GetComponent<Line>();
            _currentLine.gameObject.tag = "Line";
            _currentLine.GetComponent<LineRenderer>().sortingOrder = m_orderNumber;
            _currentLine.SetColor(_lineColor);
            _currentLine.SetWidth(_lineWidth);
            _currentLine.SetPosition(position);
            NetworkServer.Spawn(_currentLine.gameObject);
            m_orderNumber++;
        }

        [Command]
        private void CmdAddToLine(Vector3 position)
        {
            _currentLine.SetPosition(position);

        }

        [Command]
        public void CmdUpdateColor(Color color)
        {
            _lineColor = color;
        }

        [Command]
        public void CmdUpdateWidth(float width)
        {
            _lineWidth = width;
        }

        [Command]
        public void CmdUpdateCurrentWord(string word)
        {
            _currentWord = word;
        }

        private Vector3 GetCursorPosition()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            Vector3 worldPosition = m_cam.ScreenToWorldPoint(mousePos);
            return worldPosition;
        }
    }
}
