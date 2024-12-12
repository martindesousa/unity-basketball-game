using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animation", menuName = "ScriptableObjects/Animation", order = 1)]
public class PlayerAnimation : ScriptableObject
{
    public string animationName;
    public List<BodyPartRotation> bodyPartRotationsList = new List<BodyPartRotation>();
    [System.NonSerialized]
    public Dictionary<string, BodyPartRotation> bodyPartRotations;

    public void OnEnable()
    {
        // Convert list to dictionary
        bodyPartRotations = new Dictionary<string, BodyPartRotation>();
        foreach (var partRotation in bodyPartRotationsList)
        {
            bodyPartRotations[partRotation.partName] = partRotation;
        }
    }
}
