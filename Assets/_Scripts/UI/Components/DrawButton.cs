using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Linq;

namespace Hackathon
{
    public class DrawButton : NetworkBehaviour
    {
        private PlayerController _playerController;
        [SerializeField] private Button _button;

        public void Start()
        {
            _playerController = (PlayerController)GameObject.FindObjectsOfType(typeof(PlayerController)).FirstOrDefault(o => ((PlayerController)o).isLocalPlayer);
        }

        public void OnButtonPress()
        {
            if (_button.tag == "Color Button")
            {
                var buttonColor = _button.GetComponent<Image>().color;
                _playerController.CmdUpdateColor(buttonColor);
            }

            if (_button.tag == "Size Button")
            {
                var buttonWidth = _button.GetComponent<RectTransform>().sizeDelta.x;
                _playerController.CmdUpdateWidth(buttonWidth);
            }

            if (_button.tag == "Eraser Button")
            {
                GameObject[] lines = GameObject.FindGameObjectsWithTag("Line");
                foreach (GameObject line in lines)
                {
                    Destroy(line);
                }
            }
        }
    }
}