using System;
using UnityEngine;
using UnityEngine.UI;

namespace Management
{
    public class UIManager : MonoBehaviour
    {
        
        public Text cherryNum;
        public Text gemNum;

        private static UIManager instance;

        private GameManager gm;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        
            DontDestroyOnLoad(instance);
        }

        private void Start()
        {
            gm = GameManager.GetInstance();
        }

        private void Update()
        {
            cherryNum.text = gm.GetCherryNum().ToString();
            gemNum.text = gm.GetGemNum().ToString();
        }

        public static UIManager GetInstance()
        {
            return instance;
        }
    }
}
