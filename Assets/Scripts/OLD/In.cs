using System;
using UnityEngine;

public class In : MonoBehaviour
{
    public static bool RightPressed;
    public static bool LeftPressed;
    public static bool RightReleased;
    public static bool LeftReleased;
    public static float X;
    public static int XInt => (int)X;
    static float prevX;
    public void Update()
    {
        X = Input.GetAxisRaw("Horizontal");
        
        RightPressed = prevX <= 0 && X > 0;
        LeftPressed = prevX >= 0 && X < 0;
        
        RightReleased = prevX >= 0 && X <= 0;
        LeftReleased = prevX <= 0 && X >= 0;
        
        prevX = X;
    }
}