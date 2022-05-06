using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Hackathon
{
    public class PlayerManager : NetworkBehaviour
    {
        [SerializeField] private float m_ZOffset = 0f;

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
