using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

namespace Hackathon
{
    public class WordSelector : NetworkBehaviour
    {

        [SerializeField] GameObject WordSelectorUI;
        [SerializeField] Button _inputButton_1;
        [SerializeField] Button _inputButton_2;
        [SerializeField] Button _inputButton_3;
        private WordManager _wordManager;

        [HideInInspector] public List<Button> buttons = new List<Button>();

        // Start is called before the first frame update
        void Start()
        {
            if (WordManager.Instance != null)
            {
                _wordManager = WordManager.Instance;
                _wordManager._wordSelector = this;
            }

            buttons.Add(_inputButton_1);
            buttons.Add(_inputButton_2);
            buttons.Add(_inputButton_3);

            _wordManager.SetButtonWords();
        }

        public override void OnStartAuthority()
        {
            WordSelectorUI.SetActive(true);
        }

        public void OnWordSelect(Button button)
        {
            _wordManager?.OnWordSelect(button);
        }
    }
}
