using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hackathon
{
    public class DrawButton : MonoBehaviour
    {
        [SerializeField] private NetworkDrawManager _drawManager;
        [SerializeField] private Button _button;

        public void OnButtonPress()
        {
            if (_button.tag == "Color Button")
            {
                var buttonColor = _button.GetComponent<Image>().color;
                _drawManager._lineColor = buttonColor;
            }

            if (_button.tag == "Size Button")
            {
                var buttonWidth = GetComponent<RectTransform>().sizeDelta.x;
                _drawManager._lineWidth = buttonWidth;
            }
        }
    }
}
