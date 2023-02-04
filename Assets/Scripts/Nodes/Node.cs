using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    public GameObject createdObject;
    public abstract void Activate();
    public abstract void Deactivate();
}
