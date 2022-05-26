using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hackathon
{
    public class MenuOpener : MonoBehaviour
    {
        public string menuName;

        // Start is called before the first frame update
        void Start()
        {
            if (menuName == "Lobby")
            {
                LobbyMenu.Open();
            }
            else if (menuName == "Game")
            {
                GameMenu.Open();
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
