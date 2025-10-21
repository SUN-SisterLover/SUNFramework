using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SwordFrames
{
    public interface IState
    {
        public void OnEnter();//不通过OnEnter来注入 而通过具体实现类的构造函数来注入 和board相同 OnEenter只做逻辑处理 实现数据和逻辑分离 
        public void OnUpdate();
        public void OnFixUpdate();
        public void OnExit();
    }

    public interface IDataBoard
    {
        public void UpdateBoard();//只提供更新方法 初始化在具体实现类的构造函数
    }

    public class Fsm 
    {
        public IState currState;

        public IDataBoard board;//数据面板

        // 字典改成工厂委托
        public Dictionary<Type, Func<Fsm, IState>> stateFactories;//储存委托 用于接收外部注册
        public Dictionary<Type, IState> stateCache;//状态缓存 这里加的是真的state

        public Fsm(IDataBoard dataBoard)
        {
                stateFactories = new Dictionary<Type, Func<Fsm, IState>>();//这是个Func  Fsm是参数  IState是返回值 所以我们需要东西把它储存起来
                stateCache = new Dictionary<Type, IState>();
                board = dataBoard;
        }



        #region 生命周期函数和更新黑板
        //更新数据版
        public void UpdateDataBoard()
        {
            board.UpdateBoard();
        }






        public void FsmUpdate()
        {
            currState.OnUpdate();
        }

        public void FsmFixUpdate()
        {
            currState.OnFixUpdate();
        }



        #endregion




        // 注册：lambda 里是 ***编译期检查*** 的构造器调用
        public void AddState<T>(Func<Fsm, T> factory) where T : IState
        {
            var tp = typeof(T);
            stateFactories[tp] = factory as Func<Fsm, IState>; //这里直接加到factories即可 而不是stateCache 等到真正用的时候 再实例化
        }

        // 切换：第一次 new，以后复用
        public void SwitchState<T>() where T : IState
        {
            var tp = typeof(T);
            if (!stateFactories.TryGetValue(tp, out var fac))
            {
                Debug.LogError($"FSM 未注册状态 {tp.Name}");
                return;
            }

            currState?.OnExit();

            if (!stateCache.TryGetValue(tp, out currState))
            {
                currState = fac(this);   // 调用 lambda，零反射   用到再加载 懒加载
                stateCache[tp] = currState;
            }

            currState.OnEnter();
        }



        //移除状态
        public void RemoveState<T>() where T : IState
        {
             var tp = typeof(T);

            // 如果当前正好在跑这个状态，先退出
            if (currState?.GetType() == tp)
            {
                currState.OnExit();
                currState = null;
            }


            stateCache.Remove(tp);  
            stateFactories.Remove(tp);
              
            
        }


        public IState GetCurrState()=>currState;

       

       

    }

}
