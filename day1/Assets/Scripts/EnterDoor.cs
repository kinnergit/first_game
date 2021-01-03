using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{ 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
