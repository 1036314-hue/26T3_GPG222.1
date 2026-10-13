using JetBrains.Annotations;
using Unity.Services.Matchmaker.Models;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LightToggler), editorForChildClasses:true)]
public class LightToggler_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LightToggler lightToggler = (LightToggler)target;
        if (GUILayout.Button(text: "On"))
        {
            lightToggler.Toggle_Rpc(state: true);
        }
        if (GUILayout.Button(text: "Off"))
        {
            lightToggler.Toggle_Rpc(state: false);
        }
    }
         
    
}
