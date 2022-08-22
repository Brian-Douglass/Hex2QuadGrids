using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quadding : MonoBehaviour
{
    public List<Cell> cells = new List<Cell>();
    public CellUtilities cellUtilities = new();
    public void SubdivideTris(List<Hex> hexes) {

        foreach(Hex hex in hexes) {
            for (int i = 0; i < hex.triangles.Count; i++) {
                if (Random.Range(0, 2) == 1 && i + 1 < hex.triangles.Count) {
                    SubdivideQuad(hex.triangles[i], hex.triangles[i + 1]);
                    i++;
                }
                else{
                    SubdivideTriangles(hex.triangles[i]);
                }
            }
        }
    }

    public void SubdivideQuad(List<Vector3> triA, List<Vector3> triB) {
        List<Vector3> corners = new List<Vector3>();
        corners.AddRange(triA);
        corners.AddRange(triB);
        corners = corners.Distinct().ToList();


        //get the midpoints of the sides of the quad
        Vector3 sideA = (corners[0] + corners[1]) * .5f;
        Vector3 sideB = (corners[1] + corners[2]) * .5f;
        Vector3 sideC = (corners[2] + corners[3]) * .5f;
        Vector3 sideD = (corners[3] + corners[0]) * .5f;

        //get intersection of the lines, aka the quad's centroid
        Vector3 Compute5(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4) => Mathf.Abs((v1.z - v2.z) / (v1.x - v2.x) - (v3.z - v4.z) / (v3.x - v4.x)) < .0001f ? new Vector3(0, 0, 0) : new Vector3(Mathf.Abs(v1.x - v2.x) < .0001f ? v1.x : Mathf.Abs(v3.x - v4.x) < .01f ? v3.x : -(v1.z - (v1.z - v2.z) / (v1.x - v2.x) * v1.x - (v3.z - (v3.z - v4.z) / (v3.x - v4.x) * v3.x)) / ((v1.z - v2.z) / (v1.x - v2.x) - (v3.z - v4.z) / (v3.x - v4.x)), 0, Mathf.Abs(v1.x - v2.x) < .0001f ? (v3.z - v4.z) / (v3.x - v4.x) * (Mathf.Abs(v1.x - v2.x) < .0001f ? v1.x : Mathf.Abs(v3.x - v4.x) < .0001f ? v3.x : -(v1.z - (v1.z - v2.z) / (v1.x - v2.x) * v1.x - (v3.z - (v3.z - v4.z) / (v3.x - v4.x) * v3.x)) / ((v1.z - v2.z) / (v1.x - v2.x) - (v3.z - v4.z) / (v3.x - v4.x))) + (v3.z - (v3.z - v4.z) / (v3.x - v4.x) * v3.x) : (v1.z - v2.z) / (v1.x - v2.x) * (Mathf.Abs(v1.x - v2.x) < .0001f ? v1.x : Mathf.Abs(v3.x - v4.x) < .0001f ? v3.x : -(v1.z - (v1.z - v2.z) / (v1.x - v2.x) * v1.x - (v3.z - (v3.z - v4.z) / (v3.x - v4.x) * v3.x)) / ((v1.z - v2.z) / (v1.x - v2.x) - (v3.z - v4.z) / (v3.x - v4.x))) + (v1.z - (v1.z - v2.z) / (v1.x - v2.x) * v1.x));
        Vector3 centroid = Compute5(corners[0], corners[2], corners[1], corners[3]);

        //build subquads 

        List<Node> subQuadA = new List<Node>();
        subQuadA.Add(cellUtilities.GenerateNodeFromPoint(corners[0]));
        subQuadA.Add(cellUtilities.GenerateNodeFromPoint(sideA));
        subQuadA.Add(cellUtilities.GenerateNodeFromPoint(centroid));
        subQuadA.Add(cellUtilities.GenerateNodeFromPoint(sideD));

        List<Node> subQuadB = new List<Node>();
        subQuadB.Add(cellUtilities.GenerateNodeFromPoint(sideA));
        subQuadB.Add(cellUtilities.GenerateNodeFromPoint(corners[1]));
        subQuadB.Add(cellUtilities.GenerateNodeFromPoint(sideB));
        subQuadB.Add(cellUtilities.GenerateNodeFromPoint(centroid));

        List<Node> subQuadC = new List<Node>();
        subQuadC.Add(cellUtilities.GenerateNodeFromPoint(centroid));
        subQuadC.Add(cellUtilities.GenerateNodeFromPoint(sideB));
        subQuadC.Add(cellUtilities.GenerateNodeFromPoint(corners[2]));
        subQuadC.Add(cellUtilities.GenerateNodeFromPoint(sideC));

        List<Node> subQuadD = new List<Node>();
        subQuadD.Add(cellUtilities.GenerateNodeFromPoint(sideD));
        subQuadD.Add(cellUtilities.GenerateNodeFromPoint(centroid));
        subQuadD.Add(cellUtilities.GenerateNodeFromPoint(sideC));
        subQuadD.Add(cellUtilities.GenerateNodeFromPoint(corners[3]));


        //cells for subquads
        Cell cellA = new Cell();
        Cell cellB = new Cell();
        Cell cellC = new Cell();
        Cell cellD = new Cell();
        cells.Add(cellA);
        cells.Add(cellB);
        cells.Add(cellC);
        cells.Add(cellD);
        //fill cells with data
        cellUtilities.GenerateCellFromQuad(subQuadA, cellA);
        cellUtilities.GenerateCellFromQuad(subQuadB, cellB);
        cellUtilities.GenerateCellFromQuad(subQuadC, cellC);
        cellUtilities.GenerateCellFromQuad(subQuadD, cellD);

        //assign cell neighbors
        cellA.NeighborA = cellB;
        cellA.NeighborB = cellD;

        cellB.NeighborB = cellC;
        cellB.NeighborC = cellA;

        cellC.NeighborC = cellD;
        cellC.NeighborD = cellB;

        cellD.NeighborA = cellC;
        cellD.NeighborD = cellA;
    }

    Vector3 ComputeTriangleCentroid(Vector3 vert1, Vector3 vert2, Vector3 vert3) {
        Vector3 centroid = (vert1 + vert2 + vert3) * .33333f;
        return centroid;
    }
    
    public Vector3 ComputeQuadCentroid(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4) {
        Vector3 sideA = (v1 + v2) * .5f;
        Vector3 sideB = (v3 + v4) * .5f;
        Vector3 sideC = (v2 + v3) * .5f;
        Vector3 sideD = (v3 + v4) * .5f;

        return new Vector3(((sideC.x + sideD.x) * .5f), 0, ((sideA.z + sideB.z) * .5f));
    }

    public void SubdivideTriangles(List<Vector3> vertices) {

        Vector3 centroid = ComputeTriangleCentroid(vertices[0], vertices[1], vertices[2]);
        Vector3 SideA = (vertices[0] + vertices[1]) * .5f;
        Vector3 SideB = (vertices[1] + vertices[2]) * .5f;
        Vector3 SideC = (vertices[2] + vertices[0]) * .5f;

        List<Node> quad1 = new();
        List<Node> quad2 = new();
        List<Node> quad3 = new();

        Cell cellA = new Cell();
        Cell cellB = new Cell();
        Cell cellC = new Cell();
        cells.Add(cellA);
        cells.Add(cellB);
        cells.Add(cellC);

        //build the first quad from the triangle and convert the points into nodes
        quad1.Add(cellUtilities.GenerateNodeFromPoint(SideC));
        quad1.Add(cellUtilities.GenerateNodeFromPoint(vertices[0]));
        quad1.Add(cellUtilities.GenerateNodeFromPoint(SideA));
        quad1.Add(cellUtilities.GenerateNodeFromPoint(centroid));

        //build the first cell from the first quad and assign its known neighbors
        cellUtilities.GenerateCellFromQuad(quad1, cellA);
        cellA.NeighborA = cellB;
        cellA.NeighborB = cellC;

        //build the second quad from the triangle and convert the points into nodes
        quad2.Add(cellUtilities.GenerateNodeFromPoint(centroid));
        quad2.Add(cellUtilities.GenerateNodeFromPoint(SideA));
        quad2.Add(cellUtilities.GenerateNodeFromPoint(vertices[1]));
        quad2.Add(cellUtilities.GenerateNodeFromPoint(SideB));
        

        //build the second cell from the second quad and assign its known neighbors
        cellUtilities.GenerateCellFromQuad(quad2, cellB);
        cellB.NeighborB = cellC;
        cellB.NeighborC = cellA;

        //build the final quad from the triangle and convert the points to nodes
        quad3.Add(cellUtilities.GenerateNodeFromPoint(vertices[2]));
        quad3.Add(cellUtilities.GenerateNodeFromPoint(SideC));
        quad3.Add(cellUtilities.GenerateNodeFromPoint(centroid));
        quad3.Add(cellUtilities.GenerateNodeFromPoint(SideB));


        //build the final cell from quad3 and assign its known neighbors
        cellUtilities.GenerateCellFromQuad(quad3, cellC);
        cellC.NeighborC = cellA;
        cellC.NeighborD = cellB;
    }

}
