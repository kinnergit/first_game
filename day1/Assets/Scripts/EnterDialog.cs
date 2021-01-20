using Management;
using UnityEngine;

public class EnterDialog : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.gameObject.CompareTag("Player"))
        {
            CanvasManager.GetInstance().dialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.gameObject.CompareTag("Player"))
        {
            CanvasManager.GetInstance().dialog.SetActive(false);
        }
    }
}
