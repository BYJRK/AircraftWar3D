using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour
{
    public AircraftSO aircraftSO;

    private int hp;
    private float speed;
    public float Speed => speed;
    public float speedScaler = 1f;

    public bool isEnemy;

    // Start is called before the first frame update
    void Start()
    {
        hp = aircraftSO.hp;
        speed = aircraftSO.basicSpeed;

        if (isEnemy)
        {
            speedScaler = Random.Range(0.75f, 1.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemy)
        {
            transform.position -= new Vector3(0, 0, speed * speedScaler * Time.deltaTime);
            if (transform.position.z < -22)
                Destroy(gameObject);
        }
    }

    public void Damage(int value)
    {
        hp -= value;
        if (hp <= 0)
        {
            Destroy(gameObject);

            aircraftSO.CastExplosion(transform.position);

            if (isEnemy)
            {
                GameManager.Instance.IncreaseScore(aircraftSO.score);
            }
        }
    }
}
