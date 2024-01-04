using System.Collections;
using Actions_Stuff;
using DefaultNamespace;
using UnityEngine;

public class Traveler : MonoBehaviour
{
    /*ActionRoutine _actionRoutine;
    [SerializeField] Character _character;
    Actioner _actioner;
    void Awake()
    {
        _actioner = GetComponent<Actioner>();
        var path = PathBuilder.Instance.PathForward(transform, 1000);
        _actionRoutine = ActionBuilder.CreateAction((int)Actions.Forward, Co_Travel(path, _character.travelSpeed));
        _actioner.AddToPool(_actionRoutine);
    }*/

    public IEnumerator Co_Travel(Path path, float speed)
    {
        yield return null;
    }
}