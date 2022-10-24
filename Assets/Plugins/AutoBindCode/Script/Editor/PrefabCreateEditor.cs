using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEditor.Callbacks;
using System.Reflection;

namespace AutoBindCode
{

#pragma warning disable
    public class PrefabCreateEditor : UnityEditor.AssetModificationProcessor
    {
        static CacheConfig cls_Config;
        static string Str_Info;
        static string Str_ClassTemplate;
        static string Str_ParamTemplate;
        static string Str_ClassPath;

        static void FnmReadTemplateTxt()
        {
            cls_Config = CacheConfigTool.FnGetConfig();
            FnmGetTemplateString(string.Concat(cls_Config.Str_ConfigPath, cls_Config.Str_ClassName), ref Str_ClassTemplate, cls_Config.IsUseNameSpace);
            FnmGetTemplateString(string.Concat(cls_Config.Str_ConfigPath, cls_Config.Str_ParamName), ref Str_ParamTemplate, cls_Config.IsUseNameSpace);
        }

        static void FnmGetTemplateString(string path, ref string content, bool isUseNameSpace)
        {
            content = CacheConfigTool.FnReadStrFromFile(path);

            if (!content.Contains("&")) return;

            string replaceStr = "";

            if (isUseNameSpace)
            {
                replaceStr = "  ";
            }
            else
            {
                int first = content.IndexOf('&');
                content = content.Substring(first + 1, content.LastIndexOf('&') - first - 1);
            }

            content =  content.Replace("&", replaceStr);

            //Debug.Log(content);
        }

        [MenuItem("GameObject/Tools/CreatePrefab", false, 11)]
        public static void FnmPrefabCreate()
        {
            Str_Info = "脚本和预制体";

            FnmStartCreate((GameObject obj) =>
            {
                string nameSpace = cls_Config.IsUseNameSpace ? string.Concat(cls_Config.Str_NameSpace, ".") : "";

                FnmRemoveComponent(string.Concat(nameSpace, obj.name), obj.transform);

                Bind[] binds = obj.GetComponentsInChildren<Bind>(true);

                for (int i = 0; i < binds.Length; i++)
                {
                    if (binds[i].Enum_Type != EnumBindType.Class) continue;
                    
                    FnmRemoveComponent(string.Concat(nameSpace, obj.name), binds[i].transform);
                }

                FnmCheckDirectory(cls_Config.Str_CreatePrefabPath);

                string prefabPath = string.Concat(cls_Config.Str_CreatePrefabPath, obj.name, ".prefab");

                if (File.Exists(prefabPath)) File.Delete(prefabPath);

                GameObject prefab = PrefabUtility.SaveAsPrefabAsset(obj, prefabPath);

                string objName = obj.name;
                int siblingIndex = obj.transform.GetSiblingIndex();
                GameObject clone = PrefabUtility.InstantiatePrefab(prefab, obj.transform.parent) as GameObject;
                Selection.activeGameObject = clone;
                GameObject.DestroyImmediate(obj);
                clone.name = objName;
                clone.transform.SetSiblingIndex(siblingIndex);

                FnmSetEditorPresValue(prefab.GetInstanceID(), objName, -11, cls_Config.IsUseNameSpace ? cls_Config.Str_NameSpace : null);

            });
        }

        static void FnmRemoveComponent(string component, Transform obj)
        {
            //Debug.Log(type);
            Component pom = obj.GetComponent(component);

            if (pom)
            {
                GameObject.DestroyImmediate(pom);
            }
        }

        [MenuItem("GameObject/Tools/CreateCs", false, 11)]
        public static void FnmCsCreate()
        {
            Str_Info = "脚本";

            FnmStartCreate((GameObject obj) =>
            {
                FnmSetEditorPresValue(obj.GetInstanceID(), obj.name, -10, cls_Config.IsUseNameSpace ? cls_Config.Str_NameSpace : null);
            });
        }

