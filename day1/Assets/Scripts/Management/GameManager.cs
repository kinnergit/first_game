using UnityEngine;

namespace Management
{
    public class GameManager : MonoBehaviour
    {
        
        [SerializeField]
        private int cherryNum;
    
        [SerializeField]
        private int gemNum;

        private static GameManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        
            DontDestroyOnLoad(instance);
        }

        public static GameManager GetInstance()
        {
            return instance;
        }

        public void IncrCherryNum()
        {
            cherryNum++;
        }

        public void IncrGemNum()
        {
            gemNum++;
        }
        
        public int GetCherryNum()
        {
            return cherryNum;
        }

        public int GetGemNum()
        {
            return gemNum;
        }

        public void ClearCollectionNums()
        {
            cherryNum = 0;
            gemNum = 0;
        }
    }
}
