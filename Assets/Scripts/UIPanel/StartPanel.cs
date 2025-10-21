using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SUNFrames;

public class StartPanel : BasePanel
{
    private static string name = "StartPanel";
    private static string path = "Panel/StartPanel";
    public static readonly UIType uiType = new UIType(path, name);

    public StartPanel() : base(uiType)
    {
        
    }

    public override void OnStart()
    {
        base.OnStart();
        UIMethods.GetInstance().GetOrAddComponentInChildren<Button>(ActiveObj, "Back").onClick.AddListener(Back);
        UIMethods.GetInstance().GetOrAddComponentInChildren<Button>(ActiveObj, "Load").onClick.AddListener(Load);
    }

    private void Load()
    {
        Scene2 scene2 = new Scene2();
        GameRoot.GetInstance().SceneControlRoot.LoadScene(scene2.sceneName, scene2);
    }

    private void Back()
    {
        GameRoot.GetInstance().UIManagerRoot.Pop(false);
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDisable()
    {
        Debug.Log("已关闭StratPanel");
        base.OnDisable();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}

