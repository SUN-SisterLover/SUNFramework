using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
namespace SwordFrames
{
    public class MonoManager : Singleton<MonoManager>
    {
        public MonoBase monoBase;

        public MonoManager()
        {
            GameObject obj = new GameObject("MonoBase");
            monoBase = obj.AddComponent<MonoBase>();
        }
        //初始化 生成挂载mono的MonoBase脚本
        public void AddUpdateListener(UnityAction action)
        {
            monoBase.AddUpdateListener(action);
        }
        //添加帧时间更新函数
        public void RenewUpdateListener(UnityAction action)
        {
            monoBase.RemoveUpdateListener(action);
        }
        //解绑帧时间更新函数
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return monoBase.StartCoroutine(routine);
        }
        //不继承mono类的开始协程方法接口
        public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
        {
            return monoBase.StartCoroutine(methodName, value);
        }
        public Coroutine StartCoroutine(string methodName)
        {
            return monoBase.StartCoroutine(methodName);
        }





    }
}