using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter1;
    [SerializeField] private MeshFilter meshFilter2;
    [SerializeField] private MeshFilter meshFilter3;
    [SerializeField] private MeshFilter meshFilter4;

    Mesh mesh1;
    Vector3[] vertices1;
    int[] triangles1;
    Vector2[] uvs1;
    Color[] colors1;

    Mesh mesh2;
    Vector3[] vertices2;
    int[] triangles2;
    Vector2[] uvs2;
    Color[] colors2;

    Mesh mesh3;
    Vector3[] vertices3;
    int[] triangles3;
    Vector2[] uvs3;
    Color[] colors3;

    Mesh mesh4;
    Vector3[] vertices4;
    int[] triangles4;
    Vector2[] uvs4;
    Color[] colors4;

    public int xSize = 5;
    public int zSize = 5;
    public float scaler = 0.01f;
    public float generationSpeed = 20f;
    public Gradient gradient;
    float minHeight;
    float maxHeight;

    public Func<float, float, float> customFunction;
    public string input;

    private void Start()
    {
        mesh1 = new Mesh();
        mesh2 = new Mesh();
        mesh3 = new Mesh();
        mesh4 = new Mesh();
        meshFilter1.mesh = mesh1;
        meshFilter2.mesh = mesh2;
        meshFilter3.mesh = mesh3;
        meshFilter4.mesh = mesh4;
    }

    private void Update()
    {
        UpdateMesh();
    }

    public void StartCreateShape()
    {
        StartCoroutine(CreateShape1());
        StartCoroutine(CreateShape2());
        StartCoroutine(CreateShape3());
        StartCoroutine(CreateShape4());
    }

    IEnumerator CreateShape1()
    {
        // vertices and colors
        vertices1 = new Vector3[(xSize + 1) * (zSize + 1)];
        colors1 = new Color[vertices1.Length];

        // +x +z
        int i = 0;
        for (float x = 0; x <= xSize; x++)
        {
            for (float z = 0; z <= zSize; z++)
            {
                // the user function
                float y = scaler * GetMathFunctionFromInput(input).Invoke(x, z);
                float height = Mathf.InverseLerp(minHeight, maxHeight, y);

                colors1[i] = gradient.Evaluate(height);
                vertices1[i] = new Vector3(x, y, z);

                if (y > maxHeight)
                    maxHeight = y;
                if (y < minHeight)
                    minHeight = y;

                i++;
            }
        }

        // uvs
        uvs1 = new Vector2[vertices1.Length];
        i = 0;
        for (float z = 0; z <= zSize; z++)
        {
            for (float x = 0; x <= xSize; x++)
            {
                uvs1[i] = new Vector2(x / xSize, z / zSize);
                i++;
            }
        }

        // triangles
        triangles1 = new int[xSize * zSize * 6 * 2];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                // +x +z
                triangles1[tris + 0] = vert + 0;
                triangles1[tris + 1] = vert + 1;
                triangles1[tris + 2] = vert + xSize + 1;
                triangles1[tris + 3] = vert + 1;
                triangles1[tris + 4] = vert + xSize + 2;
                triangles1[tris + 5] = vert + xSize + 1;
                triangles1[tris + 6] = vert + 0;
                triangles1[tris + 7] = vert + xSize + 1;
                triangles1[tris + 8] = vert + 1;
                triangles1[tris + 9] = vert + 1;
                triangles1[tris + 10] = vert + xSize + 1;
                triangles1[tris + 11] = vert + xSize + 2;

                tris += 12;
                vert++;
                yield return new WaitForSeconds(1f / generationSpeed);
            }
            vert++;
        } 
    }

    IEnumerator CreateShape2()
    {
        // vertices and colors
        vertices2 = new Vector3[(xSize + 1) * (zSize + 1)];
        colors2 = new Color[vertices2.Length];

        // +x -z
        int i = 0;
        for (float z = 0; z >= -zSize; z--)
        {
            for (float x = 0; x <= xSize; x++)
            {
                // the user function
                float y = scaler * GetMathFunctionFromInput(input).Invoke(x, z);
                float height = Mathf.InverseLerp(minHeight, maxHeight, y);

                colors2[i] = gradient.Evaluate(height);
                vertices2[i] = new Vector3(x, y, z);

                if (y > maxHeight)
                    maxHeight = y;
                if (y < minHeight)
                    minHeight = y;

                i++;
            }
        }

        // uvs
        uvs2 = new Vector2[vertices2.Length];
        i = 0;
        for (float z = 0; z <= zSize; z++)
        {
            for (float x = 0; x <= xSize; x++)
            {
                uvs2[i] = new Vector2(x / xSize, z / zSize);
                i++;
            }
        }

        // triangles
        triangles2 = new int[xSize * zSize * 6 * 2];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                // +x -z
                triangles2[tris + 0] = vert + 0;
                triangles2[tris + 1] = vert + 1;
                triangles2[tris + 2] = vert + xSize + 1;
                triangles2[tris + 3] = vert + xSize + 1;
                triangles2[tris + 4] = vert + 1;
                triangles2[tris + 5] = vert + xSize + 2;
                triangles2[tris + 6] = vert + 0;
                triangles2[tris + 7] = vert + xSize + 1;
                triangles2[tris + 8] = vert + 1;
                triangles2[tris + 9] = vert + 1;
                triangles2[tris + 10] = vert + xSize + 1;
                triangles2[tris + 11] = vert + xSize + 2;

                tris += 12;
                vert++;
                yield return new WaitForSeconds(1f / generationSpeed);
            }
            vert++;
        }
    }

    IEnumerator CreateShape3()
    {
        // vertices and colors
        vertices3 = new Vector3[(xSize + 1) * (zSize + 1)];
        colors3 = new Color[vertices3.Length];

        // +x -z
        int i = 0;
        for (float z = 0; z <= zSize; z++)
        {
            for (float x = 0; x >= -xSize; x--)
            {
                // the user function
                float y = scaler * GetMathFunctionFromInput(input).Invoke(x, z);
                float height = Mathf.InverseLerp(minHeight, maxHeight, y);

                colors3[i] = gradient.Evaluate(height);
                vertices3[i] = new Vector3(x, y, z);

                if (y > maxHeight)
                    maxHeight = y;
                if (y < minHeight)
                    minHeight = y;

                i++;
            }
        }

        // uvs
        uvs3 = new Vector2[vertices3.Length];
        i = 0;
        for (float z = 0; z <= zSize; z++)
        {
            for (float x = 0; x <= xSize; x++)
            {
                uvs3[i] = new Vector2(x / xSize, z / zSize);
                i++;
            }
        }

        // triangles
        triangles3 = new int[xSize * zSize * 6 * 2];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                // +x -z
                triangles3[tris + 0] = vert + 0;
                triangles3[tris + 1] = vert + 1;
                triangles3[tris + 2] = vert + xSize + 1;
                triangles3[tris + 3] = vert + xSize + 1;
                triangles3[tris + 4] = vert + 1;
                triangles3[tris + 5] = vert + xSize + 2;
                triangles3[tris + 6] = vert + 0;
                triangles3[tris + 7] = vert + xSize + 1;
                triangles3[tris + 8] = vert + 1;
                triangles3[tris + 9] = vert + 1;
                triangles3[tris + 10] = vert + xSize + 1;
                triangles3[tris + 11] = vert + xSize + 2;

                tris += 12;
                vert++;
                yield return new WaitForSeconds(1f / generationSpeed);
            }
            vert++;
        }
    }

    IEnumerator CreateShape4()
    {
        // vertices and colors
        vertices4 = new Vector3[(xSize + 1) * (zSize + 1)];
        colors4 = new Color[vertices4.Length];

        // +x -z
        int i = 0;
        for (float x = 0; x >= -xSize; x--)
        {
            for (float z = 0; z >= -zSize; z--)
            {
                // the user function
                float y = scaler * GetMathFunctionFromInput(input).Invoke(x, z);
                float height = Mathf.InverseLerp(minHeight, maxHeight, y);

                colors4[i] = gradient.Evaluate(height);
                vertices4[i] = new Vector3(x, y, z);

                if (y > maxHeight)
                    maxHeight = y;
                if (y < minHeight)
                    minHeight = y;

                i++;
            }
        }

        // uvs
        uvs4 = new Vector2[vertices4.Length];
        i = 0;
        for (float z = 0; z <= zSize; z++)
        {
            for (float x = 0; x <= xSize; x++)
            {
                uvs4[i] = new Vector2(x / xSize, z / zSize);
                i++;
            }
        }

        // triangles
        triangles4 = new int[xSize * zSize * 6 * 2];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                // +x -z
                triangles4[tris + 0] = vert + 0;
                triangles4[tris + 1] = vert + 1;
                triangles4[tris + 2] = vert + xSize + 1;
                triangles4[tris + 3] = vert + xSize + 1;
                triangles4[tris + 4] = vert + 1;
                triangles4[tris + 5] = vert + xSize + 2;
                triangles4[tris + 6] = vert + 0;
                triangles4[tris + 7] = vert + xSize + 1;
                triangles4[tris + 8] = vert + 1;
                triangles4[tris + 9] = vert + 1;
                triangles4[tris + 10] = vert + xSize + 1;
                triangles4[tris + 11] = vert + xSize + 2;

                tris += 12;
                vert++;
                yield return new WaitForSeconds(1f / generationSpeed);
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        mesh1.Clear();
        mesh2.Clear();
        mesh3.Clear();
        mesh4.Clear();

        mesh1.vertices = vertices1;
        mesh1.triangles = triangles1;
        mesh1.uv = uvs1;
        mesh1.colors = colors1;
        mesh2.vertices = vertices2;
        mesh2.triangles = triangles2;
        mesh2.uv = uvs2;
        mesh2.colors = colors2;
        mesh3.vertices = vertices3;
        mesh3.triangles = triangles3;
        mesh3.uv = uvs3;
        mesh3.colors = colors3;
        mesh4.vertices = vertices4;
        mesh4.triangles = triangles4;
        mesh4.uv = uvs4;
        mesh4.colors = colors4;

        mesh1.RecalculateNormals();
        mesh2.RecalculateNormals();
        mesh3.RecalculateNormals();
        mesh4.RecalculateNormals();
    }

    #region Read Input
    public void CatchInput(string input)
    {
        this.input = input;
        StartCreateShape();
    }

    public Func<float, float, float> GetMathFunctionFromInput(string input)
    {
        // trim all white spaces
        string trimedInput = input.Replace(" ", "");

        // convert the user input to a 2-variable math function
        bool checkConversion = false;
        List<char> oprands = new List<char>();
        List<char> operators = new List<char>();
        ConvertInputToExpressionElementLists(trimedInput, oprands, operators, out checkConversion);
        if (!checkConversion)
        {
            return null;
        }

        // convert operations to func
        Func<float, float, float> function = (x, z) =>
        {
            List<float> decimalOprands = ConvertOprandCharListToFloatList(oprands, x, z);
            return ConvertExpressionElementsToResult(decimalOprands, operators);
        };

        return function;
    }

    /// <summary>
    /// Convert the user input string to 1 oprand list and 
    /// 1 operator list, if the conversion succeeds, it will 
    /// out a bool value = true, otherwise out value = false.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="oprands"></param>
    /// <param name="operators"></param>
    /// <param name="isConversionSuccessful"></param>
    private void ConvertInputToExpressionElementLists(string input, List<char> oprands, List<char> operators, out bool isConversionSuccessful)
    {
        foreach (char i in input)
        {
            if (i == '+' || i == '-' || i == '*' || i == '/' || i == '^')
            {
                operators.Add(i);
            }
            else
            {
                oprands.Add(i);
            }
        }

        if (oprands.Count == operators.Count + 1)
            isConversionSuccessful = true;
        else
            isConversionSuccessful = false;
    }

    private float ConvertExpressionElementsToResult(List<float> decimalOprands, List<char> operators)
    {
        // handle all ^ operators
        for (int i = 0; i < operators.Count; i++)
        {
            if (operators[i] == '^')
            {
                decimalOprands[i] = SimpleOperation(decimalOprands[i], decimalOprands[i + 1], '^');
                decimalOprands.RemoveAt(i + 1);
                operators.RemoveAt(i);
                i = 0;
                continue;
            }
        }

        // handle all *, / operators
        for (int i = 0; i < operators.Count; i++)
        {
            if (operators[i] == '*')
            {
                decimalOprands[i] = SimpleOperation(decimalOprands[i], decimalOprands[i + 1], '*');
                decimalOprands.RemoveAt(i + 1);
                operators.RemoveAt(i);
                i = 0;
                continue;
            }
            if (operators[i] == '/')
            {
                decimalOprands[i] = SimpleOperation(decimalOprands[i], decimalOprands[i + 1], '/');
                decimalOprands.RemoveAt(i + 1);
                operators.RemoveAt(i);
                i = 0;
                continue;
            }
        }

        // handle all +, - operators
        for (int i = 0; i < operators.Count; i++)
        {
            if (operators[i] == '+')
            {
                decimalOprands[i] = SimpleOperation(decimalOprands[i], decimalOprands[i + 1], '+');
                decimalOprands.RemoveAt(i + 1);
                operators.RemoveAt(i);
                i = 0;
                continue;
            }
            if (operators[i] == '-')
            {
                decimalOprands[i] = SimpleOperation(decimalOprands[i], decimalOprands[i + 1], '-');
                decimalOprands.RemoveAt(i + 1);
                operators.RemoveAt(i);
                i = 0;
                continue;
            }
        }

        return decimalOprands[0];
    }

    private List<float> ConvertOprandCharListToFloatList(List<char> oprands, float x, float z)
    {
        // convert char list to float list
        List<float> decimalOprands = new List<float>();
        foreach (char c in oprands)
        {
            if (c == 'x')
            {
                decimalOprands.Add(x);
            }
            else if (c == 'z')
            {
                decimalOprands.Add(z);
            }
            else
            {
                decimalOprands.Add(Convert.ToSingle(c.ToString()));
            }
        }

        return decimalOprands;
    }

    private float SimpleOperation(float var1, float var2, char opt)
    {
        // handle divided by 0 exception
        try
        {
            switch (opt)
            {
                default:
                case '+': return var1 + var2;
                case '-': return var1 - var2;
                case '*': return var1 * var2;
                case '/': return var1 / var2;
                case '^': return Mathf.Pow(var1, var2);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return 0;
        }
    }
    #endregion
}
