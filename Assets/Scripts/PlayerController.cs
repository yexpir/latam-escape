using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _forwardSpeed;
    [SerializeField] float _sideStepSpeed;
    [SerializeField] float _sideStepSize;
    [SerializeField] float _turnSpeed;
    bool isSideSteping;

    RaycastHit _hitR;
    RaycastHit _hitL;
    void Update()
    {
        transform.position += transform.forward * (_forwardSpeed * Time.deltaTime);
        if(isSideSteping) return;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(Physics.Raycast(transform.position, transform.right, 10) ? 
                SideStep(1) : TurnCorner(1));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(Physics.Raycast(transform.position, -transform.right, 10) ? 
                SideStep(-1) : TurnCorner(-1));
        }
    }

    Vector3 targetSidePos;
    IEnumerator SideStep(int direction)
    {
        if (Physics.Raycast(transform.position, transform.right * direction, 3, Block.blockLayerMask)) yield break;
        isSideSteping = true;
        float currDistance;
        var prevDistance = _sideStepSize;
        
        if (Mathf.Abs(transform.right.x) > Mathf.Abs(transform.right.z))
        {
            targetSidePos = transform.position + transform.right * (_sideStepSize * direction);
            while (true)
            {
                transform.position += transform.right * (direction * _sideStepSpeed * Time.deltaTime);
                targetSidePos.z = transform.position.z;
                currDistance = Vector3.Distance(targetSidePos, transform.position);
                if (currDistance > prevDistance)
                {
                    break;
                }
                prevDistance = currDistance;
                yield return null;
            }
            transform.position = targetSidePos;
        }
        else
        {
            targetSidePos = transform.position + transform.right * (_sideStepSize * direction);
            while (true)
            {
                transform.position += transform.right * (direction * _sideStepSpeed * Time.deltaTime);
                targetSidePos.x = transform.position.x;
                currDistance = Vector3.Distance(targetSidePos, transform.position);
                if (currDistance > prevDistance)
                {
                    break;
                }
                prevDistance = currDistance;
                yield return null;
            }
            transform.position = targetSidePos;
        }
        
        
        isSideSteping = false;
    }

    

    IEnumerator TurnCorner(int direction)
    {
        isSideSteping = true;
        
        var target = transform.position + transform.right * direction;
        var lookRotation = Quaternion.LookRotation(target - transform.position);
        var time = 0.0f;
        while (time < 0.6f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime * _turnSpeed;
            print(time + "DEJAVUUU!!!  " + transform.rotation + " --- " + lookRotation);
            yield return null;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 1);
        
        isSideSteping = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Block")) GameManager.ResetCurrentScene();
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 10);
        Gizmos.DrawLine(transform.position, transform.position - transform.right * 10);
        Gizmos.DrawWireSphere(targetSidePos, 3);
    }
}
