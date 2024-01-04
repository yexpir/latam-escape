using UnityEngine;
using UnityEngine.Splines;

public class ForwardPath : Path
{
    public void Init(Transform target, float maxDistance)
    {
        base.Init();
        
        AddKnot(target.position, target.rotation, -target.forward, target.forward);
        var ray = new Ray(target.position, target.forward);
        RaycastHit hit;
        AddKnot(Physics.Raycast(ray, out hit, maxDistance, Block.layerMask) ? 
            hit.point : 
            target.position + target.forward * maxDistance, target.rotation, -target.forward, target.forward);
    }

    public override float GetTotalDistance() => Vector3.Distance(Spline[0].Position, Spline[1].Position);
}