        static void FnmStartCreate(Action<GameObject> dAct)
        {
            FnmReadTemplateTxt();

            GameObject obj = Selection.activeGameObject;

            if (!obj) return;

            //解除预制体关系
            FnmUnpackPrefab(obj);

            FnmGetClassBind(cls_Config.Str_CreateCsPath, obj.transform, FnmGetBindString);

            dAct(obj);

            Debug.Log("=====》创建" + Str_Info + "完成");
        }

        static void FnmUnpackPrefab(GameObject obj)
        {
            //解除预制体关系
            if (PrefabUtility.IsAnyPrefabInstanceRoot(obj))
                PrefabUtility.UnpackPrefabInstance(obj, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
        }

        #region 生成脚本

        static void FnmCreateUICode(string className, string path, string[] typeArr)
        {
            FnmCheckDirectory(path);

            Str_ClassPath = string.Concat(path, className, cls_Config.Str_ClassSuffix, ".cs");

            EditorPrefs.SetString("ClassPath", Str_ClassPath.Replace("Param", ""));

            FnmWriterClass(Str_ClassPath, FnmParamClassString(className, typeArr));

            path = string.Concat(path, className, ".cs");
            if (!File.Exists(path)) FnmWriterClass(path, FnmClassString(className));

            AssetDatabase.SaveAssets();
        }

        static void FnmWriterClass(string path, string classContent)
        {
            if (File.Exists(path)) File.Delete(path);

            CacheConfigTool.FnWriteFileFromStr(path, classContent);

            AssetDatabase.Refresh();

            AssetDatabase.SaveAssets();
        }

        static string FnmParamClassString(string className, string[] typeArr)
        {
            string str_class = Str_ParamTemplate;

            str_class = str_class.Replace("@类名", className);
            str_class = str_class.Replace("@成员", FnmGetBindParam(typeArr));
            if(cls_Config.IsUseNameSpace) str_class = str_class.Replace("@命名空间", cls_Config.Str_NameSpace);

            return str_class;
        }

        static string FnmClassString(string className)
        {
            string str_class = Str_ClassTemplate;

            str_class = str_class.Replace("@类名", className);
            str_class = str_class.Replace("#类名", cls_Config.Str_DefaultInherit);
            if (cls_Config.IsUseNameSpace) str_class = str_class.Replace("@命名空间", cls_Config.Str_NameSpace);

            return str_class;
        }

        #endregion

        #region 获取绑定物体

        static string FnmGetBindParam(string[] typeArr)
        {
            StringBuilder paramSB = new StringBuilder("\n");
            string serializeField = string.Concat("\t" + "[SerializeField]");

            for (int i = 0; i < typeArr.Length; i++)
            {
                paramSB.AppendLine(serializeField);
                paramSB.AppendLine(string.Concat("\t", typeArr[i], ";"));
            }

            return paramSB.ToString();
        }

        static void FnmGetBindString(Transform obj, BindParam bindParam ,string path)
        {
            string[] strArr = new string[bindParam.ClassBind.Count + bindParam.ElementBind.Count];
            //Debug.Log("ele : " + bindParam.ElementBind.Count + ", cla : " + bindParam.ClassBind.Count);

            for (int i = 0; i < bindParam.ElementBind.Count; i++)
            {
                //Debug.Log(bindParam.ElementBind[i].name);
                strArr[i] = string.Concat(bindParam.ElementBind[i].Com_Type.GetType().ToString(), " ", bindParam.ElementBind[i].Com_Type.name);
            }

            for (int i = 0; i < bindParam.ClassBind.Count; i++)
            {
                strArr[i + bindParam.ElementBind.Count] = string.Concat(bindParam.ClassBind[i].name, " ", bindParam.ClassBind[i].name);
            }

            FnmCreateUICode(obj.name, path, strArr);
        }
            
        //性能优化TODO
        static void FnmGetClassBind(string path, Transform obj, Action<Transform, BindParam, string> act, BindParam parentBind = null)
        {
            BindParam bindParam = new BindParam(new List<Bind>(), FnmGetBindsInChild(obj));

            for (int i = 0; i < bindParam.ElementBind.Count; i++)
            {
                //Debug.Log(bindParam.ElementBind[i].name + " : " + bindParam.ElementBind[i].Enum_Type);

                if (bindParam.ElementBind[i].Enum_Type == EnumBindType.Class && !bindParam.ClassBind.Contains(bindParam.ElementBind[i]))
                {
                    //Debug.Log(bindParam.ElementBind[i].name);
                    bindParam.ClassBind.Add(bindParam.ElementBind[i]);
                }
                else if(!bindParam.ElementBind[i].Com_Type)
                {
                    Debug.LogWarning(bindParam.ElementBind[i].name + " 元素，未绑定组件？");
                    bindParam.ElementBind.Remove(bindParam.ElementBind[i]);
                }
            }

            bindParam.ElementBind.RemoveAll((a) => bindParam.ClassBind.Contains(a));

            if (parentBind != null)
            {
                parentBind.ClassBind.RemoveAll((a) => bindParam.ClassBind.Contains(a));
                parentBind.ElementBind.RemoveAll((a) => bindParam.ElementBind.Contains(a));
            }

            for (int i = 0; i < bindParam.ClassBind.Count; i++)
            {
                FnmGetClassBind(string.Concat(path, obj.name, "/"), bindParam.ClassBind[i].transform, act, bindParam);
            }

            act(obj, bindParam, path);
        }

        static List<Bind> FnmGetBindsInChild(Transform obj)
        {
            List<Bind> binds = obj.GetComponentsInChildren<Bind>(true).ToList();

            Bind myBind = obj.GetComponent<Bind>();
            if (myBind && binds.Contains(myBind)) binds.Remove(myBind);

            return binds;
        }

        class BindParam
        {
            public List<Bind> ClassBind;
            public List<Bind> ElementBind;

            public BindParam(List<Bind> classBind, List<Bind> elementBind)
            {
                ClassBind = classBind;
                ElementBind = elementBind;
            }
        }

        #endregion

        static void FnmCheckDirectory(params string[] paths)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                if (!Directory.Exists(paths[i])) Directory.CreateDirectory(paths[i]);
            }
        }

