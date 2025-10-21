using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUNFrames;

namespace SUNFrames
{

    public class GameRoot : MonoBehaviour
    {
        private static GameRoot instance;

        private UIManager UIManager;

        public UIManager UIManagerRoot
        {
            get => UIManager;
        }

        private SceneControl SceneControl;

        public SceneControl SceneControlRoot
        {
            get => SceneControl;
        }

        public static GameRoot GetInstance()
        {
            if (instance == null)
            {
                Debug.LogWarning("GameRoot获得实例失败");
                return instance;
            }

            return instance;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            UIManager = new UIManager();
            SceneControl = new SceneControl();
        }
        
        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            UIManagerRoot.CanvasObj = UIMethods.GetInstance().FindCanvas();

            #region 初始场景/UI设定
            
            Scene1 scene1 = new Scene1();
            SceneControlRoot.sceneDict.Add(scene1.sceneName, scene1);
            UIManagerRoot.Push(new StartPanel());

            #endregion

        }
    }
}
