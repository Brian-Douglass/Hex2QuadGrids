using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelObject", menuName ="Create Level Object")]
public class ScriptablePoints : ScriptableObject
{
    public List<Vector3> nodes = new List<Vector3>();
}
