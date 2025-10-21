using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUNFrames;

namespace SUNFrames
{
    /// <summary>
    /// [基类]BasePanel给所有UI面板提供统一的生命周期与公共数据
    /// UIManager可以按相同方式创建/打开/关闭/销毁任意面板
    /// </summary>
    public class BasePanel
    {
        public UIType uiType;

        public GameObject ActiveObj;

        public BasePanel(UIType _uiType)
        {
            uiType = _uiType;
        }

        //通过GetOrAddComponent方法获取CanvasGroup组件下的interactabel，修改当前UI是否可交互
        public virtual void OnStart()
        {
            Debug.Log($"{uiType.Name}开始使用！");
            UIMethods.GetInstance().GetOrAddComponent<CanvasGroup>(ActiveObj).interactable = true;
        }

        public virtual void OnEnable()
        {
            UIMethods.GetInstance().GetOrAddComponent<CanvasGroup>(ActiveObj).interactable = true;
        }

        public virtual void OnDisable()
        {
            UIMethods.GetInstance().GetOrAddComponent<CanvasGroup>(ActiveObj).interactable = false;
        }

        public virtual void OnDestroy()
        {
            UIMethods.GetInstance().GetOrAddComponent<CanvasGroup>(ActiveObj).interactable = false;
        }
    }
}