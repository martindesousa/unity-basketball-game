using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BodyPartRotation
{
    public string partName; //name of the body part
    public Vector3 eulerAngles; // rotation of the body part

    public BodyPartRotation(string name, float x, float y, float z)
    {
        partName = name;
        eulerAngles = new Vector3(x, y, z);
    }
}
