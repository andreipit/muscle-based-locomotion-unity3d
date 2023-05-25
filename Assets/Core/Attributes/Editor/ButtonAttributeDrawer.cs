using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace MuscleSystemV01
{
    [CustomPropertyDrawer(typeof(BttnAttribute))]
    public class BttnAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _Rect, SerializedProperty _Property, GUIContent _Label)
        {
            if (GUI.Button(_Rect, _Label))
            {
                if (!(attribute is BttnAttribute)) 
                    return;
                object targetObject = _Property.serializedObject.targetObject;
                MethodInfo[] infos = targetObject.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (MethodInfo info in infos)
                {
                    if (info.Name != (attribute as BttnAttribute).FunctionTitle)
                        continue;
                    ParameterInfo[] parameters = info.GetParameters();
                    
                    if (parameters.Length == 1 && parameters[0].ParameterType == typeof(bool))
                        info.Invoke(targetObject, new object[] { _Property.boolValue });
                    else if (parameters.Length == 0)
                        info.Invoke(targetObject, null);
                }
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                UnityEditor.EditorUtility.SetDirty(_Property.serializedObject.targetObject);
                UnityEditor.AssetDatabase.SaveAssets();
            }
        }
    }
}
