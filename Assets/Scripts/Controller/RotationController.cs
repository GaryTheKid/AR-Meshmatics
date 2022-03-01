using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            float rotateSpeed = 0.09f;
            Touch touchZero = Input.GetTouch(0);

            //Rotate the model based on offset
            Vector3 localAngle = transform.localEulerAngles;
            localAngle.y -= rotateSpeed * touchZero.deltaPosition.x;
            transform.localEulerAngles = localAngle;
        }
    }
}
