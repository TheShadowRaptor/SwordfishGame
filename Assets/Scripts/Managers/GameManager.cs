using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SwordfishGame
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            mainmenu,
            gameplay,
            paused,
        }
        public GameState gameState;

        // Start is called before the first frame update
        void Start()
        {
            SetStateToMainmenu();
        }

        // Update is called once per frame
        void Update()
        {
            switch (gameState)
            {
                case GameState.mainmenu:
                    ManageMainmenuState();
                    break;

                case GameState.gameplay:
                    ManageGameplayState();
                    break;

                case GameState.paused:
                    ManagePausedState();
                    break;
            }
        }

        public void SetStateToMainmenu()
        {
            gameState = GameState.mainmenu;
        }

        public void SetStateToGameplay()
        {
            gameState = GameState.gameplay;
        }

        public void SetStateToPaused()
        {
            gameState = GameState.paused;
        }

        void ManageMainmenuState()
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Confined;
        }

        void ManageGameplayState()
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void ManagePausedState()
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
