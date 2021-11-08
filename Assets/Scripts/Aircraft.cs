using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour
{
    public AircraftSO aircraftSO;

    protected int hp;
    protected float speed;
    public float Speed => speed;
    public float speedScaler = 1f;

    // Start is called before the first frame update
    void Start()
    {
        hp = aircraftSO.hp;
        speed = aircraftSO.basicSpeed;
    }

    public void Damage(int value)
    {
        hp -= value;
        if (hp <= 0)
        {
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        Destroy(gameObject);

        aircraftSO.CastExplosion(transform.position);
    }
}
