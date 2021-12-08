using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MixArraySystem
{

    public static void MixArray<T>(ref T[] arr)
    {
        int length = arr.Length;

        System.Random random = new System.Random(DateTime.Now.Millisecond);
        int[] array = new int[length];
        for (int i = 0; i < length; i++) array[i] = i;
        array = array.OrderBy(x => random.Next()).ToArray();

        T[] tmpArr = new T[length];
        for (int i = 0; i < length; i++) tmpArr[i] = arr[i];
        for (int i = 0; i < length; i++) arr[i] = tmpArr[array[i]];

        for (int i = 0; i < array.Length; i++) Debug.Log(arr[i]);
    }
}
