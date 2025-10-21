using System.Collections;


using UnityEngine;
using UnityEngine.Events;

namespace SwordFrames
{
    public class ResourceLoad : Singleton<ResourceLoad>
    {
        public T LoadResources<T>(string name) where T : Object
        {

            T res = Resources.Load<T>(name);
            //如果是GameObj 实例化之后直接使用
            if (res is GameObject)
            {
                return GameObject.Instantiate(res) as T;
            }
            else return res;
        }
        public T LoadResources<T>(string name, Transform fatherTrans) where T : Object//可以设置位置
        {

            T res = Resources.Load<T>(name);
            //如果是GameObj 实例化之后直接使用
            if (res is GameObject)
            {
                return GameObject.Instantiate(res, fatherTrans) as T;
            }
            else return res;
        }
        //资源同步加载

        public void LoadResourcesAsync<T>(string name, UnityAction<T> callBack) where T : Object
        {
            MonoManager.Instance.StartCoroutine(IELoadResourcesAsync(name, callBack));
        }//资源异步加载 可以传一个回调函数
        public IEnumerator IELoadResourcesAsync<T>(string name, UnityAction<T> callBack) where T : Object
        {
            ResourceRequest r = Resources.LoadAsync<T>(name);
            yield return r;
            if (r.asset is GameObject)//如果是游戏物体

                callBack(GameObject.Instantiate(r.asset) as T);
            else
                callBack(r.asset as T);






        }




    }

}