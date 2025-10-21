using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SUNFrames;

namespace SUNFrames
{
    /// <summary>
    /// [单例]UIManager，管理游戏中所有的UI
    /// </summary>
    public class UIManager
    {
        /// <summary>
        /// 存储UI Panel的栈结构
        /// </summary>
        public Stack<BasePanel> uiStack;

        /// <summary>
        /// 存储Panel的名称与物体的对应关系
        /// </summary>
        public Dictionary<string, GameObject> uiDictObject;

        /// <summary>
        /// 当前场景下对应的Canvas
        /// </summary>
        public GameObject CanvasObj;

        private static UIManager instance;

        public static UIManager GetInstance()
        {
            if (instance == null)
            {
                Debug.LogError("UIManager实例不存在");
                return instance;
            }
            else
            {
                return instance;
            }
        }

        public UIManager()
        {
            instance = this;
            uiStack = new Stack<BasePanel>();
            uiDictObject = new Dictionary<string, GameObject>();
        }

        /// <summary>
        /// 加载资源用方法
        /// </summary>
        /// <param name="uiType">加载的资源索引</param>
        /// <returns></returns>
        public GameObject GetSingleObject(UIType uiType)
        {
            //[判断]如果此时资源已经在字典中，直接返回
            if (uiDictObject.ContainsKey(uiType.Name))
            {
                return uiDictObject[uiType.Name];
            }

            //[判断]找不到当前画布时，报错
            if (CanvasObj == null)
            {
                CanvasObj = UIMethods.GetInstance().FindCanvas();
            }

            //找到uiType的path，使用Resource.Load()方法加载，并且Instantiate()实例化到CanvasObj画布上
            GameObject gameObject =
                GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(uiType.Path), CanvasObj.transform);
            return gameObject;
        }

        /// <summary>
        /// 往Stack里面压入一个Panel
        /// </summary>
        /// <param name="basePanel">目标Panel</param>
        public void Push(BasePanel basePanel)
        {
            Debug.Log($"{basePanel.uiType.Name}被Push进stack");

            //如果此时Stack已有面板，先将其隐藏，避免误检测
            if (uiStack.Count > 0)
            {
                uiStack.Peek().OnDisable();
            }

            GameObject uiObject = GetSingleObject(basePanel.uiType);
            uiDictObject.Add(basePanel.uiType.Name, uiObject);
            basePanel.ActiveObj = uiObject;

            if (uiStack.Count == 0)
            {
                uiStack.Push(basePanel);
            }
            else
            {
                if (uiStack.Peek().uiType.Name != basePanel.uiType.Name)
                {
                    uiStack.Push(basePanel);
                }

            }

            //初始化Push进来的新面板
            basePanel.OnStart();
        }

        /// <summary>
        /// 出栈操作
        /// </summary>
        /// <param name="isload">isload为真时，Pop全部，isload为假时，Pop栈顶</param>
        public void Pop(bool isload)
        {
            if (isload == true)
            {
                if (uiStack.Count > 0)
                {
                    uiStack.Peek().OnDisable();
                    uiStack.Peek().OnDestroy();
                    GameObject.Destroy(uiDictObject[uiStack.Peek().uiType.Name]);
                    uiDictObject.Remove(uiStack.Peek().uiType.Name);
                    uiStack.Pop();
                    Pop(true);
                }
            }
            else
            {
                if (uiStack.Count > 0)
                {
                    uiStack.Peek().OnDisable();
                    uiStack.Peek().OnDestroy();
                    GameObject.Destroy(uiDictObject[uiStack.Peek().uiType.Name]);
                    uiDictObject.Remove(uiStack.Peek().uiType.Name);
                    uiStack.Pop();

                    if (uiStack.Count > 0)
                    {
                        uiStack.Peek().OnEnable();
                    }
                }
            }
        }
    }
}
