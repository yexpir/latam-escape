using UnityEngine;
using UnityEngine.Splines;

public class Path
{
        public Spline Spline => _spline;
        
        SplineContainer _container;
        Spline _spline;
        
        public Path(SplineContainer container)
        {
                _container = container;
                _spline = _container.Spline;
        }

        public void AddKnot(Vector3 position)
        {
                var knot = new BezierKnot(position, Vector3.zero, Vector3.zero, Quaternion.identity);
                _spline.Add(knot, TangentMode.Mirrored);
        }
        public void AddKnot(Vector3 position, Quaternion rotation)
        {
                var knot = new BezierKnot(position, Vector3.zero, Vector3.zero, rotation);
                _spline.Add(knot, TangentMode.Mirrored);
        }
        public void AddKnot(Vector3 position, Quaternion rotation, Vector3 tanIn, Vector3 tanOut)
        {
                var knot = new BezierKnot(position, tanIn, tanOut, rotation);
                _spline.Add(knot, TangentMode.Mirrored);
        }

        public void RemoveKnot(int index)
        {
                _spline.RemoveAt(index);
        }
}