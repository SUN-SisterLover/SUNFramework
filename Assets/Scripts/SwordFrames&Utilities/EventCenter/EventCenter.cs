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


        //想要装泛型，但是事件管理中心是单例，只能初始化一次，那么如果把它变成了泛型类，那就只能装一种类型
        //所以我们尝试把它包裹一层 用父类装子类 但是父类也是带泛型的，
        //那么我们自然而然想到接口 父类继承一个空接口 存接口 使用时候强制转换


        public class EventInfo : IEventInfo
        {
            public UnityAction actions;
            public EventInfo(UnityAction action)
            {
                actions += action;
            }
        }
        public Dictionary<string, IEventInfo> eventDic = new();
      
        //有参数事件


        //name:事件名字， action:要添加的函数们
        public void AddEventListener<T>(string eventName, UnityAction<T> action)
        {
            if (eventDic.ContainsKey(eventName))
            {
                //eventDic[eventName] += action;
                (eventDic[eventName] as EventInfo<T>).actions += action;
                //这步的目的是 把eventDic中对应eventName中的值，即IEventInfo 转化为EventInfo<T>
            }
            else
            {
                eventDic.Add(eventName, new EventInfo<T>(action));
            }



        }
        //无参数事件
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
                    Debug.Log(eventName + "为空");
                }
            }
            else
            {
                eventDic.Add(eventName, new EventInfo(action));
                //(eventDic[eventName] as EventInfo).actions += action;
            }
        }

        //触发事件 有参数
        public void TriggerEvent<T>(string eventName, T info)
        {
            //触发事件
            if (eventDic.ContainsKey(eventName))
            {

                (eventDic[eventName] as EventInfo<T>).actions?.Invoke(info);
            }
            else
            {
                Debug.Log("不存在事件" + eventName);
            }

        }
        //触发事件 无参数
        public void TriggerEvent(string eventName)
        {
            //触发事件
            if (eventDic.ContainsKey(eventName))
            {
                (eventDic[eventName] as EventInfo).actions?.Invoke();
            }
            else
            {
                Debug.Log("不存在事件" + eventName);
            }

        }
        //在此解释 同名事件（一个事件同时装了有参和无参）触发报错原因：在TriggerEvent中会进行对象的转型
        //但里面有的是 EventInfo<T> 有的是 EventInfo 所以会出现错误 因为二者不能互相转换

        //移除事件  有参数
        public void RemoveEventListener<T>(string eventName, UnityAction<T> action)
        {
            if (eventDic.ContainsKey(eventName))
            {
                (eventDic[eventName] as EventInfo<T>).actions -= action;

            }

        }
        //移出事件（无参数）
        public void RemoveEventListener(string eventName, UnityAction action)
        {
            if (eventDic.ContainsKey(eventName))
            {
                (eventDic[eventName] as EventInfo).actions -= action;

            }

        }

        //清除所有事件 建议场景切换后清除
        public void Clear()
        {
            eventDic.Clear();
        }













    }
}
