using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
public class CSVToAsset
{

    private static string coordFile = "/Editor/coords.csv";
    [MenuItem("Utilities/Generate Levels")]
    public static void GenerateLevels() {
        string[] allLines = File.ReadAllLines(Application.dataPath + coordFile);
        List<Vector2> coordinates = new List<Vector2>();
        ScriptablePoints scriptablePoints = ScriptableObject.CreateInstance<ScriptablePoints>();
        foreach (string line in allLines) {
            string[] splitData = line.Split(',');
            float lineX = float.Parse(splitData[0]);
            float lineY = float.Parse(splitData[1]);
            scriptablePoints.nodes.Add(new Vector2(lineX *5,lineY*5));
        }

        AssetDatabase.CreateAsset(scriptablePoints, $"Assets/_scripts/ScriptableObjects/testMesh.asset");
        AssetDatabase.SaveAssets();
    } 
}
