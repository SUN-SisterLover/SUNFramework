using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SwordFrames
{
    public class SingletonMonoBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        //需要我们自己保证它的唯一性
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
                Destroy(this.gameObject); // 销毁当前对象，确保单例唯一性
               
                return;
            }

            //这里应该时protected vitual的 这样允许子类重写 并且若为private 子类并不会调用其中的Awake函数
        }
    }

}