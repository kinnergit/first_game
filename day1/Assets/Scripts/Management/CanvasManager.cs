using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Management
{
    public class CanvasManager : MonoBehaviour
    {
        public GameObject pauseDialog;
        public GameObject dialog;
        public AudioMixer am;
        public Joystick joystick;
        
        private static CanvasManager instance;
        private float amDft;

        public static CanvasManager GetInstance()
        {
            return instance;
        }
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                
                DontDestroyOnLoad(instance);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            am.GetFloat("MainVolume", out amDft);
        }

        public void PauseGame()
        {
            pauseDialog.SetActive(true);
            
            Time.timeScale = 0;

            am.SetFloat("MainVolume", -80f);
        }

        public void ResumeGame()
        {
            pauseDialog.SetActive(false);

            Time.timeScale = 1f;
            
            am.SetFloat("MainVolume", amDft);
        }

        public void SetVolume(float vol)
        {
            amDft = vol;
            am.SetFloat("MainVolume", vol);
        }
    }
}
