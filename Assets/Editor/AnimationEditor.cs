
/*
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Shooting))]
public class AnimationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Shooting shootingAnimator = (Shooting)target;

        // Draw the default enum field
        shootingAnimator.animationMethod = (BallerAnimator.AnimationMethod)EditorGUILayout.EnumPopup("Rotation Source", shootingAnimator.animationMethod);

        // Show fields based on the rotation source
        if (shootingAnimator.animationMethod == BallerAnimator.AnimationMethod.PremadeAnimation)
        {
            shootingAnimator.animationData = (PlayerAnimation)EditorGUILayout.ObjectField("Player Animation", shootingAnimator.animationData, typeof(PlayerAnimation), false);
        }
        else if (shootingAnimator.animationMethod == BallerAnimator.AnimationMethod.Manual)
        {
            SerializedProperty transformsToRotate = serializedObject.FindProperty("transformsToRotate");
            EditorGUILayout.PropertyField(transformsToRotate, true);
        }

        // Apply changes to the serializedProperty - always do this at the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }
}
*/