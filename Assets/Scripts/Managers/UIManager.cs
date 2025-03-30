using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SwordfishGame
{
    public class UIManager : MonoBehaviour
    {
        enum CanvasState
        {
            mainmenu,
            settings,
            gameplay,
            pause,
            gameover,
        }
        CanvasState canvasState;

        Canvas mainmenu;
        Canvas settings;
        Canvas gameplay;
        Canvas pause;
        Canvas gameover;

        public Canvas Mainmenu { get => mainmenu; }
        public Canvas Settings { get => settings; }
        public Canvas Gameplay { get => gameplay; }
        public Canvas Pause { get => pause; }
        public Canvas Gameover { get => gameover; }

        void Start()
        {
            mainmenu = transform.Find("CanvasMainmenu").GetComponent<Canvas>();
            settings = transform.Find("CanvasSettings").GetComponent<Canvas>();
            gameplay = transform.Find("CanvasGameplay").GetComponent<Canvas>();
            pause = transform.Find("CanvasPause").GetComponent<Canvas>();
            gameover = transform.Find("CanvasGameover").GetComponent<Canvas>();
            SwitchCanvasToMainmenu();    
        }

        // Update is called once per frame
        void Update()
        {
            if (NullsFound()) return;

            switch (canvasState)
            {
                case CanvasState.mainmenu:
                    MainmenuCanvasOn();
                    break;

                case CanvasState.settings:
                    SettingsCanvasOn();
                    break;

                case CanvasState.gameplay:
                    GameplayCanvasOn();
                    break;

                case CanvasState.pause:
                    PauseCanvasOn();
                    break;

                case CanvasState.gameover:
                    GameoverCanvasOn();
                    break;
            }
        }

        public void SwitchCanvasToMainmenu()
        {
            canvasState = CanvasState.mainmenu;
            gameplay.GetComponent<GameplayHud>().ResetScore();
        }

        public void SwitchCanvasToSettings()
        {
            canvasState = CanvasState.settings;
        }

        public void SwitchCanvasToGameplay()
        {
            canvasState = CanvasState.gameplay;
        }

        public void SwitchCanvasToPause()
        {
            canvasState = CanvasState.pause;
        }
        public void SwitchCanvasToGameover()
        {
            canvasState = CanvasState.gameover;
        }

        void MainmenuCanvasOn()
        {
            mainmenu.enabled = true;
            settings.enabled = false;
            gameplay.enabled = false;
            pause.enabled = false;
            gameover.enabled = false;
        }

        void SettingsCanvasOn()
        {
            mainmenu.enabled = false;
            settings.enabled = true;
            gameplay.enabled = false;
            pause.enabled = false;
            gameover.enabled = false;
        }

        void GameplayCanvasOn()
        {
            mainmenu.enabled = false;
            settings.enabled = false;
            gameplay.enabled = true;
            pause.enabled = false;
            gameover.enabled = false;
        }

        void PauseCanvasOn()
        {
            mainmenu.enabled = false;
            settings.enabled = false;
            gameplay.enabled = false;
            pause.enabled = true;
            gameover.enabled = false;
        }

        void GameoverCanvasOn()
        {
            mainmenu.enabled = false;
            settings.enabled = false;
            gameplay.enabled = false;
            pause.enabled = false;
            gameover.enabled = true;
        }

        bool NullsFound()
        {
            if (mainmenu == null) return true;
            if (settings == null) return true;
            if (gameplay == null) return true;
            if (pause == null) return true;
            if (gameover == null) return true;
            return false;
        }

        void OnGUI()
        {
            if (NullsFound()) 
            {
                GUI.Label(new Rect(0, 0, 1000, 1000), "NullsFound");

            }
        }
    }   
}
