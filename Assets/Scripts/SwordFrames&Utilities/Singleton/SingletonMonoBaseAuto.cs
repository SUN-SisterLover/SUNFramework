using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SwordFrames
{

    public class SingletonMonoBaseAuto<T> : MonoBehaviour where T : MonoBehaviour
    {
        //������ù����ڿ������ϣ�����ʱ���Զ�����
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    //��������
                    GameObject go = new GameObject();
                    //�趨����
                    go.name = typeof(T).ToString();
                    //������
                    // DontDestroyOnLoad(go);
                    instance = go.AddComponent<T>();

                }

                return instance;
            }
        }
        private void OnDestroy()
        {
            // ����������ʱ��ȷ������ instance ����
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}