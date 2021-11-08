using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    [SerializeField] AircraftSO aircraftSO;

    void Update()
    {
        transform.position += Vector3.forward * aircraftSO.basicSpeed * Time.deltaTime;

        if (transform.position.z > -0.8f)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 如果击中了敌人飞机
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var aircraft = collision.gameObject.GetComponent<Aircraft>();

            aircraft.Damage(aircraftSO.hp);
            aircraftSO.CastExplosion(transform.position);

            Destroy(gameObject);
        }
    }
}
