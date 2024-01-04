using UnityEngine;
using UnityEngine.Splines;

public class CirclePath : Path
{
    float Stepsize => CityBuilder.stepsideSize;
    float tanInHandle => 0.5519150244935105707435627f;

    Transform _target;
    Vector3 Up => _target.forward * Stepsize;
    Vector3 Down => -_target.forward * Stepsize;
    Vector3 Left => -_target.right * Stepsize;
    Vector3 Right => _target.right * Stepsize;
        
        
    public void Init(Transform target, int dir)
    {
        base.Init();
        _target = target;
        var mult = tanInHandle * dir;
        AddKnot(target.position, Quaternion.identity, Down * tanInHandle, Up * tanInHandle);
        AddKnot(target.position + Up + Right * dir, Quaternion.identity, Left * mult, Right * mult);
        AddKnot(target.position + Right * (2 * dir), Quaternion.identity, Up * tanInHandle, Down * tanInHandle);
        AddKnot(target.position + Down + Right * dir, Quaternion.identity, Right * tanInHandle, Left * mult);
        Spline.Closed = true;
    }

    public override float GetTotalDistance() => 1 * Mathf.PI * CityBuilder.stepsideSize;
}