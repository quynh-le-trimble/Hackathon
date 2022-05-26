using Mirror;
using TMPro;
using UnityEngine;

namespace Hackathon
{
    public class ListCreator : NetworkBehaviour
    {
        [SerializeField] private WordManager _wordManager;

        public TMP_InputField _inputText;

        // Start is called before the first frame update
        void Start()
        {
            if (WordManager.Instance != null)
            {
                _wordManager = WordManager.Instance;
                _wordManager._listCreator = this;
            }
        }

        public void SetWordList()
        {
            _wordManager?.SetWordList();
        }
    }
}
