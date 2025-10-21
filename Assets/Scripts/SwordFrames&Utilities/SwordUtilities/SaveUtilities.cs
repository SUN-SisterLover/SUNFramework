using System.Collections.Generic;
using UnityEngine;
using System.IO;

using System;
namespace SwordFrames
{ 
public class SaveUtilities : SingletonMonoBaseAuto<SaveUtilities>
{

        public string jsonPath;
        public string multipleJsonPath;

        private void Awake()
        {
                jsonPath = Path.Combine(Application.persistentDataPath, "SaveData");
                multipleJsonPath = Path.Combine(Application.persistentDataPath, "MultipleSaveData");
        }
   






        public bool ContainJson(bool multiple = false)
        {
                string path = multiple ? multipleJsonPath : jsonPath;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path); // 判断文件夹是否存在 不存在就创建文件夹
                }

                string[] files = Directory.GetFiles(path, "*.json");
                //多存档 判断文件夹中是否存在至少一个json文件
                return files.Length > 0;

        }




        /// <summary>
        /// 单存档命名为类名+Data 多存档命名为类名+时间戳+data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="multiple"></param>
        public void SaveData<T>(T data, bool multiple = false)
        {
            if (typeof(T).IsSubclassOf(typeof(MonoBehaviour)))
            {
                //如果这个是继承自Mono的对象的话直接返回
                Debug.LogWarning("不能序列化该对象,该对象继承自MonoBehaviour,对象类型为" + typeof(T).ToString());
                return;
            }

            string path = multiple ? multipleJsonPath : jsonPath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path); // 判断文件夹是否存在 不存在就创建文件夹
            }
            string json = JsonUtility.ToJson(data, true);
            if (!multiple)
            {

                File.WriteAllText(Path.Combine(path, typeof(T).ToString() + ".json"), json);
            }
            //根据参数来判断存档路径

     


            //单存档
            else
            {//多存档
                string pathStr = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + typeof(T).ToString() + ".json";
                //不带冒号 是合法的
                File.WriteAllText(Path.Combine(path, pathStr), json);
                //加入时间戳
            }
            //写入文件
        }

        /// <summary>
        /// 单存档读档
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Load<T>()
        {
          

            //读取单存档，返回data数据
            if (!ContainJson(false))
            {
                Debug.Log("路径下不存在文件! 反序列化失败，返回默认值");
                return default;
            }


            string json = File.ReadAllText(Path.Combine(jsonPath,typeof(T).ToString() + ".json"));

            return JsonUtility.FromJson<T>(json);


        }


        /// <summary>
        /// 多存档不支持反序列化Mono对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public List<T> LoadMultiple<T>() 
        {//读取多个存档，返回data列表
            if (typeof(T).IsSubclassOf(typeof(MonoBehaviour)))
            {
                //如果这个是继承自Mono的对象的话直接返回
                Debug.LogWarning("不能序列化该对象列表,该对象继承自MonoBehaviour,对象类型为" + typeof(T).ToString());
                return default;
            }

            var result = new List<T>();  

            if (ContainJson(true))
            {
                foreach (string file in Directory.GetFiles(multipleJsonPath, "*.json"))
                {
                    //这是一个静态方法，用于获取指定路径下所有匹配指定模式的文件。这里使用"*.json"作为模式，表示获取所有扩展名为.json的文件。
                 
                    string json = File.ReadAllText(file);

                    T singleData = JsonUtility.FromJson<T>(json);

                    result.Add(singleData);
                    //逐个读取

                }
                return result;
            }
            else
            {
                Debug.Log("路径下不存在文件! 反序列化失败，返回默认值");
                return new List<T>(0);
            }
        }
       

}

}

