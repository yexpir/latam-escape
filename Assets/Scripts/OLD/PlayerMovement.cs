using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Character _data;
    
    List<IEnumerator> abilityPool = new();
    List<Coroutine> activeAbilies = new();

    Coroutine turnRight;
    Coroutine turnLeft;
    Coroutine run;

    public float speed;

    public static PlayerMovement Instance;

    void Start()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
        
        speed = _data.travelSpeed;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StopAllCoroutines();
            activeAbilies.Clear();
            turnRight = Turn(1);
            run = null;
            turnLeft = null;
            activeAbilies.Add(turnRight);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StopAllCoroutines();
            activeAbilies.Clear();
            turnLeft = Turn(-1);
            run = null;
            turnRight = null;
            activeAbilies.Add(turnLeft);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) && activeAbilies.Contains(turnRight))
        {
            activeAbilies.Remove(turnRight);
            turnRight = null;
            StopAllCoroutines();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) && activeAbilies.Contains(turnLeft))
        {
            activeAbilies.Remove(turnLeft);
            turnLeft = null;
            StopAllCoroutines();
        }

        if (turnRight == null && turnLeft == null && run == null)
        {
            run = Run();
            activeAbilies.Add(run);
        }

    }

    Coroutine Run()
    {
        return StartCoroutine(Co_Run());
    }

    IEnumerator Co_Run()
    {
        
        while (true)
        {
            speed += _data.acceleration*Time.deltaTime;
            transform.position += transform.forward * (speed * Time.deltaTime);
            yield return null;
        }
    }
    
    Coroutine Turn(int direction)
    {
        return StartCoroutine(Co_Turn(direction));
    }

    IEnumerator Co_Turn(int direction)
    {
        var rotationPoint = transform.position + transform.right * (CityBuilder.cellSize * direction);
        while (true)
        {
            var linearDistance = speed * Time.deltaTime;

            var relativePosition = transform.position - rotationPoint;

            var rotationAngle = linearDistance / (2 * Mathf.PI * relativePosition.magnitude) * 360 * -direction;

            var newX = Mathf.Cos(Mathf.Deg2Rad * rotationAngle) * relativePosition.x - Mathf.Sin(Mathf.Deg2Rad * rotationAngle) * relativePosition.z;
            var newZ = Mathf.Sin(Mathf.Deg2Rad * rotationAngle) * relativePosition.x + Mathf.Cos(Mathf.Deg2Rad * rotationAngle) * relativePosition.z;

            var nextPosition = new Vector3(newX, relativePosition.y, newZ) + rotationPoint;
            var delta = nextPosition - transform.position;
            delta.y = 0;
            transform.position += delta;
            transform.right = (rotationPoint - transform.position) * direction;
            yield return null;
        }
    }
}
