using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public int Index;
    
    public void TrySet()
    {
        Manager.Instance.SetElement(this);
    }
}
