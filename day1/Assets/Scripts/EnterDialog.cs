using UnityEngine;

public class EnterDialog : MonoBehaviour
{
    public GameObject dialog;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dialog.SetActive(false);
        }
    }
}
