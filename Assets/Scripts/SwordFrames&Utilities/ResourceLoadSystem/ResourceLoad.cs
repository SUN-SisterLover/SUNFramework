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
            //�����GameObj ʵ����֮��ֱ��ʹ��
            if (res is GameObject)
            {
                return GameObject.Instantiate(res) as T;
            }
            else return res;
        }
        public T LoadResources<T>(string name, Transform fatherTrans) where T : Object//��������λ��
        {

            T res = Resources.Load<T>(name);
            //�����GameObj ʵ����֮��ֱ��ʹ��
            if (res is GameObject)
            {
                return GameObject.Instantiate(res, fatherTrans) as T;
            }
            else return res;
        }
        //��Դͬ������

        public void LoadResourcesAsync<T>(string name, UnityAction<T> callBack) where T : Object
        {
            MonoManager.Instance.StartCoroutine(IELoadResourcesAsync(name, callBack));
        }//��Դ�첽���� ���Դ�һ���ص�����
        public IEnumerator IELoadResourcesAsync<T>(string name, UnityAction<T> callBack) where T : Object
        {
            ResourceRequest r = Resources.LoadAsync<T>(name);
            yield return r;
            if (r.asset is GameObject)//�������Ϸ����

                callBack(GameObject.Instantiate(r.asset) as T);
            else
                callBack(r.asset as T);






        }




    }

}