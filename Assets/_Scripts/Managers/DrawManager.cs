<<<<<<< HEAD
=======
using System.Collections;
using System.Collections.Generic;
>>>>>>> c64f320... Beginings of Draw Manager
using UnityEngine;
using Mirror;

namespace Hackathon
{
    public class DrawManager : Singleton<DrawManager>
    {
<<<<<<< HEAD
        public const float RESOLUTION = 0.1f;
        public float m_lineWidth = 1f;
        public Color m_lineColor;

        private Camera m_cam;
        [SerializeField]
        private Line _networkLine;
        [SerializeField]
        private Transform _playerCursor;
        private Line _currentLine;

        void Start()
        {
            m_cam = Camera.main;
=======
        private Camera _cam;
        [SerializeField] private NetworkLine _networkLine;
        [SerializeField] private Transform _playerCursor;

        public const float RESOLUTION = 0.1f;
        public float _lineWidth = 1f;
        public Color _lineColor;

        private Line _currentLine;
        void Start()
        {
            _cam = Camera.main;
>>>>>>> c64f320... Beginings of Draw Manager
        }

        void Update()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5f;
<<<<<<< HEAD
            Vector3 worldPosition = m_cam.ScreenToWorldPoint(mousePos);
            if (_playerCursor != null) _playerCursor.position = worldPosition;

            if (Input.GetMouseButtonDown(0) && CanDraw(worldPosition))
            {
                Debug.Log("world position: " + worldPosition.x + " " + worldPosition.y);
                _currentLine = Instantiate(_networkLine, worldPosition, Quaternion.identity);
                _currentLine.SetColor(m_lineColor);
                _currentLine.SetWidth(m_lineWidth);
                NetworkServer.Spawn(_currentLine.gameObject);
            }

            if (Input.GetMouseButton(0) && CanDraw(worldPosition)) _currentLine.SetPosition(worldPosition);
=======
            Vector3 worldPosition = _cam.ScreenToWorldPoint(mousePos);
            if(_playerCursor!=null) _playerCursor.position = worldPosition;

            if(Input.GetMouseButtonDown(0) && CanDraw(worldPosition))
            {
                Debug.Log("world position: " + worldPosition.x + " " + worldPosition.y);
                _currentLine = Instantiate(_networkLine, worldPosition, Quaternion.identity);
                _currentLine.SetColor(_lineColor);
                _currentLine.SetWidth(_lineWidth);
                NetworkServer.Spawn(_currentLine.gameObject);
            } 

            if(Input.GetMouseButton(0) && CanDraw(worldPosition)) _currentLine.SetPosition(worldPosition);
>>>>>>> c64f320... Beginings of Draw Manager
        }

        bool CanDraw(Vector3 worldPosition)
        {
            Vector3 screenLowerRight = new Vector3(Screen.width, 0f, 5f);
<<<<<<< HEAD
            var lowerRight = m_cam.ScreenToWorldPoint(screenLowerRight);
            if ((worldPosition.x > lowerRight.x - 175 && worldPosition.y < lowerRight.y + 225)) return false;
=======
            var lowerRight = _cam.ScreenToWorldPoint(screenLowerRight);
            if((worldPosition.x > lowerRight.x-175 && worldPosition.y < lowerRight.y+225)) return false;
>>>>>>> c64f320... Beginings of Draw Manager
            return true;
        }
    }
}
