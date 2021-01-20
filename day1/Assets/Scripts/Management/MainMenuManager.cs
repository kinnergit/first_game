using UnityEngine;
using UnityEngine.SceneManagement;

namespace Management
{
    public class MainMenuManager : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void UIEnable()
        {
            GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
        }
        
    }
}
