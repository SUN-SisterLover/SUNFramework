using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace SwordFrames
{

    public class PoolData
    {
        public Queue<GameObject> objQueue;
        public GameObject fatherObj;

        public PoolData(GameObject obj, GameObject poolObj)
        {//给抽屉 创建父对象 把它们作为们总的PoolMgr的子物体

            //一个抽屉 装的就是同种对象的所有物体

            fatherObj = new GameObject(obj.name + "Pool");
            //这个是子物体的父对象
            fatherObj.transform.parent = poolObj.transform;//这个是父对象的父对象 就是PoolObj
            //初始化队列
            objQueue = new Queue<GameObject>();
            PushObj(obj);

        }
        public GameObject GetObj()
        {
            if (objQueue.Count > 0)
            {
                GameObject obj = objQueue.Dequeue();
                //从队列中出来一个
                obj.SetActive(true);
                // obj.transform.parent = null;
                obj.transform.SetParent(null);
                //断开父子关系
                return obj;
            }
            else
            {
                Debug.LogWarning("子物体的队列为空" + fatherObj.name);
                return null;
            }
        }
        public void PushObj(GameObject obj)
        {
            obj.SetActive(false);
            //清除物体 
            obj.transform.SetParent(fatherObj.transform);
            //obj.transform.parent=fatherObj.transform;
            //设置父物体
            objQueue.Enqueue(obj);
            //进队

        }

    }
    public class PoolManager : Singleton<PoolManager>
    {
        //一个字典 存东西 
        //存东西 拿东西 之前考虑 是否存在
        public Dictionary<string, PoolData> poolDic = new();
        private GameObject poolObj;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>物体名字 在Resources文件夹里的
        /// <param name="callBack"></param>加载之后要做的事情
        public void GetObjAsync(string name, UnityAction<GameObject> callBack)
        {

            if (poolDic.ContainsKey(name) && poolDic[name].objQueue.Count > 0)
            { //如果存在 并且数量>0
                //obj = poolDic[name].GetObj();
                callBack(poolDic[name].GetObj());
                Debug.Log("获得了物体 Pool");
                //直接回调
            }
            else
            {
                // obj= GameObject.Instantiate(Resources.Load<GameObject>(name));
                //没有就加载一个 
                //异步加载之后返回给外面进行使用
                ResourceLoad.Instance.LoadResourcesAsync<GameObject>(name, (obj) =>
                {
                    obj.name = name;
                    callBack(obj);
                });
            }

            //   return obj;
        }

        //直接获取物体
        public GameObject GetObj(string name)
        {

            if (poolDic.ContainsKey(name) && poolDic[name].objQueue.Count > 0)
            { //如果存在 并且数量>0
                //obj = poolDic[name].GetObj();
                return (poolDic[name].GetObj());
                //直接回调
            }
            else
            {
                // obj= GameObject.Instantiate(Resources.Load<GameObject>(name));
                //没有就加载一个 
                //异步加载之后返回给外面进行使用
                return ResourceLoad.Instance.LoadResources<GameObject>(name);
            }

            //   return obj;
        }
        public GameObject GetObj(string name, Transform fatherTrans)//可以设置父物体的 避免UI布局错位
        {

            if (poolDic.ContainsKey(name) && poolDic[name].objQueue.Count > 0)
            { //如果存在 并且数量>0
                //obj = poolDic[name].GetObj();
                var obj = (poolDic[name].GetObj());
                obj.transform.SetParent(fatherTrans);
                return obj;

                //直接回调
            }
            else
            {
                // obj= GameObject.Instantiate(Resources.Load<GameObject>(name));
                //没有就加载一个 
                //异步加载之后返回给外面进行使用
                return ResourceLoad.Instance.LoadResources<GameObject>(name, fatherTrans);
            }

            //   return obj;
        }

        public void PushObj(string name, GameObject obj)
        {
            //用完了之后，往抽屉里面放物体
            if (poolObj == null)
            {

                poolObj = new GameObject("PoolManager");

            }
            //obj.transform.SetParent(poolObj.transform);
            //设置父节点 上面代码注释掉了 因为设置父节点已经在下面的PushObj中完成了
            obj.SetActive(false);
            if (poolDic.ContainsKey(name))
            {//如果有该对象的池子 直接往里面加就行
                poolDic[name].PushObj(obj);
                //进队
            }
            else
            {
                poolDic.Add(name, new PoolData(obj, poolObj));
                //没有的话就新建一个Data对象 再执行初始化操作（构造函数）
            }


        }
        public void ClearPool()
        {
            poolDic.Clear();
        }

    }
}
