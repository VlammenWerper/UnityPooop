using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour
{
    public float scalex;
    public float scaley;
    public float scalez;

    private int Count;

    Vector3[] CubeCorners;
    private Vector3[] vertices;
    private int[] traingles;
    private Mesh mesh;
    private MeshCollider meshcol;
    void Start()
    {
        Count = 0;
        mesh = GetComponent<MeshFilter>().mesh;
        meshcol = GetComponent<MeshCollider>();
        CubeCorners = new Vector3[8];

        //3vertices * 2traingles* 6sides
        int MeshLenght = 3 * 2 * 6;
        vertices = new Vector3[MeshLenght];
        traingles = new int[MeshLenght];
        for(int i = 0; i < 8; i++)
        {
            CubeCorners[i] = new Vector3(CubeInfo[i][0]*(scalex/2), CubeInfo[i][1]*(scaley / 2), CubeInfo[i][2]*(scalez / 2));
        }
        for (int t = 0; t < 12; t++)
        {
            vertices[Count] = CubeCorners[TraingleInfo[t][0]];
            traingles[Count] = Count;
            Count++;

            vertices[Count] = CubeCorners[TraingleInfo[t][1]];
            traingles[Count] = Count;
            Count++;

            vertices[Count] = CubeCorners[TraingleInfo[t][2]];
            traingles[Count] = Count;
            Count++;

        }
        mesh.vertices = vertices;
        mesh.triangles = traingles;
        mesh.RecalculateNormals();
        meshcol.sharedMesh = mesh;
    }
    private static readonly int[][] CubeInfo =
    {
        new[] {1, -1, 1},
        new[] {-1,-1, 1},
        new[] {-1,-1,-1},
        new[] {1,-1, -1},

        new[] { 1, 1, 1},
        new[] {-1, 1, 1},
        new[] {-1, 1,-1},
        new[] { 1, 1,-1},
    };
    private static readonly int[][] TraingleInfo =
    {
        //bottom
        new[]{1,0,2},
        new[]{2,0,3},
        //top
        new[]{5,6,4},
        new[]{4,6,7},
        //front
        new[]{0,1,5},
        new[]{5,4,0},
        //back
        new[]{2,3,7},
        new[]{6,2,7},
        //right
        new[]{1,2,6},
        new[]{6,5,1},
        //left
        new[]{4,3,0},
        new[]{3,4,7},
    };
}
