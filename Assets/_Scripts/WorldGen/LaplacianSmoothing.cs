using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaplacianSmoothing : MonoBehaviour
{

    public void Smoothing() {
        Quadding quadding = GetComponent<Quadding>();
        Debug.Log("here");
            foreach (Cell cell in quadding.cells) {
                foreach (Node node in cell.nodes) {
                    if (cell.borderCell == false) {
                        Vector3 newCoords = (node.partners[0].Coordinates + node.partners[1].Coordinates + node.partners[2].Coordinates + node.partners[3].Coordinates) * .25f;
                        Debug.Log($"new x: {newCoords.x} new y: {newCoords.y} new z: {newCoords.z}");
                        Debug.Log($"old x: {node.NodeX} old y: {node.NodeY} old z: {node.NodeZ}");
                        node.Coordinates = newCoords;

                    }
                }
            }
        

    }

    public void AssembleNodeConnections() {
        Quadding quadding = GetComponent<Quadding>(); 
        foreach (Cell cell in quadding.cells) {
            if(cell.borderCell == false) {
                for (int i = 0; i < cell.nodes.Count; i++) {
                    switch (i) {
                        case 0:
                            cell.nodes[0].partners.Add(cell.nodes[1]);
                            cell.nodes[0].partners.Add(cell.nodes[3]);
                            cell.nodes[0].partners.Add(cell.NeighborB.nodes[0]);
                            cell.nodes[0].partners.Add(cell.NeighborD.nodes[0]);
                            break;
                        case 1:
                            cell.nodes[1].partners.Add(cell.NeighborC.nodes[1]);
                            cell.nodes[1].partners.Add(cell.nodes[2]);
                            cell.nodes[1].partners.Add(cell.nodes[0]);
                            cell.nodes[1].partners.Add(cell.NeighborD.nodes[1]);
                            break;
                        case 2:
                            cell.nodes[2].partners.Add(cell.NeighborC.nodes[2]);
                            cell.nodes[2].partners.Add(cell.NeighborA.nodes[2]);
                            cell.nodes[2].partners.Add(cell.nodes[3]);
                            cell.nodes[2].partners.Add(cell.nodes[1]);
                            break;
                        case 3:
                            cell.nodes[3].partners.Add(cell.nodes[2]);
                            cell.nodes[3].partners.Add(cell.NeighborA.nodes[3]);
                            cell.nodes[3].partners.Add(cell.NeighborB.nodes[3]);
                            cell.nodes[3].partners.Add(cell.nodes[0]);
                            break;
                    }
                }
            }
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.black;
        Quadding quadding = GetComponent<Quadding>();
        foreach (Cell cell in quadding.cells) {
            Gizmos.DrawLine(cell.nodes[0].Coordinates, cell.nodes[1].Coordinates);
            Gizmos.DrawLine(cell.nodes[1].Coordinates, cell.nodes[2].Coordinates);
            Gizmos.DrawLine(cell.nodes[2].Coordinates, cell.nodes[3].Coordinates);
            Gizmos.DrawLine(cell.nodes[3].Coordinates, cell.nodes[0].Coordinates);
        }
    }
}
