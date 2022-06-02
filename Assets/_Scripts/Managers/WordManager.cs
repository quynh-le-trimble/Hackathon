using System.Collections.Generic;
using System.Linq;
using TMPro;
using Mirror;
using UnityEngine;
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

        private PlayerController _playerController;
        public List<Button> buttons = new List<Button>();

        public string currentWord = string.Empty;

        [HideInInspector] public ListCreator _listCreator;
        [HideInInspector] public WordSelector _wordSelector;

        //Default list to use if no list passed in
        string DefaultWordList = "cat,dog,car,house,cpu,speaker,knife,computer";

        private void Start()
        {
            WordList.parsedWordList = DefaultWordList.Split(',').ToList();
            WordList.usedWordsList = new List<string>();
        }

        public void SetButtonWords()
        {
            foreach (Button button in _wordSelector.buttons)
            {
                Debug.Log("Before: " + button.GetComponentInChildren<TextMeshProUGUI>().text);
                GetNextRandomWord(button);
                Debug.Log("After: " + button.GetComponentInChildren<TextMeshProUGUI>().text);
            }
        }

        public void SetWordList()
        {
            string wordList;
            if (!string.IsNullOrEmpty(_listCreator._inputText.text) && _listCreator._inputText.text != "Enter comma seperated word list here")
                wordList = _listCreator._inputText.text;
            else
                wordList = DefaultWordList;

            WordList.parsedWordList = wordList.Split(',').ToList();
            WordList.usedWordsList = new List<string>();
        }

       
        public void OnWordSelect(Button button)
        {

            currentWord = button.GetComponentInChildren<TextMeshProUGUI>().text;

            //foreach (Button hideButton in _wordSelector.buttons)
            //{
            //    hideButton.GetComponentInParent<CanvasGroup>().alpha = 0f;
            //    hideButton.GetComponentInParent<CanvasGroup>().interactable = false;
            //    hideButton.GetComponentInParent<CanvasGroup>().blocksRaycasts = false;
            //}

            GameMenu.Instance.m_SelectedWordText.text = currentWord;

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
