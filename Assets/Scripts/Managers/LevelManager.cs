using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SwordfishGame
{
    public class LevelManager : MonoBehaviour
    {
        const string gameplayScene = "Gameplay";
        const string codeDevScene = "Code-Dev";
        const string mainmenuScene = "Mainmenu";

        public void ExitGame()
        {
            Application.Quit();
        }

        public void SwitchSceneToMainmenu()
        {
            SceneManager.LoadScene(mainmenuScene);
        }

        public void SwitchSceneToGameplay()
        {
            SceneManager.LoadScene(gameplayScene);
        }

        public void SwitchSceneToCodeDev()
        {
            SceneManager.LoadScene(codeDevScene);
        }
        
    }
}
