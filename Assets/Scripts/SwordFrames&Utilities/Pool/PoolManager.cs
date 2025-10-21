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
        {//������ ���������� ��������Ϊ���ܵ�PoolMgr��������

            //һ������ װ�ľ���ͬ�ֶ������������

            fatherObj = new GameObject(obj.name + "Pool");
            //�����������ĸ�����
            fatherObj.transform.parent = poolObj.transform;//����Ǹ�����ĸ����� ����PoolObj
            //��ʼ������
            objQueue = new Queue<GameObject>();
            PushObj(obj);

        }
        public GameObject GetObj()
        {
            if (objQueue.Count > 0)
            {
                GameObject obj = objQueue.Dequeue();
                //�Ӷ����г���һ��
                obj.SetActive(true);
                // obj.transform.parent = null;
                obj.transform.SetParent(null);
                //�Ͽ����ӹ�ϵ
                return obj;
            }
            else
            {
                Debug.LogWarning("������Ķ���Ϊ��" + fatherObj.name);
                return null;
            }
        }
        public void PushObj(GameObject obj)
        {
            obj.SetActive(false);
            //������� 
            obj.transform.SetParent(fatherObj.transform);
            //obj.transform.parent=fatherObj.transform;
            //���ø�����
            objQueue.Enqueue(obj);
            //����

        }

    }
    public class PoolManager : Singleton<PoolManager>
    {
        //һ���ֵ� �涫�� 
        //�涫�� �ö��� ֮ǰ���� �Ƿ����
        public Dictionary<string, PoolData> poolDic = new();
        private GameObject poolObj;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>�������� ��Resources�ļ������
        /// <param name="callBack"></param>����֮��Ҫ��������
        public void GetObjAsync(string name, UnityAction<GameObject> callBack)
        {

            if (poolDic.ContainsKey(name) && poolDic[name].objQueue.Count > 0)
            { //������� ��������>0
                //obj = poolDic[name].GetObj();
                callBack(poolDic[name].GetObj());
                Debug.Log("��������� Pool");
                //ֱ�ӻص�
            }
            else
            {
                // obj= GameObject.Instantiate(Resources.Load<GameObject>(name));
                //û�оͼ���һ�� 
                //�첽����֮�󷵻ظ��������ʹ��
                ResourceLoad.Instance.LoadResourcesAsync<GameObject>(name, (obj) =>
                {
                    obj.name = name;
                    callBack(obj);
                });
            }

            //   return obj;
        }

        //ֱ�ӻ�ȡ����
        public GameObject GetObj(string name)
        {

            if (poolDic.ContainsKey(name) && poolDic[name].objQueue.Count > 0)
            { //������� ��������>0
                //obj = poolDic[name].GetObj();
                return (poolDic[name].GetObj());
                //ֱ�ӻص�
            }
            else
            {
                // obj= GameObject.Instantiate(Resources.Load<GameObject>(name));
                //û�оͼ���һ�� 
                //�첽����֮�󷵻ظ��������ʹ��
                return ResourceLoad.Instance.LoadResources<GameObject>(name);
            }

            //   return obj;
        }
        public GameObject GetObj(string name, Transform fatherTrans)//�������ø������ ����UI���ִ�λ
        {

            if (poolDic.ContainsKey(name) && poolDic[name].objQueue.Count > 0)
            { //������� ��������>0
                //obj = poolDic[name].GetObj();
                var obj = (poolDic[name].GetObj());
                obj.transform.SetParent(fatherTrans);
                return obj;

                //ֱ�ӻص�
            }
            else
            {
                // obj= GameObject.Instantiate(Resources.Load<GameObject>(name));
                //û�оͼ���һ�� 
                //�첽����֮�󷵻ظ��������ʹ��
                return ResourceLoad.Instance.LoadResources<GameObject>(name, fatherTrans);
            }

            //   return obj;
        }

        public void PushObj(string name, GameObject obj)
        {
            //������֮�����������������
            if (poolObj == null)
            {

                poolObj = new GameObject("PoolManager");

            }
            //obj.transform.SetParent(poolObj.transform);
            //���ø��ڵ� �������ע�͵��� ��Ϊ���ø��ڵ��Ѿ��������PushObj�������
            obj.SetActive(false);
            if (poolDic.ContainsKey(name))
            {//����иö���ĳ��� ֱ��������Ӿ���
                poolDic[name].PushObj(obj);
                //����
            }
            else
            {
                poolDic.Add(name, new PoolData(obj, poolObj));
                //û�еĻ����½�һ��Data���� ��ִ�г�ʼ�����������캯����
            }


        }
        public void ClearPool()
        {
            poolDic.Clear();
        }

    }
}
