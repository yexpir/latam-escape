using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class TravelerOld : MonoBehaviour
{
    Path _currentPath;
    bool _shouldStop;

    public Coroutine Travel(Path path, float speed)
    {
        return StartCoroutine(Co_Travel(path, speed));
    }
    IEnumerator Co_Travel(Path path, float speed)
    {
        while (path.Spline.Count < 2)
            yield return null;
        _currentPath = path;
        _shouldStop = false;
        var pathTime = 0.0f;
        var time = 0.0f;
        while (pathTime < 1)
        {
            var totalDistance = path.GetTotalDistance();
            
            transform.position = (Vector3) path.Spline.EvaluatePosition(pathTime) + path.transform.position;
            transform.forward = path.Spline.EvaluateTangent(pathTime);
            
            yield return null;
            
            time += Time.deltaTime * speed;
            pathTime = time / totalDistance;
            print("SHOULD STOP = " + _shouldStop);
            if (!_shouldStop) continue;
            
            pathTime = Mathf.Ceil(pathTime / 0.25f) * 0.25f;
            if (pathTime > 1.0f) pathTime = 0.0f;
            break;
        }

        print("PATHTIME END = " + pathTime);
        transform.position = path.Spline.EvaluatePosition(pathTime);
        transform.forward = path.Spline.EvaluateTangent(pathTime);
    }
    
    public Coroutine Sidestep(float size, float speed, float dir)
    {
        return StartCoroutine(Co_Sidestep(size, speed, dir));
    }

    IEnumerator Co_Sidestep(float size, float speed, float dir)
    {
        var origin = _currentPath.transform.position;
        var target = origin + transform.right * (size * dir); 
        var time = 0.0f;
        while (time < 1)
        {
            _currentPath.transform.position = Vector3.Lerp(origin, target, time);
            time += Time.deltaTime * speed;
            yield return null;
        }
        _currentPath.transform.position = Vector3.Lerp(origin, target, 1);
    }

    public void Enter()
    {
        _shouldStop = false;
    }

    public void Exit()
    {
        _shouldStop = true;
    }
}
