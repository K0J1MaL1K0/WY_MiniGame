using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace AutoBindCode
{
    public class WindowEditor : EditorWindow
    {
        public static WindowEditor window;

        private static CacheConfig cls_Config;
        /// <summary>
        /// 是否绘制窗口
        /// </summary>
        private bool isOpen = true;
        private Vector2 scrollPosition = Vector2.zero;

        [MenuItem("Tools/AutoBindSetting")]
        public static void FnInit()
        {
            cls_Config = CacheConfigTool.FnGetConfig();

            Rect wr = new Rect(-100, -150, 500, 300);
            window = (WindowEditor)EditorWindow.GetWindow(typeof(WindowEditor), true, "AutoBindSetting");
        }

        private void OnGUI()
        {

            if (!isOpen) return;

            try
            {
                scrollPosition = GUILayout.BeginScrollView(scrollPosition);

                GUILayout.Space(20);

                cls_Config.Str_CreateCsPath = EditorGUILayout.TextField("脚本生成路径 : ", cls_Config.Str_CreateCsPath);
                GUILayout.Space(10);

                cls_Config.Str_CreatePrefabPath = EditorGUILayout.TextField("预制体生成路径 : ", cls_Config.Str_CreatePrefabPath);
                GUILayout.Space(10);

                //cls_Config.Str_ConfigPath =  EditorGUILayout.TextField("缓存数据路径 : ", cls_Config.Str_ConfigPath);
                //GUILayout.Space(10);

                cls_Config.Str_DefaultInherit = EditorGUILayout.TextField("默认继承 : ", cls_Config.Str_DefaultInherit);
                GUILayout.Space(10);

                FnmIsUseNameSpace(EditorGUILayout.Toggle("是否使用命名空间 : ", cls_Config.IsUseNameSpace));

                FnmAddSingleButton("重置数据", FnmResetConfig);

                FnmAddSingleButton("保存数据", FnmSaveConfig);

                GUILayout.EndScrollView();
            }
            catch (Exception e)
            {
                Debug.Log("绘制窗口失败,停止绘制" + e);
                isOpen = false;
            }

        }

        #region 事件监听

        private void FnmResetConfig()
        {
            cls_Config = CacheConfigTool.FnGetDefaultConfig();
        }

        private void FnmSaveConfig()
        {
            if(CacheConfigTool.FnSaveConfig(cls_Config))
            {
                Debug.Log("=====》保存成功了！");
            }
            else
            {
                Debug.Log("=====》保存失败了！");
            }
        }

        private void FnmIsUseNameSpace(bool isUse)
        {
            GUILayout.Space(10);

            if (cls_Config.IsUseNameSpace == isUse && !isUse) return;

            cls_Config.IsUseNameSpace = isUse;

            if (!isUse) return;

            cls_Config.Str_NameSpace = EditorGUILayout.TextField("命名空间 : ", cls_Config.Str_NameSpace);
            GUILayout.Space(10);
        }

        #endregion

        #region UI设置

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        private void FnmAddButton(string name, Action action)
        {
            if (GUILayout.Button(name, GUILayout.Width(200)))
            {
                action();
            }
        }

        /// <summary>
        /// 添加水平上单独一个按钮
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        private void FnmAddSingleButton(string name, Action action)
        {
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(150);
            FnmAddButton(name, action);
            GUILayout.EndHorizontal();
        }

        #endregion

    }

#pragma warning disable
    public static class CacheConfigTool
    {
        public static readonly string Str_ConfigName = "CacheConfig.txt";
        public static readonly string Str_ConfigPath = FnGetDefaultPath() + "/Cache/";

        public static CacheConfig FnGetConfig()
        {
            CacheConfig config = FnLoadConfig(Str_ConfigPath + Str_ConfigName);

            if (config == null)
            {
                Debug.Log("没有缓存文件");
                config = FnGetDefaultConfig();
                FnSaveConfig(config);
            }

            config.Str_ConfigPath = Str_ConfigPath;

            return config;
        }

        public static CacheConfig FnGetDefaultConfig()
        {
            CacheConfig config = new CacheConfig();

            string defaultPath = FnGetDefaultPath();

            config.Str_ConfigPath = string.Concat(defaultPath, "/Cache/");
            config.Str_CreateCsPath = string.Concat(defaultPath, "/Script/");
            config.Str_CreatePrefabPath = string.Concat(defaultPath, "/Prefab/");

            return config;
        }

        public static CacheConfig FnLoadConfig(string path)
        {
           return FnmStringToConfig(FnReadStrFromFile(path));
        }

        public static bool FnSaveConfig(CacheConfig config)
        {
            string path = string.Concat(Str_ConfigPath, Str_ConfigName);

            if (File.Exists(path)) File.Delete(path);

            return FnWriteFileFromStr(path, config.ToString());
        }

        public static string FnReadStrFromFile(string path)
        {
            string content = null;
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                content = sr.ReadToEnd();
                sr.Close();
                fs.Close();
            }
            catch(IOException)
            {
                Debug.Log("路径文件不存在 : " + path);
            }
            return content;
        }

        public static bool FnWriteFileFromStr(string path, string str)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.CreateNew);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

                sw.Write(str);
                sw.Flush();

                sw.Close();
                fs.Close();

                if (File.Exists(path)) return true;

            }
            catch (IOException)
            {
                //Debug.Log("保存失败 : " + e.Message);
                return false;
            }

            return false;
        }

        private static string FnGetDefaultPath()
        {
            string defaultPath;

            string[] guidArr = AssetDatabase.FindAssets("AutoBindCode");

            if (guidArr.Length > 0)
            {
                defaultPath = AssetDatabase.GUIDToAssetPath(guidArr[0]);
            }
            else
            {
                guidArr = AssetDatabase.FindAssets("WindowEditor");

                defaultPath = guidArr.Length > 0 ? AssetDatabase.GUIDToAssetPath(guidArr[0]) : string.Concat(Application.dataPath, "/AutoBindCodeCreate");
            }

            return defaultPath;
        }

        private static CacheConfig FnmStringToConfig(string str)
        {
            //Debug.Log("1111 : " + str);

            if (str == null || !str.Contains(";")) return null;

            CacheConfig config = new CacheConfig();

            string[] values = str.Split(';');

            string[] param;
            Dictionary<string, string> nameValue = new Dictionary<string, string>();

            for (int i = 0; i < values.Length; i++)
            {
                param = values[i].Split('|');
                nameValue[param[0]] = param[1];
            }


            config.Str_CreateCsPath = nameValue["Str_CreateCsPath"];
            config.Str_CreatePrefabPath = nameValue["Str_CreatePrefabPath"];
            config.Str_ClassSuffix = nameValue["Str_ClassSuffix"];
            config.Str_ClassName = nameValue["Str_ClassName"];
            config.Str_ParamName = nameValue["Str_ParamName"];
            config.Str_ResetName = nameValue["Str_ResetName"];
            config.Str_DefaultInherit = nameValue["Str_DefaultInherit"];
            config.Str_NameSpace = nameValue["Str_NameSpace"];
            config.IsUseNameSpace = nameValue["IsUseNameSpace"] == bool.TrueString ? true : false;
            //Debug.Log(nameValue["IsUseNameSpace"] + " : "  + config.IsUseNameSpace);

            return config;
        }

    }

    [Serializable]
    public class CacheConfig
    {
        public string Str_ConfigPath;
        public string Str_CreateCsPath;                         //生成脚本路径
        public string Str_CreatePrefabPath;                     //生成预制体路径
        public string Str_ClassSuffix = "Param";                //成员类后缀名
        public string Str_ClassName = "ClassTemplate.txt";      //主类的样本文件名
        public string Str_ParamName = "ParamTemplate.txt";      //成员类...
        public string Str_ResetName = "ResetTemplate.txt";      //重置类...
        public string Str_DefaultInherit = "MonoBehaviour";     //默认继承
        public string Str_NameSpace = "AutoBindCode";           //默认命名空间
        public bool IsUseNameSpace;                             //是否使用命名空间

        public CacheConfig()
        {
            Str_ClassSuffix = "Param";
            Str_ClassName = "ClassTemplate.txt";
            Str_ParamName = "ParamTemplate.txt";
            Str_ResetName = "ResetTemplate.txt";
            Str_DefaultInherit = "MonoBehaviour";
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Str_CreateCsPath|" + Str_CreateCsPath);
            sb.Append(";");
            sb.Append("Str_CreatePrefabPath|" + Str_CreatePrefabPath);
            sb.Append(";");
            sb.Append("Str_ClassSuffix|" + Str_ClassSuffix);
            sb.Append(";");
            sb.Append("Str_ClassName|" + Str_ClassName);
            sb.Append(";");
            sb.Append("Str_ParamName|" + Str_ParamName);
            sb.Append(";");
            sb.Append("Str_ResetName|" + Str_ResetName);
            sb.Append(";");
            sb.Append("Str_DefaultInherit|" + Str_DefaultInherit);
            sb.Append(";");
            sb.Append("Str_NameSpace|" + Str_NameSpace);
            sb.Append(";");
            sb.Append("IsUseNameSpace|" + IsUseNameSpace);

            return sb.ToString();
        }
    }
}