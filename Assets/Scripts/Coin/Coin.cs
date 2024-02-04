using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Vector3 rotationAmount;
    public float amp;
    public float freq;
    Vector3 position;

    void Update()
    {
        transform.Rotate(rotationAmount * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Sin(freq * Time.time) * amp, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoinSpawner.CountCoin();
            Destroy(gameObject);
        }
    }
}
