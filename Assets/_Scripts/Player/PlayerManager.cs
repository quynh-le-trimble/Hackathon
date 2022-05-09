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

        // Start is called before the first frame update
        void Start()
        {
            m_cam = Camera.main;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        // Update is called once per frame
        void Update()
        {
            if (!isLocalPlayer) return;

            // Only on Client: Move to mouse position on canvas. 
            var cursorPoint = GetCursorPoint();
            transform.position = cursorPoint;

            if (Input.GetMouseButton(0))  // On Holding Mouse
            {
                CmdDraw(Input.mousePosition);
            }

        }

        [Command]
        private void CmdDraw(Vector3 cursorPoint)
        {
            NetworkDrawManager.Instance.DrawLine(cursorPoint);
        }

        private Vector3 GetCursorPoint()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            Vector3 worldPosition = m_cam.ScreenToWorldPoint(mousePos);
            return worldPosition;
        }
    }
}
