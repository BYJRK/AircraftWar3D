using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    [SerializeField] AircraftSO aircraftSO;

    void Start()
    {

    }

    void Update()
    {
        transform.position += Vector3.forward * aircraftSO.basicSpeed * Time.deltaTime;

        if (transform.position.z > -0.8f)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Aircraft>(out var aircraft))
        {
            if (!aircraft.isEnemy)
                return;

            aircraft.Damage(aircraftSO.hp);

            aircraftSO.CastExplosion(transform.position);

            Destroy(gameObject);
        }
    }
}