        [DidReloadScripts]
        public static void FnmPrefabAddCode()
        {
            int autoBindCodeID = UnityEditor.EditorPrefs.GetInt("AutoBindCodeID");

            if (autoBindCodeID >= -1) return;

            //Debug.Log(autoBindCodeID);
            //Debug.Log("AutoBindCodeID : " + UnityEditor.EditorPrefs.GetInt("AutoBindCodeID"));

            string prefabName = EditorPrefs.GetString("ClassName");

            int prefabID = EditorPrefs.GetInt("PrefabID");

            FnmSetEditorPresValue(0, null, 0, EditorPrefs.GetString("BindNameSpace"));

            if (prefabName == null || prefabID == 0) return;

            GameObject prefab = EditorUtility.InstanceIDToObject(prefabID) as GameObject;

            FnmGetClassBind(null, prefab.transform, FnmStartBind);

            try
            {
                PrefabUtility.SavePrefabAsset(prefab);
            }
            catch (Exception)
            {

            }

            FnmDeleteEditorPresValue();

            Debug.Log("=====》绑定脚本完成");

            //TODO光标到保存目录
            //if (autoBindCodeID >= -10) return;

            //AssetDatabase.SaveAssets();
            //AssetDatabase.Refresh();

            //string path = AssetDatabase.GetAssetPath(prefab);
            //path = path.Substring(0, path.LastIndexOf('/'));
            //////Debug.Log(path);
            //UnityEngine.Object obj = AssetDatabase.LoadMainAssetAtPath(path);
            //Debug.Log(" : " + obj.GetInstanceID());

            //Selection.activeInstanceID = obj.GetInstanceID();
        }

