using System.Collections.Generic;
using UnityEngine;

namespace Hackathon
{
    public class MenuManager : Singleton<MenuManager>
    {
        [SerializeField] MainMenu m_MainMenu;
        [SerializeField] LobbyMenu m_LobbyMenu;
        [SerializeField] ProfileMenu m_ProfileMenu;
        [SerializeField] GameMenu m_GameMenu;

        Transform m_MenuParent;
        Stack<Menu> m_MenuStack = new Stack<Menu>();

        protected override void Awake()
        {
            base.Awake();
            InitializeMenus();
        }

        public void OpenMenu(Menu menuInstance)
        {
            if (menuInstance == null)
            {
                Debug.LogWarning($"MENUMANAGER OpenMenu Error: Invalid Menu");
                return;
            }

            if (m_MenuStack.Count > 0)
            {
                foreach (var menu in m_MenuStack)
                {
                    menu.gameObject.SetActive(false);
                }
            }

            menuInstance.gameObject.SetActive(true);
            m_MenuStack.Push(menuInstance);
        }

        public void CloseMenu()
        {
            if (m_MenuStack.Count == 0)
            {
                Debug.LogWarning($"MENUMANAGER CloseMenu Error: No menus in stack");
                return;
            }

            Menu topMenu = m_MenuStack.Pop();
            topMenu.gameObject.SetActive(false);

            if (m_MenuStack.Count > 0)
            {
                var nextMenu = m_MenuStack.Peek();
                nextMenu.gameObject.SetActive(true);
            }
        }

        private void InitializeMenus()
        {
            // Generate a default parent if none is specified. 
            if (m_MenuParent == null)
            {
                GameObject menuParentObject = new GameObject("Menus");
                m_MenuParent = menuParentObject.transform;
            }

            // Manual registration, prob a better way to handle this. 
            Menu[] menuPrefabs = { m_MainMenu, m_LobbyMenu, m_ProfileMenu, m_GameMenu };

            foreach (var menuPrefab in menuPrefabs)
            {
                var menuInstance = Instantiate(menuPrefab, m_MenuParent);
                if (menuPrefab != m_MainMenu)
                {
                    menuInstance.gameObject.SetActive(false);
                }
                else
                {
                    OpenMenu(menuInstance);
                }
            }
        }
    }
}
