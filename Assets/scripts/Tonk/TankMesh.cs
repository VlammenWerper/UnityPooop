using UnityEngine;
using System.Collections.Generic;

public class TankMesh : MonoBehaviour
{
    MeshCollider meshcol;
    Mesh mesh;
    LineRenderer liner;

    private Vector3[] vertices;
    private int[] traingles;
    void Start()
    {
        //mesh.();
    }
    void LineRFonction()
    {
        liner = GetComponent<LineRenderer>();
        meshcol = GetComponent<MeshCollider>();
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        traingles = mesh.triangles;
        liner.positionCount = vertices.Length;
        liner.widthMultiplier = 0.1f;
        for (int i = 0; i < vertices.Length; i += 3)
        {
            liner.SetPosition(traingles[i + 0], new Vector3(
    vertices[i + 0].x * transform.localScale.x,
    vertices[i + 0].y * transform.localScale.y,
    vertices[i + 0].z * transform.localScale.z
    ));
            liner.SetPosition(traingles[i + 1], new Vector3(
    vertices[i + 1].x * transform.localScale.x,
    vertices[i + 1].y * transform.localScale.y,
    vertices[i + 1].z * transform.localScale.z
    ));
            liner.SetPosition(traingles[i + 2], new Vector3(
    vertices[i + 2].x * transform.localScale.x,
    vertices[i + 2].y * transform.localScale.y,
    vertices[i + 2].z * transform.localScale.z
    ));
        }
        liner.BakeMesh(mesh, false);
    }


}
