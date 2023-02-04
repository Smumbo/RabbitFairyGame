using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public Drawable lastObject;
    public static SceneManager instance;
    private bool canDraw;

    public SceneManager()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public bool GetCanDraw()
    {
        return canDraw;
    }

    public void SetCanDraw(bool b)
    {
        canDraw = b;
    }
}
