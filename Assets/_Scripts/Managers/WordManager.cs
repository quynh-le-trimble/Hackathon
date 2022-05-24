using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Hackathon
{
    public class WordManager : PersistentSingleton<WordManager>
    {
        private static class WordList
        {
            public static List<string> parsedWordList { get; set; }
            public static List<string> usedWordsList { get; set; }
        }

        [SerializeField] TMP_InputField _inputText;
        [SerializeField] Button _inputButton;
        private PlayerController _playerController;
        

        //Default list to use if no list passed in
        string DefaultWordList = "cat,dog,car,house,microprocessor,speaker,knife,computer";

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Game")
            {
                GameObject[] buttons = GameObject.FindGameObjectsWithTag("ChooseWordButton");
                foreach (GameObject button in buttons)
                {
                    Debug.Log("Before: " + button.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text);
                    GetNextRandomWord(button.GetComponent<Button>());
                    Debug.Log("After: " + button.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text);
                }
            }
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void OnButtonPress()
        {
            string wordList;

            if (_inputButton.tag == "SetWordListButton")
            {
                if (!string.IsNullOrEmpty(_inputText.text) && _inputText.text != "Enter comma seperated word list here")
                    wordList = _inputText.text;
                else
                    wordList = DefaultWordList;
                
                WordList.parsedWordList = wordList.Split(',').ToList();
                WordList.usedWordsList = new List<string>();
            }
            else
            {
                _playerController = (PlayerController)GameObject.FindObjectsOfType(typeof(PlayerController)).FirstOrDefault(o => ((PlayerController)o).isLocalPlayer);

                // Expose Chosen word and hide buttons
                _playerController.CmdUpdateCurrentWord(_inputButton.GetComponentInChildren<TextMeshProUGUI>().text);
                _inputButton.GetComponentInParent<CanvasGroup>().alpha = 0f;
                _inputButton.GetComponentInParent<CanvasGroup>().interactable = false;
                _inputButton.GetComponentInParent<CanvasGroup>().blocksRaycasts = false;
            }
        }

        private void GetNextRandomWord(Button button)
        {
            var rand = new System.Random();
            int index = rand.Next(WordList.parsedWordList.Count);
            button.GetComponentInChildren<TextMeshProUGUI>().text = WordList.parsedWordList[index];
            
            ModifyWordLists(index);
        }
        
        // Remove words from wordlist to avoid reusing, reset wordlist if too few words remain
        private void ModifyWordLists(int index)
        {
            WordList.usedWordsList.Add(WordList.parsedWordList[index]);
            WordList.parsedWordList.Remove(WordList.parsedWordList[index]);

            if (WordList.parsedWordList.Count <= 3)
            {
                WordList.usedWordsList.AddRange(WordList.parsedWordList);
                WordList.parsedWordList.Clear();
                WordList.parsedWordList.AddRange(WordList.usedWordsList);
            }
        }
    }
}
