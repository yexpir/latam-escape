using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (Direction != Vector2.zero)
        {
            transform.position += Direction.To3() * speed;
        }
    }
    
    Vector2 _direction = Vector2.zero;
    Vector2 Direction
    {
        get
        {
            _direction.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            return _direction;
        }
    }
}
