using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUNFrames;

namespace SUNFrames
{
    /// <summary>
    /// 将UIMethod制作成单例模式
    /// 其方法大多数是提供给Panel使用
    /// </summary>
    public class UIMethods
    {
        private static UIMethods instance;

        public static UIMethods GetInstance()
        {
            if (instance == null)
            {
                instance = new UIMethods();
            }

            return instance;
        }

        /// <summary>
        /// 获得场景中的Canvas
        /// </summary>
        /// <returns>Canvas_Obj</returns>
        public GameObject FindCanvas()
        {
            GameObject gameObject = GameObject.FindObjectOfType<Canvas>().gameObject;
            if (gameObject == null)
            {
                Debug.LogError("没有在场景里面找到Canvas");
                return gameObject;
            }

            return gameObject;
        }

        /// <summary>
        /// 从目标对象中根据名字找到子对象
        /// </summary>
        /// <param name="panel">目标Panel</param>
        /// <param name="childName">目标子对象名称</param>
        /// <returns></returns>
        public GameObject FindObjectInChild(GameObject panel, string childName)
        {
            Transform[] transforms = panel.GetComponentsInChildren<Transform>();

            foreach (var tra in transforms)
            {
                if (tra.gameObject.name == childName)
                {
                    return tra.gameObject;
                }
            }

            Debug.LogWarning($"在{panel.name}物体当中没有找到{childName}物体！");
            return null;
        }

        public T GetOrAddComponent<T>(GameObject gameObject) where T : Component
        {
            if (gameObject.GetComponent<T>() != null)
            {
                return gameObject.GetComponent<T>();
            }
            else
            {
                Debug.LogWarning($"{gameObject.name}物体上不存在目标组件");
                gameObject.AddComponent<T>();
                return gameObject.GetComponent<T>();
            }
        }

        public T GetOrAddComponentInChildren<T>(GameObject gameObject, string childName) where T : Component
        {
            Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();

            foreach (var tra in transforms)
            {
                if (tra.gameObject.name == childName)
                {
                    if (tra.GetComponent<T>() != null)
                    {
                        return tra.GetComponent<T>();
                    }
                    else
                    {
                        tra.gameObject.AddComponent<T>();
                        return tra.GetComponent<T>();
                    }
                }
            }

            Debug.LogError($"没有在{gameObject.name}当中找到{childName}组件!");
            return null;
        }
    }
}