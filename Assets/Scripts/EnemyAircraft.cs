using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAircraft : Aircraft
{
    void Update()
    {
        transform.position -= new Vector3(0, 0, speed * speedScaler * Time.deltaTime);
        if (transform.position.z < -22)
            Destroy(gameObject);
    }

    protected override void OnDeath()
    {
        base.OnDeath();

        GameManager.Instance.IncreaseScore(aircraftSO.score);
    }
}
