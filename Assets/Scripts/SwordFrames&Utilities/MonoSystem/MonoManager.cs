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
        //��ʼ�� ���ɹ���mono��MonoBase�ű�
        public void AddUpdateListener(UnityAction action)
        {
            monoBase.AddUpdateListener(action);
        }
        //���֡ʱ����º���
        public void RenewUpdateListener(UnityAction action)
        {
            monoBase.RemoveUpdateListener(action);
        }
        //���֡ʱ����º���
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return monoBase.StartCoroutine(routine);
        }
        //���̳�mono��Ŀ�ʼЭ�̷����ӿ�
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