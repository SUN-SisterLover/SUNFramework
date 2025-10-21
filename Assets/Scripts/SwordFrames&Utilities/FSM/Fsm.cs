using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SwordFrames
{
    public interface IState
    {
        public void OnEnter();//��ͨ��OnEnter��ע�� ��ͨ������ʵ����Ĺ��캯����ע�� ��board��ͬ OnEenterֻ���߼����� ʵ�����ݺ��߼����� 
        public void OnUpdate();
        public void OnFixUpdate();
        public void OnExit();
    }

    public interface IDataBoard
    {
        public void UpdateBoard();//ֻ�ṩ���·��� ��ʼ���ھ���ʵ����Ĺ��캯��
    }

    public class Fsm 
    {
        public IState currState;

        public IDataBoard board;//�������

        // �ֵ�ĳɹ���ί��
        public Dictionary<Type, Func<Fsm, IState>> stateFactories;//����ί�� ���ڽ����ⲿע��
        public Dictionary<Type, IState> stateCache;//״̬���� ����ӵ������state

        public Fsm(IDataBoard dataBoard)
        {
                stateFactories = new Dictionary<Type, Func<Fsm, IState>>();//���Ǹ�Func  Fsm�ǲ���  IState�Ƿ���ֵ ����������Ҫ����������������
                stateCache = new Dictionary<Type, IState>();
                board = dataBoard;
        }



        #region �������ں����͸��ºڰ�
        //�������ݰ�
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




        // ע�᣺lambda ���� ***�����ڼ��*** �Ĺ���������
        public void AddState<T>(Func<Fsm, T> factory) where T : IState
        {
            var tp = typeof(T);
            stateFactories[tp] = factory as Func<Fsm, IState>; //����ֱ�Ӽӵ�factories���� ������stateCache �ȵ������õ�ʱ�� ��ʵ����
        }

        // �л�����һ�� new���Ժ���
        public void SwitchState<T>() where T : IState
        {
            var tp = typeof(T);
            if (!stateFactories.TryGetValue(tp, out var fac))
            {
                Debug.LogError($"FSM δע��״̬ {tp.Name}");
                return;
            }

            currState?.OnExit();

            if (!stateCache.TryGetValue(tp, out currState))
            {
                currState = fac(this);   // ���� lambda���㷴��   �õ��ټ��� ������
                stateCache[tp] = currState;
            }

            currState.OnEnter();
        }



        //�Ƴ�״̬
        public void RemoveState<T>() where T : IState
        {
             var tp = typeof(T);

            // �����ǰ�����������״̬�����˳�
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
