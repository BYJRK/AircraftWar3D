using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    Camera mainCam;

    float speed;

    [SerializeField] Transform[] muzzles;
    int muzzleIndex = 0;

    bool isPowerUp = false;
    [SerializeField] float missileInterval = 0.6f;

    [SerializeField] GameObject missile;

    float lastMissile;

    void Start()
    {
        mainCam = Camera.main;
        Cursor.visible = false;
        speed = GetComponent<Aircraft>().aircraftSO.basicSpeed;
        lastMissile = Time.time;
    }

    void Update()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = mainCam.transform.position.y;
        var pos = mainCam.ScreenToWorldPoint(mousePos);

        if (pos.x < -4.75f) pos.x = -4.75f;
        if (pos.x >= 4.75f) pos.x = 4.75f;
        if (pos.z <= -18f) pos.z = -18f;
        if (pos.z >= 1.75f) pos.z = 1.75f;

        transform.position = Vector3.Lerp(transform.position, pos, speed);

        // launch a missile
        var now = Time.time;
        if (now - lastMissile >= missileInterval)
        {
            if (!isPowerUp)
            {
                Instantiate(missile, muzzles[muzzleIndex++].position, Quaternion.identity);
            }
            else
            {
                foreach (var muzzle in muzzles)
                {
                    Instantiate(missile, muzzle.position, Quaternion.identity);
                }
            }
            lastMissile = now;
            if (muzzleIndex >= muzzles.Length)
                muzzleIndex = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var aircraft = collision.gameObject.GetComponent<Aircraft>();

        if (aircraft && aircraft.isEnemy)
        {
            GameOver(aircraft);
        }
    }

    private void GameOver(Aircraft aircraft)
    {
        Destroy(gameObject);

        GetComponent<Aircraft>().aircraftSO.CastExplosion(transform.position);

        aircraft.Damage(1);

        GameManager.Instance.ReadyForRestart();

        mainCam.GetComponent<AudioSource>().Stop();
    }

    public void PowerUp()
    {
        StartCoroutine(PowerUping());
    }

    IEnumerator PowerUping()
    {
        isPowerUp = true;
        yield return new WaitForSeconds(10);
        isPowerUp = false;
    }
}
