using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_MathFunctionInput : MonoBehaviour
{
    public MeshGenerator meshGenerator;

    public void CatchInput(string input)
    {
        print(GetMathFunctionFromInput("x*x - z  * z").Invoke(2, 0));
        //print(GetMathFunctionFromInput("x*x - z  * z").Invoke(1, 2));
        //print(GetMathFunctionFromInput("x*x - z  * z").Invoke(2, 3));
        meshGenerator.customFunction = GetMathFunctionFromInput("x*x - z  * z");
        meshGenerator.StartCreateShape();
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
            if (i == '+' || i == '-' || i == '*' || i == '/')
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
            }
        }
        catch(Exception e) 
        {
            Debug.Log(e);
            return 0;
        }
    }
}