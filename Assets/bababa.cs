using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class bababa : MonoBehaviour
{

    SplineContainer spline;
    // Start is called before the first frame update
    void Awake()
    {
        spline = GetComponent<SplineContainer>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
