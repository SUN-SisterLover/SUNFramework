using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SwordFrames
{

    public class EventCenter : Singleton<EventCenter>
    {
        public interface IEventInfo
        {

        }
       

        public class EventInfo<T> : IEventInfo
        {
            public UnityAction<T> actions;
            public EventInfo(UnityAction<T> action)
            {
                actions += action;
            }

        }


        //��Ҫװ���ͣ������¼����������ǵ�����ֻ�ܳ�ʼ��һ�Σ���ô�����������˷����࣬�Ǿ�ֻ��װһ������
        //�������ǳ��԰�������һ�� �ø���װ���� ���Ǹ���Ҳ�Ǵ����͵ģ�
        //��ô������Ȼ��Ȼ�뵽�ӿ� ����̳�һ���սӿ� ��ӿ� ʹ��ʱ��ǿ��ת��


        public class EventInfo : IEventInfo
        {
            public UnityAction actions;
            public EventInfo(UnityAction action)
            {
                actions += action;
            }
        }
        public Dictionary<string, IEventInfo> eventDic = new();
      
        //�в����¼�


        //name:�¼����֣� action:Ҫ��ӵĺ�����
        public void AddEventListener<T>(string eventName, UnityAction<T> action)
        {
            if (eventDic.ContainsKey(eventName))
            {
                //eventDic[eventName] += action;
                (eventDic[eventName] as EventInfo<T>).actions += action;
                //�ⲽ��Ŀ���� ��eventDic�ж�ӦeventName�е�ֵ����IEventInfo ת��ΪEventInfo<T>
            }
            else
            {
                eventDic.Add(eventName, new EventInfo<T>(action));
            }



        }
        //�޲����¼�
        public void AddEventListener(string eventName, UnityAction action)
        {
            if (eventDic.ContainsKey(eventName))
            {
                if (eventDic[eventName] != null)
                {
                    (eventDic[eventName] as EventInfo).actions += action;
                }
                else
                {
                    Debug.Log(eventName + "Ϊ��");
                }
            }
            else
            {
                eventDic.Add(eventName, new EventInfo(action));
                //(eventDic[eventName] as EventInfo).actions += action;
            }
        }

        //�����¼� �в���
        public void TriggerEvent<T>(string eventName, T info)
        {
            //�����¼�
            if (eventDic.ContainsKey(eventName))
            {

                (eventDic[eventName] as EventInfo<T>).actions?.Invoke(info);
            }
            else
            {
                Debug.Log("�������¼�" + eventName);
            }

        }
        //�����¼� �޲���
        public void TriggerEvent(string eventName)
        {
            //�����¼�
            if (eventDic.ContainsKey(eventName))
            {
                (eventDic[eventName] as EventInfo).actions?.Invoke();
            }
            else
            {
                Debug.Log("�������¼�" + eventName);
            }

        }
        //�ڴ˽��� ͬ���¼���һ���¼�ͬʱװ���вκ��޲Σ���������ԭ����TriggerEvent�л���ж����ת��
        //�������е��� EventInfo<T> �е��� EventInfo ���Ի���ִ��� ��Ϊ���߲��ܻ���ת��

        //�Ƴ��¼�  �в���
        public void RemoveEventListener<T>(string eventName, UnityAction<T> action)
        {
            if (eventDic.ContainsKey(eventName))
            {
                (eventDic[eventName] as EventInfo<T>).actions -= action;

            }

        }
        //�Ƴ��¼����޲�����
        public void RemoveEventListener(string eventName, UnityAction action)
        {
            if (eventDic.ContainsKey(eventName))
            {
                (eventDic[eventName] as EventInfo).actions -= action;

            }

        }

        //��������¼� ���鳡���л������
        public void Clear()
        {
            eventDic.Clear();
        }













    }
}
