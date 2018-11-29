using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    public Texture2D cursor;

    public static CursorLockMode lockState { get; internal set; }
    public static bool visible { get; internal set; }

    void Awake()
    {
        Cursor.SetCursor(cursor);
    }

    private static void SetCursor(Texture2D cursor)
    {
        throw new NotImplementedException();
    }
}
