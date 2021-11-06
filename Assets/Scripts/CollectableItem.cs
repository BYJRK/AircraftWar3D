using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    Fire
}

public class CollectableItem : MonoBehaviour
{
    public AudioClip sfx;
    public PowerUpType type;
    public float speed;

    public void PlaySFX()
    {
        if (sfx)
        {
            AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position, 2f);
        }
    }

    private void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;

        if (transform.position.z < -22)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hero = collision.gameObject.GetComponent<HeroController>();

        if (hero)
        {
            hero.PowerUp();
            PlaySFX();
            Destroy(gameObject);
        }
    }
}
