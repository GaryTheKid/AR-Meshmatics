using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPointsGizmo : MonoBehaviour
{
    public Vector3[] verts;
    public int xSize;
    public int zSize;

    private void OnDrawGizmosSelected()
    {
        if (verts != null)
        {
            int i = 0;
            for (float x = 0; x <= xSize; x++)
            {
                for (float z = 0; z <= zSize; z++)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(transform.TransformPoint(verts[i]), 0.01f);
                    i++;
                }
            }
        }
    }
}
