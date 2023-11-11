using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

class Traveler : MonoBehaviour
{
    public void Travel(Path path, float speed)
    {
        StartCoroutine(Co_Travel(path, speed));
    }

    private IEnumerator Co_Travel(Path path, float speed)
    {
        while (path.Spline.Count < 2)
            yield return null;
        var time = 0.0f;
        var t = 0.0f;
        var totalDistance = Vector3.Distance(path.Spline[0].Position, path.Spline[1].Position);
        while (t < 1)
        {
            t = time / totalDistance;
            transform.position = (Vector3)path.Spline.EvaluatePosition(t) + path.transform.position;
            transform.forward = path.Spline.EvaluateTangent(t);
            yield return null;
            time += Time.deltaTime * speed;
        }
        transform.position = path.Spline.EvaluatePosition(1);
        transform.forward = path.Spline.EvaluateTangent(1);
    }
}