        static void FnmStartBind(Transform obj, BindParam bindParam, string path)
        {
            string className = FnmGetClassTypeName(obj);

            FnmRemoveComponent(className, obj);

            string classPath = EditorPrefs.GetString("ClassPath");

            MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(classPath);

            Type type = script.GetClass();

            obj.gameObject.AddComponent(type);

            Component targetCompo = obj.GetComponent(type);

            if (targetCompo == null)
            {
                Debug.Log("绑定失败，无法获取组件1 : " + targetCompo.name);
                return;
            }

            FnmBindComponent(targetCompo, FnmGetNameField(type), bindParam.ElementBind);
            FnmBindComponent(targetCompo, FnmGetNameField(type), bindParam.ClassBind);
        }

        static string FnmGetClassTypeName(Transform obj)
        {
            string nameSpace = UnityEditor.EditorPrefs.GetString("BindNameSpace");

            return nameSpace == null ? obj.name : string.Concat(nameSpace, ".", obj.name);
        }

        static void FnmBindComponent(Component targetCompo, Dictionary<string, FieldInfo> dicNameField, List<Bind> binds)
        {
            for (int i = 0; i < binds.Count; i++)
            {
                if (dicNameField.TryGetValue(binds[i].name, out FieldInfo _))
                {
                    try
                    {
                        //Debug.Log("222 : " + bind.name);
                        dicNameField[binds[i].name].SetValue(targetCompo, binds[i].GetComponent(dicNameField[binds[i].name].FieldType));
                        dicNameField.Remove(binds[i].name);
                    }
                    catch (Exception)
                    {
                        Debug.Log(dicNameField[binds[i].name] + " : 绑定失败");
                    }
                }
            }
        }

        /// <summary>
        /// 查找脚本中所有需要绑定成员的名字、数据
        /// </summary>
        /// <param name="type">脚本类型</param>
        /// <returns></returns>
        static Dictionary<string, FieldInfo> FnmGetNameField(Type type)
        {
            
            Dictionary<string, FieldInfo> dicNameField = new Dictionary<string, FieldInfo>();

            try
            {
                foreach (FieldInfo field in type.GetRuntimeFields())
                {
                    //Debug.Log(field.Name);
                    if (!field.IsNotSerialized)
                    {
                        dicNameField[field.Name] = field;
                    }
                }
            }
            catch (Exception)
            {

            }

            return dicNameField;
        }

        static void FnmSetEditorPresValue(int prefabID, string className, int bindID, string nameSpace = null)
        {
            //Debug.Log(cls_Config.IsUseNameSpace + " : " + cls_Config.Str_NameSpace);

            EditorPrefs.SetInt("PrefabID", prefabID);

            EditorPrefs.SetString("ClassName", className);

            EditorPrefs.SetInt("AutoBindCodeID", bindID);

            EditorPrefs.SetString("BindNameSpace", nameSpace);
        }

        static void FnmDeleteEditorPresValue()
        {
            //Debug.Log(cls_Config.IsUseNameSpace + " : " + cls_Config.Str_NameSpace);

            UnityEditor.EditorPrefs.DeleteKey("PrefabID");

            UnityEditor.EditorPrefs.DeleteKey("ClassName");

            UnityEditor.EditorPrefs.DeleteKey("AutoBindCodeID");

            UnityEditor.EditorPrefs.DeleteKey("BindNameSpace");
        }


        ///// <summary>
        ///// 监听资源即将被保存
        ///// </summary>
        ///// <param name="paths"></param>
        ///// <returns></returns>
        //public static string[] OnWillSaveAssets(string[] paths)
        //{
        //    Debug.Log("OnWillSaveAssets");
        //    if (paths != null)
        //    {
        //        Debug.LogFormat("path:{0}", string.Join(",", paths));
        //    }
        //    return paths;
        //}
    }
}
