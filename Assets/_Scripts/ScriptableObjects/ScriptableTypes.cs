using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Create Scriptable Type",fileName ="Type")]
public class ScriptableTypes : ScriptableObject
{
    public GameObject tile;
    public List<ScriptableTypes> legalConnectionsA;
    public List<ScriptableTypes> legalConnectionsB;
    public List<ScriptableTypes> legalConnectionsC;
    public List<ScriptableTypes> legalConnectionsD;

    public List<ScriptableTypes> GetLegalConnectionsA() { return legalConnectionsA; }
    public List<ScriptableTypes> GetLegalConnectionsB() { return legalConnectionsB; }
    public List<ScriptableTypes> GetLegalConnectionsC() { return legalConnectionsC; }
    public List<ScriptableTypes> GetLegalConnectionsD() { return legalConnectionsD; }
}
