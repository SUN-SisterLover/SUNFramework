using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUNFrames;
using UnityEngine.SceneManagement;

namespace SUNFrames
{
    public class SceneControl
    {
        private static SceneControl instance;

        public static SceneControl GetInstance()
        {
            if (instance == null)
            {
                Debug.LogError("SceneControl实体不存在！");
                return instance;
            }

            return instance;
        }

        public int sceneNumber = 1;
        public string[] stringScene;

        /// <summary>
        /// key为场景的名称，val为场景的信息
        /// </summary>
        public Dictionary<string, SceneBase> sceneDict;

        public SceneControl()
        {
            instance = this;

            sceneDict = new Dictionary<string, SceneBase>();
        }

        /// <summary>
        /// 加载一个场景
        /// </summary>
        /// <param name="sceneName">目标场景的名称</param>
        /// <param name="sceneBase">目标场景的Scene</param>
        public void LoadScene(string sceneName, SceneBase sceneBase)
        {
            if (sceneNumber >= 2)
            {
                foreach (string scenename in stringScene)
                {
                    if (scenename == sceneName)
                    {
                        Debug.Log($"场景{sceneName}被加载过");
                    }

                    sceneNumber++;
                    stringScene[sceneNumber] = sceneName;
                }
            }

            //找不到对应的场景，将其添加进字典种
            if (!sceneDict.ContainsKey(sceneName))
            {
                sceneDict.Add(sceneName, sceneBase);
            }

            //加载新场景时，老场景退出执行方法
            if (sceneNumber >= 2)
            {
                sceneDict[SceneManager.GetActiveScene().name].ExitScene();
            }

            sceneBase.EnterScene();
            GameRoot.GetInstance().UIManagerRoot.Pop(true);
            SceneManager.LoadScene(sceneName);
        }
    }
}
