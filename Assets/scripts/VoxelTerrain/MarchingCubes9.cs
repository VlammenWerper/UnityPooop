using UnityEngine;
using System.Collections;

public class MarchingCubes9 : MonoBehaviour
{
    private Point9[,,] points;

    public int resolution = 10;
    public float size = 10f;
    public float isolevel;
    private int IndexCount;

    private Vector3[] vertices;
    private int[] traingles;
    Mesh mesh;

    public float xdiff;
    public float ydiff;
    public float zdiff;
    public Mesh GenerateMeshInfo()
    {
        return new Mesh();
    }
    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        MakePoints();

    }
    void Update()
    {
        if (points.GetLength(0) != resolution)
        {
            MakePoints();
        }
        int lenght = GetLenght();
        vertices = new Vector3[lenght];
        traingles = new int[lenght];
        MarchCubes();
        mesh.vertices = vertices;
        mesh.triangles = traingles;
        mesh.RecalculateNormals();
    }
    private void OnDrawGizmos()
    {/*
        if (points.GetLength(0) == null) return;

        for (int z = 0; z < points.GetLength(2); z++)
        {
            for (int y = 0; y < points.GetLength(1); y++)
            {
                for (int x = 0; x < points.GetLength(0); x++)
                {
                    Gizmos.DrawSphere(points[x,y,z].position + this.transform.position,0.1f);
                }
            }
        }
        */
    }
    private int GetLenght()
    {
        int lenght = 0;
        for (int z = 0; z < points.GetLength(2) - 1; z++)
        {
            for (int y = 0; y < points.GetLength(1) - 1; y++)
            {
                for (int x = 0; x < points.GetLength(0) - 1; x++)
                {
                    int index = 0;
                    Point9[] cubeCorners = new Point9[8];
                    for (int i = 0; i < 8; i++)
                    {
                        cubeCorners[i] = points[x + cubepointsx[i], y + cubepointsy[i], z + cubepointsz[i]];
                        if (cubeCorners[i].weight <= isolevel)
                        {
                            index |= 1 << i;
                        }
                    }
                    lenght += Lookuptables.TriangleTable[index].Length;
                }
            }
        }
        return lenght;
    }
    private void MarchCubes()
    {
        IndexCount = 0;
        for (int z = 0; z < points.GetLength(2) - 1; z++)
        {
            for (int y = 0; y < points.GetLength(1) - 1; y++)
            {
                for (int x = 0; x < points.GetLength(0) - 1; x++)
                {
                    int index = 0;
                    Vector3[] edgeVert = new Vector3[12];
                    Point9[] cubeCorners = new Point9[8];
                    for (int i = 0; i < 8; i++)
                    {
                        cubeCorners[i] = points[x + cubepointsx[i], y + cubepointsy[i], z + cubepointsz[i]];
                        if (cubeCorners[i].weight <= isolevel)
                        {
                            index |= 1 << i;
                        }
                    }
                    for (int i = 0; i < 12; i++)
                    {
                        int[] edgecorners = Lookuptables.EdgeIndexes[i];
                        int p1 = edgecorners[0]; 
                        int p2 = edgecorners[1];
                        edgeVert[i] = Interpolate(cubeCorners[p1], cubeCorners[p2]);
                    }

                    int[] row = Lookuptables.TriangleTable[index];

                    for (int i = 0; i < row.GetLength(0); i+=3)
                    {
                        vertices[IndexCount] = edgeVert[row[i + 0]];
                        traingles[IndexCount] = IndexCount;
                        IndexCount++;

                        vertices[IndexCount] = edgeVert[row[i + 1]];
                        traingles[IndexCount] = IndexCount;
                        IndexCount++;

                        vertices[IndexCount] = edgeVert[row[i + 2]];
                        traingles[IndexCount] = IndexCount;
                        IndexCount++;
                    }
                }
            }
        }
    }
    private Vector3 Interpolate(Point9 p1, Point9 p2)
    {/*
        float mu = (isolevel - p1.weight) / (p2.weight - p1.weight);
        Vector3 pr = (mu * (p1.position - p2.position)) + p1.position;
        return pr;
        */
        Vector3 vertice = new Vector3();
        Vector3 use = new Vector3();
        Vector3 other = new Vector3();
        float usedensity = 0f;
        float otherdensity = 0f;
        if (p1.position.x >= p2.position.x || p1.position.y >= p2.position.y || p1.position.z >= p2.position.z)
        {
            use = p2.position;
            other = p1.position;
            usedensity = p2.weight;
            otherdensity = p1.weight;
        }
        if (p1.position.x < p2.position.x || p1.position.y < p2.position.y || p1.position.z < p2.position.z)
        {
            use = p1.position;
            other = p2.position;
            usedensity = p1.weight;
            otherdensity = p2.weight;
        }
        float offset = 0.5f;
        float difference = usedensity - otherdensity;
        if (difference >= 0.0001f)
        {
            offset = isolevel;
        }
        if (difference != 0)
        {
            // calculate the vertex between the 2
            //bv: d1(use) = 0.3f && d2(other) = 0.5f && surfacelevel = 0.5
            //-> (0.5 - 0.3)/(0.5-0.3) = 1
            //
            //bv: d1(use) = 0.4f && d2(other) = 0.6f && surfacelevel = 0.5
            //-> (0.5 - 0.4)/(0.6-0.4) = 0.5
            //
            offset = Mathf.Abs((isolevel - usedensity) / difference);
        }
        if ((use != null) && (other != null))
        {
            if (use.x == other.x)
            {
                if (use.y == other.y)
                {
                    vertice = new Vector3(use.x, use.y, use.z + offset);
                }
            }
            if (use.x == other.x)
            {
                if (use.z == other.z)
                {
                    vertice = new Vector3(use.x, use.y + offset, use.z);
                }
            }
            if (use.y == other.y)
            {
                if (use.z == other.z)
                {
                    vertice = new Vector3(use.x + offset, use.y, use.z);
                }
            }
        }
        if (vertice == null)
        {
            Debug.Log("null vertice");
        }
        return vertice;
    }
    private void MakePoints()
    {
        points = new Point9[resolution, resolution, resolution];
        float cellsize = size/ resolution;
        for (int z = 0; z < resolution; z++)
        {
            for (int y = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    points[x, y, z] = new Point9(new Vector3(x * cellsize,y * cellsize,z * cellsize),GetWeight(x,y,z));
                }
            }
        }
    }
    private float GetWeight(int x, int y, int z)
    {
        float weight = 0;
        weight += Mathf.PerlinNoise(x * xdiff, z * zdiff);
        weight += Mathf.PerlinNoise(x * xdiff, y * ydiff);
        weight += Mathf.PerlinNoise(z * zdiff, y * ydiff);
        return weight / 3;
    }
    private static readonly int[] cubepointsx =
   {
        0,
        1,
        1,
        0,
        0,
        1,
        1,
        0,
    };
    private static readonly int[] cubepointsy =
    {
        0,
        0,
        0,
        0,
        1,
        1,
        1,
        1,
    };
    private static readonly int[] cubepointsz =
    {
        0,
        0,
        1,
        1,
        0,
        0,
        1,
        1,
    };
}
