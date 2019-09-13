using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Logger
{
    public static T Log<T>(this T obj, string prompt = default)
    {
        Debug.Log(prompt + " "+ obj?.ToString());
        return obj;
    }
}
