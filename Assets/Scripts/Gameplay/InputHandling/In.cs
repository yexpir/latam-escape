using System;
using UnityEngine;

namespace Gameplay.InputHandling
{
    public class In : MonoBehaviour
    {
        public static float x;
        public static int xInt;

        public static bool leftPress;
        public static bool rightPress;
        
        public static bool leftRelease;
        public static bool rightRelease;

        public static bool leftHold;
        public static bool rightHold;

        public static bool movePress;
        public static bool moveHold;
        public static bool moveRelease;

        public static bool turnHold;

        public static int xButton;

        public static bool left;
        public static bool right;

        public static bool jumpPress;
        static In Instance { get; set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        public void Update()
        {
            x = Input.GetAxisRaw("Horizontal");
            xInt = x != 0 ? (int) MathF.Round(x, 0, MidpointRounding.AwayFromZero) : 0;

            leftPress = Input.GetKeyDown(KeyCode.LeftArrow);
            rightPress = Input.GetKeyDown(KeyCode.RightArrow);
            
            rightHold = Input.GetKey(KeyCode.RightArrow);
            leftHold = Input.GetKey(KeyCode.LeftArrow);
            
            leftRelease = Input.GetKeyUp(KeyCode.LeftArrow);
            rightRelease = Input.GetKeyUp(KeyCode.RightArrow);
            
            movePress = leftPress || rightPress;
            moveRelease = moveHold && !leftHold && !rightHold;
            moveHold = leftHold || rightHold;
            
            turnHold = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            jumpPress = Input.GetKeyDown(KeyCode.Space);
            
            if (leftPress)
                xButton = -1;
            if (rightPress)
                xButton = 1;
            if (moveRelease)
                xButton = xInt;

            left = xButton < 0;
            right = xButton > 0;
        }
    }
}
