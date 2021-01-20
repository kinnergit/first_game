using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{ 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GoNextLevel();
        }
    }

    public void GoNextLevel()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
