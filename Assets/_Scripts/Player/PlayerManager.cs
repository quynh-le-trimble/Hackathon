using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

namespace Hackathon
{
    public class PlayerManager : NetworkBehaviour
    {
        private Camera m_cam;
        private RectTransform m_Background;
        [SerializeField] private NetworkLine _networkLine;

        public const float RESOLUTION = 0.1f;

        public float _lineWidth = 1f;
        public Color _lineColor = Color.white;

        private NetworkLine _currentLine;
        // Start is called before the first frame update
        void Start()
        {
            m_cam = Camera.main;
            m_Background = GameObject.FindWithTag("BG").GetComponent<RectTransform>();
            transform.position = GetCursorPosition();
        }

        // Update is called once per frame
        void Update()
        {
            if (!isLocalPlayer) return;

            var pos = GetCursorPosition();
            transform.position = pos;

            if (Input.GetMouseButtonDown(0))
            {
                // _currentLine = Instantiate(_networkLine, pos, Quaternion.identity, m_Background);
                // _currentLine.SetColor(_lineColor);
                // _currentLine.SetWidth(_lineWidth);
                // _currentLine.SetPosition(pos);
                CmdNewLine(pos);
            }

            if (Input.GetMouseButton(0))  // On Holding Mouse
            {
                CmdAddToLine(pos);
                //_currentLine.SetPosition(pos);
            }

        }

        [Command]
        private void CmdNewLine(Vector3 position)
        {
            _currentLine = Instantiate(_networkLine, position, Quaternion.identity, m_Background);
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

        private Vector3 GetCursorPosition()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            Vector3 worldPosition = m_cam.ScreenToWorldPoint(mousePos);
            return worldPosition;
        }
    }
}
