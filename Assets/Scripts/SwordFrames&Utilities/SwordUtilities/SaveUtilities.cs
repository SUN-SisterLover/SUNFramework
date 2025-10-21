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
                    Directory.CreateDirectory(path); // �ж��ļ����Ƿ���� �����ھʹ����ļ���
                }

                string[] files = Directory.GetFiles(path, "*.json");
                //��浵 �ж��ļ������Ƿ��������һ��json�ļ�
                return files.Length > 0;

        }




        /// <summary>
        /// ���浵����Ϊ����+Data ��浵����Ϊ����+ʱ���+data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="multiple"></param>
        public void SaveData<T>(T data, bool multiple = false)
        {
            if (typeof(T).IsSubclassOf(typeof(MonoBehaviour)))
            {
                //�������Ǽ̳���Mono�Ķ���Ļ�ֱ�ӷ���
                Debug.LogWarning("�������л��ö���,�ö���̳���MonoBehaviour,��������Ϊ" + typeof(T).ToString());
                return;
            }

            string path = multiple ? multipleJsonPath : jsonPath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path); // �ж��ļ����Ƿ���� �����ھʹ����ļ���
            }
            string json = JsonUtility.ToJson(data, true);
            if (!multiple)
            {

                File.WriteAllText(Path.Combine(path, typeof(T).ToString() + ".json"), json);
            }
            //���ݲ������жϴ浵·��

     


            //���浵
            else
            {//��浵
                string pathStr = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + typeof(T).ToString() + ".json";
                //����ð�� �ǺϷ���
                File.WriteAllText(Path.Combine(path, pathStr), json);
                //����ʱ���
            }
            //д���ļ�
        }

        /// <summary>
        /// ���浵����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Load<T>()
        {
          

            //��ȡ���浵������data����
            if (!ContainJson(false))
            {
                Debug.Log("·���²������ļ�! �����л�ʧ�ܣ�����Ĭ��ֵ");
                return default;
            }


            string json = File.ReadAllText(Path.Combine(jsonPath,typeof(T).ToString() + ".json"));

            return JsonUtility.FromJson<T>(json);


        }


        /// <summary>
        /// ��浵��֧�ַ����л�Mono����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public List<T> LoadMultiple<T>() 
        {//��ȡ����浵������data�б�
            if (typeof(T).IsSubclassOf(typeof(MonoBehaviour)))
            {
                //�������Ǽ̳���Mono�Ķ���Ļ�ֱ�ӷ���
                Debug.LogWarning("�������л��ö����б�,�ö���̳���MonoBehaviour,��������Ϊ" + typeof(T).ToString());
                return default;
            }

            var result = new List<T>();  

            if (ContainJson(true))
            {
                foreach (string file in Directory.GetFiles(multipleJsonPath, "*.json"))
                {
                    //����һ����̬���������ڻ�ȡָ��·��������ƥ��ָ��ģʽ���ļ�������ʹ��"*.json"��Ϊģʽ����ʾ��ȡ������չ��Ϊ.json���ļ���
                 
                    string json = File.ReadAllText(file);

                    T singleData = JsonUtility.FromJson<T>(json);

                    result.Add(singleData);
                    //�����ȡ

                }
                return result;
            }
            else
            {
                Debug.Log("·���²������ļ�! �����л�ʧ�ܣ�����Ĭ��ֵ");
                return new List<T>(0);
            }
        }
       

}

}

