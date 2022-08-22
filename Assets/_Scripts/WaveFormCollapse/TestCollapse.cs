using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestCollapse : MonoBehaviour
{
    private WorldManagement world;
    private Quadding quadding;
    private List<Cell> collapsedCells = new List<Cell>();

    private void Start() {
        world = GetComponent<WorldManagement>();
        quadding = GetComponent<Quadding>();
    }
    public void FillCellsInitialState(List<Cell> cells) {
        foreach (Cell cell in cells) {
            cell.potentialTileTypes = world.allTypes;
        }
    }

    public void CollapseCell() {
        if (collapsedCells.Count == 0) {
            Cell cell = quadding.cells[Random.Range(0, quadding.cells.Count)];
            cell.collapsedType = world.allTypes[Random.Range(0, world.allTypes.Count)];
            collapsedCells.Add(cell);
            PropagateChange(cell);
        }
        else {
            List<Cell> remainingCells = quadding.cells.Except(collapsedCells).ToList();
            if (remainingCells.Count == 0) {
                Debug.Log($"Collapsed Cell Count: {collapsedCells.Count}");
                Debug.Log($"Grid Cells Count: {quadding.cells.Count}");
                return;
            }
            Cell cell = remainingCells[Random.Range(0, remainingCells.Count)];
            List<ScriptableTypes> legalTypes = new List<ScriptableTypes>();
            for (int i = 0; i < cell.neighbors.Count; i++) {
                if (cell.neighbors[i].potentialTileTypes != world.allTypes) {
                    legalTypes.AddRange(cell.neighbors[i].potentialTileTypes);
                }
            }
            legalTypes.Distinct();
            if (legalTypes.Count > 0) {
                cell.collapsedType = legalTypes[Random.Range(0, legalTypes.Count)];
            }
            else {
                cell.collapsedType = world.allTypes[Random.Range(0, world.allTypes.Count)];
            }
            collapsedCells.Add(cell);
            PropagateChange(cell);
        }
    }
    
    public void PropagateChange(Cell cell) {

        if (cell.NeighborA != null && cell.NeighborA.potentialTileTypes.Contains(cell.collapsedType) && !collapsedCells.Contains(cell.NeighborA)) {
            cell.NeighborA.potentialTileTypes = cell.collapsedType.GetLegalConnectionsA();
        }
        if (cell.NeighborB != null && cell.NeighborB.potentialTileTypes.Contains(cell.collapsedType) && !collapsedCells.Contains(cell.NeighborB)) {
            cell.NeighborB.potentialTileTypes = cell.collapsedType.GetLegalConnectionsB();
        }
        if (cell.NeighborC != null && cell.NeighborC.potentialTileTypes.Contains(cell.collapsedType) && !collapsedCells.Contains(cell.NeighborC)) {
            cell.NeighborC.potentialTileTypes = cell.collapsedType.GetLegalConnectionsC();
        }
        if(cell.NeighborD != null && cell.NeighborD.potentialTileTypes.Contains(cell.collapsedType) && !collapsedCells.Contains(cell.NeighborD)) {
            cell.NeighborD.potentialTileTypes = cell.collapsedType.GetLegalConnectionsD();        
        }
        ContinuePropagation(cell);
    }

    void ContinuePropagation(Cell cell) {

        List<Cell> neighborCells = cell.neighbors;
        for(int i = 0; i < cell.neighbors.Count; i++) {
            if (collapsedCells.Contains(neighborCells[i])) {
                neighborCells.Remove(neighborCells[i]);
            }
        }
        if (neighborCells.Count == 0) {
            CollapseCell();
            return;
        }
        else {
            Cell nextCell = cell.neighbors[Random.Range(0, neighborCells.Count)];
            nextCell.collapsedType = nextCell.potentialTileTypes[Random.Range(0, nextCell.potentialTileTypes.Count)];
            collapsedCells.Add(nextCell);
            PropagateChange(nextCell);
        }
    }
}
