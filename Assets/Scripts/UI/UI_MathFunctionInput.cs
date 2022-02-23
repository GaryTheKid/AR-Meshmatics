using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_MathFunctionInput : MonoBehaviour
{
    public Func<int> GetMathFunctionFromInput(string input)
    {
        Func<int> function = null;

        // convert the user input to a 2-variable math function
        // input must include a "=" sign
        if (!input.Contains("="))
            return null;
        // input must has only 1 variable 


        return function;
    }
}
