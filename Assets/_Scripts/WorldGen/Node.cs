using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int nodeID = new();
    public Vector3 Coordinates;
    public float NodeX;
    public float NodeY;
    public float NodeZ;
    public bool borderNode;
    public List<Node> partners = new List<Node>();
}
