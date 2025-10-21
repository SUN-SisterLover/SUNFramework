using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SwordFrames
{
    public class SingletonMonoBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        //��Ҫ�����Լ���֤����Ψһ��
        public static T Instance
        {
            get
            {
                return instance;
            }
        }
        protected  virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
              
            }
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject); // ���ٵ�ǰ����ȷ������Ψһ��
               
                return;
            }

            //����Ӧ��ʱprotected vitual�� ��������������д ������Ϊprivate ���ಢ����������е�Awake����
        }
    }

}