using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CellUtilities
{
    public int nodeNumbers = 0;
    public List<Node> nodes = new List<Node>();
    private List<Vector3> nodeCoords = new List<Vector3>();
    public void GetCellNeighbors(List<Cell> cells) {
            foreach (Cell cell in cells) {
                if(cell.NeighborA == null) {
                    for (int i = 0; i < cells.Count; i++) {
                        for (int j = 0; j < cells[i].Sides.Count; j++) {
                            if (CheckEquivalence(cell.SideA[0].Coordinates, cell.SideA[1].Coordinates, cells[i].Sides[j][0].Coordinates, cells[i].Sides[j][1].Coordinates)) {
                                cell.NeighborA = cells[i];
                                cell.neighbors.Add(cell.NeighborA);
                            }
                            else {
                                cell.NeighborA = null;
                                cell.borderCell = true;
                            }
                        }
                    }
                }
                if (cell.NeighborB == null) {
                    for (int i = 0; i < cells.Count; i++) {
                        for (int j = 0; j < cells[i].Sides.Count; j++) {
                            if (CheckEquivalence(cell.SideB[0].Coordinates, cell.SideB[1].Coordinates, cells[i].Sides[j][0].Coordinates, cells[i].Sides[j][1].Coordinates)) {
                                cell.NeighborB = cells[i];
                                cell.neighbors.Add(cell.NeighborB);
                            }
                            else {
                                cell.NeighborB = null;
                                cell.borderCell = true;
                            }
                        }
                    }
                }
                if(cell. NeighborC == null) {
                    for (int i = 0; i < cells.Count; i++) {
                        for (int j = 0; j < cells[i].Sides.Count; j++) {
                            if (CheckEquivalence(cell.SideC[0].Coordinates, cell.SideC[1].Coordinates, cells[i].Sides[j][0].Coordinates, cells[i].Sides[j][1].Coordinates)) {
                                cell.NeighborC = cells[i];
                                cell.neighbors.Add(cell.NeighborC);
                            }
                            else {
                                cell.NeighborC = null;
                                cell.borderCell = true;
                            }
                        }
                    }
                }
                if(cell.NeighborD == null) {
                    for (int i = 0; i < cells.Count; i++) {
                        for (int j = 0; j < cells[i].Sides.Count; j++) {
                            if (CheckEquivalence(cell.SideD[0].Coordinates, cell.SideD[1].Coordinates, cells[i].Sides[j][0].Coordinates, cells[i].Sides[j][1].Coordinates)) {
                                cell.NeighborD = cells[i];
                                cell.neighbors.Add(cell.NeighborD);
                            }
                            else{ 
                                cell.NeighborD = null;
                                cell.borderCell = true;
                            }
                        }
                    }
                }
            }
        }

    public bool CheckEquivalence(Vector3 a, Vector3 b, Vector3 x, Vector3 y) {
        if ((a, b) == (x, y) || (b, a) == (x, y)) {
            return true;
        }
        return false;
    }

    public List<Node> GetOppositeSide(Cell cella, Cell cellb) {
        for(int i = 0; i < cella.Sides.Count; i++) {
            if (cellb.Sides.Contains(cella.Sides[i])){
                return cella.Sides[i];
            }
        }
        return null;
    }

    public Node GenerateNodeFromPoint(Vector3 point) {

        if (nodeCoords.Contains(point)) {
            return nodes.Find(x => x.Coordinates == point);
        }
        else {
            Node newNode = new();
            newNode.nodeID = nodeNumbers;
            nodeNumbers++;

            newNode.NodeX = point.x;
            newNode.NodeY = point.y;
            newNode.NodeZ = point.z;
            newNode.Coordinates = point;
            nodes.Add(newNode);
            nodeCoords.Add(point);
            return newNode;
        }
    }

    public void GenerateCellFromQuad(List<Node> nodes, Cell cell) {
        cell.nodes = nodes;
        Vector3 Compute1(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4) => Mathf.Abs((v1.z - v2.z) / (v1.x - v2.x) - (v3.z - v4.z) / (v3.x - v4.x)) < .0001f ? new Vector3(0, 0, 0) : new Vector3(Mathf.Abs(v1.x - v2.x) < .0001f ? v1.x : Mathf.Abs(v3.x - v4.x) < .01f ? v3.x : -(v1.z - (v1.z - v2.z) / (v1.x - v2.x) * v1.x - (v3.z - (v3.z - v4.z) / (v3.x - v4.x) * v3.x)) / ((v1.z - v2.z) / (v1.x - v2.x) - (v3.z - v4.z) / (v3.x - v4.x)), 0, Mathf.Abs(v1.x - v2.x) < .0001f ? (v3.z - v4.z) / (v3.x - v4.x) * (Mathf.Abs(v1.x - v2.x) < .0001f ? v1.x : Mathf.Abs(v3.x - v4.x) < .0001f ? v3.x : -(v1.z - (v1.z - v2.z) / (v1.x - v2.x) * v1.x - (v3.z - (v3.z - v4.z) / (v3.x - v4.x) * v3.x)) / ((v1.z - v2.z) / (v1.x - v2.x) - (v3.z - v4.z) / (v3.x - v4.x))) + (v3.z - (v3.z - v4.z) / (v3.x - v4.x) * v3.x) : (v1.z - v2.z) / (v1.x - v2.x) * (Mathf.Abs(v1.x - v2.x) < .0001f ? v1.x : Mathf.Abs(v3.x - v4.x) < .0001f ? v3.x : -(v1.z - (v1.z - v2.z) / (v1.x - v2.x) * v1.x - (v3.z - (v3.z - v4.z) / (v3.x - v4.x) * v3.x)) / ((v1.z - v2.z) / (v1.x - v2.x) - (v3.z - v4.z) / (v3.x - v4.x))) + (v1.z - (v1.z - v2.z) / (v1.x - v2.x) * v1.x));
        cell.center = GenerateNodeFromPoint(Compute1(nodes[0].Coordinates, nodes[2].Coordinates, nodes[1].Coordinates, nodes[3].Coordinates));
        
        cell.SideA.Add(nodes[0]);
        cell.SideA.Add(nodes[1]);
        cell.SideB.Add(nodes[1]);
        cell.SideB.Add(nodes[2]);
        cell.SideC.Add(nodes[2]);
        cell.SideC.Add(nodes[3]);
        cell.SideD.Add(nodes[3]);
        cell.SideD.Add(nodes[0]);

        cell.Sides.Add(cell.SideA);
        cell.Sides.Add(cell.SideB);
        cell.Sides.Add(cell.SideC);
        cell.Sides.Add(cell.SideD);
    }

}
