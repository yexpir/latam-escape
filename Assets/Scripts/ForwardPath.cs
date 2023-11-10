using UnityEngine;
using UnityEngine.Splines;

public class ForwardPath : Path
{
    public ForwardPath(SplineContainer obj, Transform target, float maxDistance) : base(obj)
    {
        AddKnot(target.position, target.rotation);
        var ray = new Ray(target.position, target.forward);
        RaycastHit hit;
        AddKnot(Physics.Raycast(ray, out hit, maxDistance, Block.layerMask) ? 
            hit.point : 
            target.position + target.forward * maxDistance);
    }
}