using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

namespace Hackathon
{
    public struct LinePosition
    {
        public float x;
        public float y;
        public float z;
    }

    public class Line : NetworkBehaviour
    {
        [SerializeField] public LineRenderer _renderer;

        public SyncList<LinePosition> m_LinePositions = new SyncList<LinePosition>();

        [SyncVar]
        public Color m_Color = Color.white;

        [SyncVar]
        public float m_Width = 1f;

        public void SetColor(Color color)
        {
            _renderer.material.color = color;
            m_Color = color;
        }

        public void SetWidth(float width)
        {
            _renderer.startWidth = width;
            _renderer.endWidth = width;
            m_Width = width;
        }

        public void SetPosition(Vector3 pos)
        {
            if (!CanAppend(pos)) return;

            _renderer.positionCount++;
            _renderer.SetPosition(_renderer.positionCount - 1, pos);
            m_LinePositions.Add(new LinePosition
            {
                x = pos.x,
                y = pos.y,
                z = pos.z
            });
        }

        private bool CanAppend(Vector3 pos)
        {
            if (_renderer.positionCount == 0) return true;
            return Vector3.Distance(_renderer.GetPosition(_renderer.positionCount - 1), pos) > DrawManager.RESOLUTION;
        }

        public override void OnStartClient()
        {
            m_LinePositions.Callback += OnPositionAdded;
            _renderer.material.color = m_Color;
            _renderer.startWidth = m_Width;
            _renderer.endWidth = m_Width;
        }

        private void OnPositionAdded(SyncList<LinePosition>.Operation op, int itemIndex, LinePosition oldItem, LinePosition newItem)
        {
            switch (op)
            {
                case SyncList<LinePosition>.Operation.OP_ADD:
                    // index is where it was added into the list
                    // newItem is the new item
                    _renderer.positionCount++;
                    _renderer.SetPosition(_renderer.positionCount - 1, new Vector3(newItem.x, newItem.y, newItem.z));
                    break;
                case SyncList<LinePosition>.Operation.OP_INSERT:
                    // index is where it was inserted into the list
                    // newItem is the new item
                    // _renderer.positionCount++;
                    // _renderer.SetPosition(_renderer.positionCount - 1, new Vector3(newItem.x, newItem.y, newItem.z));
                    break;
                case SyncList<LinePosition>.Operation.OP_REMOVEAT:
                    // index is where it was removed from the list
                    // oldItem is the item that was removed
                    break;
                case SyncList<LinePosition>.Operation.OP_SET:
                    // index is of the item that was changed
                    // oldItem is the previous value for the item at the index
                    // newItem is the new value for the item at the index
                    break;
                case SyncList<LinePosition>.Operation.OP_CLEAR:
                    // list got cleared
                    break;
            }
        }
    }
}
