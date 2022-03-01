using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCase : MonoBehaviour
{
    // setup mesh and mesh filter
    Mesh mesh;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }



    // this is how you draw a single triangle
    Vector3[] verts;
    int[] triangle;

    void DrawOneNiceTriangle()
    {
        verts = new Vector3[3];            // array contains 3 vertices
        verts[0] = new Vector3(0, 0, 0);   // the first point
        verts[1] = new Vector3(1, 0, 0);   // the second point
        verts[2] = new Vector3(0, 0, 1);   // the third point

        triangle = new int[3];             // 3 vertices to construct a triangle
        triangle[0] = 0;                   // the 1st vertice 
        triangle[1] = 1;                   // the 2nd vertice
        triangle[2] = 2;                   // the 3rd vertice

        mesh.vertices = verts;
        mesh.triangles = triangle;
    }
}
