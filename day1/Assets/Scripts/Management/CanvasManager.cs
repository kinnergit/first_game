using UnityEngine;

namespace Management
{
    public class CanvasManager : MonoBehaviour
    {
        private static Canvas instance;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = GetComponent<Canvas>();
            }
            
            DontDestroyOnLoad(instance);
        }
    }
}
