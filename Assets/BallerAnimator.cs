using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

public class BallerAnimator : MonoBehaviour
{
    [System.Serializable]
    public class TransformRotation
    {
        public Transform targetTransform;
        public Vector3 rotationEuler; // Rotation in Euler angles
    }

    public enum AnimationMethod
    {
        PremadeAnimation,
        Manual
    }

    public float duration = 1.0f;

    public void PlayAnimation(AnimationMethod animationMethod, PlayerAnimation animationData)
    {
        if (animationMethod == AnimationMethod.PremadeAnimation && animationData == null)
        {
            Debug.LogError("Premade Animation is not assigned!");
            return;
        }
        if (animationMethod == AnimationMethod.PremadeAnimation)
        {
            animationData.OnEnable(); // Ensure the dictionary is initialized
        }
            UsePremadeAnimation(animationData);
    }

    public void PlayAnimation(AnimationMethod animationMethod, TransformRotation[] transformsToRotate)
    {
        UseManualAnimation(transformsToRotate);
    }

    private void UsePremadeAnimation(PlayerAnimation animationData)
    {
        foreach (KeyValuePair<string, BodyPartRotation> kvp in animationData.bodyPartRotations)
        {
            string bodyPartName = kvp.Key; // Get the name of the body part (same as in the Scene)
            BodyPartRotation bodyPartRotation = kvp.Value; // Get the rotation for the body part
            Transform targetTransform = GetTransformByName(bodyPartName); // Get the transform of the body part
            if (targetTransform != null)
            {
                Quaternion endRotation = Quaternion.Euler(bodyPartRotation.eulerAngles);
                Quaternion globalEndRotation = endRotation;
                if (targetTransform.parent != null)
                {
                    globalEndRotation = targetTransform.parent.rotation * endRotation;
                }
                StartCoroutine(Rotate(targetTransform, globalEndRotation, duration));
            }
            else
            {
                Debug.LogWarning("Transform not found for body part: " + bodyPartName);
            }
        }
    }

    private void UseManualAnimation(TransformRotation[] transformsToRotate)
    {
        foreach (TransformRotation transformRotation in transformsToRotate)
        {
            Quaternion endRotation = Quaternion.Euler(transformRotation.rotationEuler);
            StartCoroutine(Rotate(transformRotation.targetTransform, endRotation, duration));
        }
    }

    private IEnumerator Rotate(Transform targetTransform, Quaternion endRotation, float duration)
    {
        Quaternion startRotation = targetTransform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            targetTransform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetTransform.rotation = endRotation;
    }

    private Transform GetTransformByName(string bodyPartName)
    {
        GameObject obj = GameObject.Find(bodyPartName);
        if (obj != null)
        {
            return obj.transform; // Return the transform component of the GameObject
        }
        else
        {
            Debug.LogWarning("GameObject not found with name: " + bodyPartName);
            return null;
        }
    }

}
