using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SUNFrames
{

    public class SingletonMonoBaseAuto<T> : MonoBehaviour where T : MonoBehaviour
    {
        //这个不用挂载在空物体上，调的时候自动创建
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    //创建物体
                    GameObject go = new GameObject();
                    //设定名字
                    go.name = typeof(T).ToString();
                    //添加组件
                    // DontDestroyOnLoad(go);
                    instance = go.AddComponent<T>();

                }

                return instance;
            }
        }
        private void OnDestroy()
        {
            // 当对象被销毁时，确保清理 instance 引用
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}