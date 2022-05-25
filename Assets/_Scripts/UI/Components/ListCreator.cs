using Mirror;
using TMPro;
using UnityEngine;

namespace Hackathon
{
    public class ListCreator : NetworkBehaviour
    {
        private WordManager _wordManager;

        [SerializeField] public TMP_InputField _inputText;

        // Start is called before the first frame update
        void Start()
        {
            _wordManager = WordManager.Instance;
            _wordManager._listCreator = this;

            SetWordList();
        }

        public void SetWordList()
        {
            _wordManager?.SetWordList();
        }
    }
}
