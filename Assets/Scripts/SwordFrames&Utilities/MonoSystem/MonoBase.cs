using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace SwordFrames
{
    public class MonoBase : MonoBehaviour
    {
        public UnityAction updateAction;
        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        private void Update()
        {
            updateAction?.Invoke();
        }
        public void AddUpdateListener(UnityAction action)
        {
            updateAction += action;
        }
        public void RemoveUpdateListener(UnityAction action)
        { updateAction -= action; }


    }
}
