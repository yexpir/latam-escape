using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
public abstract class Path : MonoBehaviour
{
        public Spline Spline => _spline;
        Spline _spline;

        public SplineContainer Container => _container;
        SplineContainer _container;
        
        protected void Init()
        {
                _container = GetComponent<SplineContainer>();
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
        public void AddKnot(Vector3 position, Quaternion rotation, Vector3 tanOut)
        {
                var knot = new BezierKnot(position, -tanOut, tanOut, rotation);
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

        public void OffsetPath(Vector3 offset)
        {
                for (int i = 0; i < _spline.Count; i++)
                {
                }
        }

        public abstract float GetTotalDistance();
}