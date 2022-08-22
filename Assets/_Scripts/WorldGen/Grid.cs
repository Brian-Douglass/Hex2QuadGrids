using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int size;
    public List<Hex> hexes = new List<Hex>();
    public int width = 10;
    public int height = 10;
    public void HexesFromGrid() {
        float w = Mathf.Sqrt(3) * size;
        float h = size * 2;
        for(int x = 0; x < width; x++) {
            for (int z = 0; z <= height; z++) {
                if (z % 2 == 0) {
                    hexes.Add(BuildHex(new Vector3(x * w,z * h*.75f, 0)));
                }
                else {
                    hexes.Add(BuildHex(new Vector3((x * w) + w/2, z*h *.75f, 0)));
                }
            }
        }
    }

    Hex BuildHex(Vector3 center) {
        Hex hex = new Hex();
        hex.center = new Vector3(center.x, 0, center.y);
        for (int i = 0; i < 6; i++) {
            hex.corners.Add(HexCornerOffset(i, center));
        }
        for (int j = 0; j < 6; j++) {
            List<Vector3> tris = new List<Vector3>();
            tris.Add(hex.center);
            tris.Add(hex.corners[(j + 5) % 6]);
            tris.Add(hex.corners[j]);
            hex.triangles.Add(tris);
        }
        return hex;
    }

    Vector3 HexCornerOffset(int corner, Vector3 center) {
        float angleDeg = 60 * corner - 30;
        float AngleRad = Mathf.PI / 180 * angleDeg;
        return new Vector3(center.x + size * Mathf.Cos(AngleRad), center.z, center.y + size * Mathf.Sin(AngleRad));
    }
}

public class Hex {
    public Vector3 center;
    public List<Vector3> corners = new List<Vector3>();
    public List<List<Vector3>> triangles = new List<List<Vector3>>();
}
