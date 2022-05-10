using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

namespace Hackathon
{
    public class NetworkDrawManager : NetworkSingleton<NetworkDrawManager>
    {
        public RectTransform m_ParentCanvas;
        public RectTransform m_debugTransform;

        private Camera _cam;
        [SerializeField] private NetworkLine _networkLinePrefab;
        [SerializeField] private GameObject m_brushPrefab;
        [SerializeField] private Transform _playerCursor;


        public const float RESOLUTION = 0.1f;
        public float _lineWidth = 0.2f;
        public Color _lineColor;
        private NetworkLine _currentLine;

        void Start()
        {
            _cam = Camera.main;

            Application.targetFrameRate = 60;
        }

        void Update()
        {


            // Vector3 mousePos = Input.mousePosition;
            // mousePos.z = 5f;
            // Vector3 worldPosition = _cam.ScreenToWorldPoint(mousePos);

            // if(_playerCursor!=null) _playerCursor.position = worldPosition;

            // if (Input.GetMouseButtonDown(0))
            // {

            // _currentLine = Instantiate(_networkLine, worldPosition, Quaternion.identity);
            // _currentLine.SetColor(_lineColor);
            // _currentLine.SetWidth(_lineWidth);
            // NetworkServer.Spawn(_currentLine.gameObject);
            //     DrawLine(worldPosition);
            // }

            //if (Input.GetMouseButton(0)) DrawDot(worldPosition);
        }

        public void DrawLine(Vector3 position)
        {
            Vector2 anchorPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_ParentCanvas, new Vector2(position.x, position.y), _cam, out anchorPoint);
            

            var brushPrefab = Instantiate(m_brushPrefab, position, Quaternion.identity, m_ParentCanvas);
            brushPrefab.GetComponent<RectTransform>().anchoredPosition = anchorPoint;
        }

        [ClientRpc]
        private void RpcDrawLine(Vector3 position, Vector2 anchorPoint)
        {
            var brushPrefab = Instantiate(m_brushPrefab, position, Quaternion.identity, m_ParentCanvas);
            var rect = brushPrefab.GetComponent<RectTransform>();
            rect.anchoredPosition = anchorPoint;
        }

        public void DrawDot(Vector3 position)
        {
            _currentLine.SetColor(_lineColor);
            _currentLine.SetWidth(_lineWidth);
            _currentLine.SetPosition(position);
            NetworkServer.Spawn(_currentLine.gameObject);
        }
    }
}
