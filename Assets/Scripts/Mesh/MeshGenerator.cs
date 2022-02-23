using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;
    Color[] colors;

    public int xSize = 5;
    public int zSize = 5;
    public float scaler = 0.01f;
    public Gradient gradient;
    public Func<float, float, float> customFunction;

    float minHeight;
    float maxHeight;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        customFunction = (float x, float z) =>
        {
            return x * x - z * z;
        };

        StartCoroutine(CreateShape());
    }

    private void Update()
    {
        UpdateMesh();
    }

    IEnumerator CreateShape()
    {
        // vertices and colors
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        colors = new Color[vertices.Length];

        int i = 0;
        for (float z = 0; z <= zSize; z++)
        {
            for (float x = 0; x <= xSize; x++)
            {
                // the user function
                float y = scaler * customFunction.Invoke(x, z);
                float height = Mathf.InverseLerp(minHeight, maxHeight, y);

                colors[i] = gradient.Evaluate(y);
                vertices[i] = new Vector3(x, y, z);

                if (y > maxHeight)
                    maxHeight = y;
                if (y < minHeight)
                    minHeight = y;

                i++;
            }
        }

        // uvs
        uvs = new Vector2[vertices.Length];
        print(uvs);
        i = 0;
        for (float z = 0; z <= zSize; z++)
        {
            for (float x = 0; x <= xSize; x++)
            {
                uvs[i] = new Vector2(x / xSize, z / zSize);
                i++;
            }
        }

        // triangles
        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;

                yield return new WaitForSeconds(0.05f);
            }
            vert++;
        } 
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.colors = colors;

        mesh.RecalculateNormals();
    }
}
