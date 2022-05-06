using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

namespace Hackathon
{
    public class PlayerManager : NetworkBehaviour
    {
        [SerializeField] private float m_ZOffset = 0f;
        // [SerializeField] private NetworkLine m_networkLine;

        // public float m_lineWidth = 0.2f;
        // public Color m_lineColor;
        // private Line m_currentLine;

        private Camera m_cam;

        // Start is called before the first frame update
        void Start()
        {
            m_cam = Camera.main;
            transform.position = new Vector3(transform.position.x, transform.position.y, m_ZOffset);
        }

        // Update is called once per frame
        void Update()
        {
            if (!isLocalPlayer) return;

            // Only on Client: Move to mouse position on canvas. 
            var cursorPoint = GetCursorPoint();
            transform.position = cursorPoint;

            if (Input.GetMouseButtonDown(0))  // On Holding Mouse
            {
                CmdDrawLine(cursorPoint);
                //     // m_currentLine = Instantiate(m_networkLine, cursorPoint, Quaternion.identity);
                //     // m_currentLine.SetColor(m_lineColor);
                //     // m_currentLine.SetWidth(m_lineWidth);
                //     // NetworkServer.Spawn(m_currentLine.gameObject);
            }

            // // On Mouse click
             if (Input.GetMouseButton(0)) CmdDrawDot(cursorPoint);
        }

        [Command]
        private void CmdDrawLine(Vector3 cursorPoint)
        {
            NetworkDrawManager.Instance.DrawLine(cursorPoint);
        }

        private void CmdDrawDot(Vector3 cursorPoint)
        {
            NetworkDrawManager.Instance.DrawDot(cursorPoint);
        }

        private Vector3 GetCursorPoint()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = m_ZOffset;
            Vector3 worldPosition = m_cam.ScreenToWorldPoint(mousePos);
            return worldPosition;
        }
    }
}
