using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManagement : MonoBehaviour
{
    private Grid grid;
    private Quadding quadding;
    private TestCollapse collapse;
    private CellUtilities cellUtilities = new CellUtilities();
    private LaplacianSmoothing laplacian;

    public GameObject sphere;


    public List<ScriptableTypes> allTypes = new List<ScriptableTypes>();

    private void Start() {
        grid = GetComponent<Grid>();
        quadding = GetComponent<Quadding>();
        collapse = GetComponent<TestCollapse>();
        laplacian = GetComponent<LaplacianSmoothing>();
        RollMap();

    }

    void VerifyCells() {
        foreach(Cell cell in quadding.cells) {
            if(cell.potentialTileTypes == null || cell.collapsedType == null) {
                RollMap();
            }
        }
    }

    void RollMap() {
        if (grid.hexes != null) {
            grid.hexes.Clear();
            quadding.cells.Clear();
        }

        //build the hexes
        grid.HexesFromGrid();
        //split them into quads
        quadding.SubdivideTris(grid.hexes);
        //get the cells' neighbors
        cellUtilities.GetCellNeighbors(quadding.cells);
        //Set the nodes' connections
        laplacian.AssembleNodeConnections();
        //smooth the grid
        laplacian.Smoothing();
        //set up the collapse
        collapse.FillCellsInitialState(quadding.cells);
        //collapse the cells
        collapse.CollapseCell();
    }
}
