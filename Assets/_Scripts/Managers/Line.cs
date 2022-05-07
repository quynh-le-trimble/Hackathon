using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Hackathon
{
    public class Line : NetworkBehaviour
    {
     
        [SerializeField] public LineRenderer _renderer;

        [SyncVar]
        Color m_color;

        

        public void SetColor(Color color)
        {
            _renderer.material.color = color;
            m_color = color;
        }

        public void SetWidth(float width)
        {
            _renderer.startWidth = width;
            _renderer.endWidth = width;
        }

        public void SetPosition(Vector3 pos)
        {
            if (!CanAppend(pos)) return;

            _renderer.positionCount++;
            _renderer.SetPosition(_renderer.positionCount-1, pos);
        }

        private bool CanAppend(Vector3 pos)
        {
            if (_renderer.positionCount == 0) return true;
            return Vector3.Distance(_renderer.GetPosition(_renderer.positionCount - 1), pos) > DrawManager.RESOLUTION;
        }
    }
}