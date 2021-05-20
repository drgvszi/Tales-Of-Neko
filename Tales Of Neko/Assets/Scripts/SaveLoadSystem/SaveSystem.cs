using System;
using UnityEngine;

namespace SaveLoadSystem
{
    public class SaveSystem:MonoBehaviour
    {
        private static SaveSystem _instance=null;
        private SceneHandler _sceneHandler;

        private SaveSystem(){}

        public static SaveSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<SaveSystem> ();
                }

                return _instance;
            }
        }

        public void Awake()
        {
            
            if (_instance == null) {
                _instance = Instance;
                _instance._sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
                
                DontDestroyOnLoad (_instance);
            } else if (this != _instance) {
                Destroy (this);
            }
        }

        public void Save()
        {
            GameManager.Instance.Save();
            _sceneHandler.SaveScene();
        }

        public void Load()
        {
            GameManager.Instance.Load();
            _sceneHandler.LoadScene();
        }
    }
}