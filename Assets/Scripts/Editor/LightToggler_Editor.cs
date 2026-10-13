using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LightToggler), editorForChildClasses:true)]
public class LightToggler_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LightToggler lightToggler = (LightToggler)target;
        if (GUILayout.Button(text: "Toggle"))
        {
            lightToggler.Toggle_Rpc();
        }
    }
}
