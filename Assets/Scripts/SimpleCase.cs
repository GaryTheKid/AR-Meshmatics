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


    void DrawManyNiceTriangles()
    {
        int xSize = 10;
        int zSize = 10;
        verts = new Vector3[(xSize + 1) * (zSize + 1)];
        triangle = new int[xSize * zSize];

        // iterate all vertices
        int i = 0;
        for (int x = 0; x <= xSize; x++) // each row
        {
            for (int z = 0; z <= zSize; z++) // each column
            {
                // calculate our y(height value) based on x and z
                float y = Y(x, z);

                verts[i] = new Vector3(x, y, z);
                i++;
            }
        }

        // map all triangles to the vertices we've just created
        for (int x = 0; x <= xSize; x++) // each row
        {
            for (int z = 0; z <= zSize; z++) // each column
            {
                // map them here...
            }
        }
    }

    // user's custom y function
    float Y(float x, float z)
    {
        return x * 2 + z * 4;
    }
}
