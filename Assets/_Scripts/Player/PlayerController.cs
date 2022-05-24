using UnityEngine;
using Mirror;

namespace Hackathon
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NetworkTransform))]
    public class PlayerController : NetworkBehaviour
    {
        public GameObject m_LinePrefab;

        [SyncVar]
        public float _lineWidth = 1f;
        [SyncVar]
        public Color _lineColor = Color.white;
        [SyncVar]
<<<<<<< HEAD
        public string _playerName;
=======
>>>>>>> 8716468... Beginning of Word Manager
        public string _currentWord = "";

        private Camera m_cam;
        private RectTransform m_Background;
        private Line _currentLine;
        private Rigidbody m_RigidBody;
        
        void Start()
        {
            m_cam = Camera.main;     
            m_RigidBody = GetComponent<Rigidbody>();            
            m_Background = GameObject.FindWithTag("BG").GetComponent<RectTransform>();
            transform.position = GetCursorPosition();
        }

        void Update()
        {
            if (!isLocalPlayer) return;

            var pos = GetCursorPosition();
            transform.position = pos;

            if (Input.GetMouseButtonDown(0)) // On Left Click
            {
                CmdNewLine(pos);
            }

            if (Input.GetMouseButton(0))  // While Left Click is down
            {
                CmdAddToLine(pos);
            }

            if (_currentWord != "")
            {
                Debug.Log("Current Word is: " + _currentWord);
            }
        }

        [Command]
        private void CmdNewLine(Vector3 position)
        {
            _currentLine = Instantiate(m_LinePrefab, position, Quaternion.identity, m_Background).GetComponent<Line>();
            _currentLine.SetColor(_lineColor);
            _currentLine.SetWidth(_lineWidth);
            _currentLine.SetPosition(position);
            NetworkServer.Spawn(_currentLine.gameObject);
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
