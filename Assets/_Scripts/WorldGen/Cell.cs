using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    //corners
    public List<Node> nodes;

    //centroid
    public Node center;

    //2 Node defining sides
    public List<Node> SideA = new List<Node>();
    public List<Node> SideB = new();
    public List<Node> SideC = new();
    public List<Node> SideD = new();

    public List<List<Node>> Sides = new List<List<Node>>();

    //Defining type
    public List<ScriptableTypes> potentialTileTypes;
    public ScriptableTypes collapsedType;

    //neighboring cells
    public Cell NeighborA;
    public Cell NeighborB;
    public Cell NeighborC;
    public Cell NeighborD;

    public List<Cell> neighbors = new List<Cell>();
    public bool borderCell = false;
}
