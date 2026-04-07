using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIButtonAction))]
public class UIButtonActionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UIButtonAction button = (UIButtonAction)target;

        // Action Type
        button.actionType = (ButtonActionType)
            EditorGUILayout.EnumPopup("Action Type", button.actionType);

        EditorGUILayout.Space();

        // Show fields based on action
        switch (button.actionType)
        {
            case ButtonActionType.LoadScene:

                EditorGUILayout.LabelField("Scene Settings", EditorStyles.boldLabel);

                button.sceneName =
                    EditorGUILayout.TextField("Scene Name", button.sceneName);

                button.transitionType =
                    (SceneTransitionType)EditorGUILayout.EnumPopup(
                        "Transition",
                        button.transitionType);

                break;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(button);
        }
    }
}