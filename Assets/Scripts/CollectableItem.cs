using UnityEngine;

public enum PowerUpType
{
    Fire,
    Bomb,
    Shield
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
        if (collision.gameObject.CompareTag("Player"))
        {
            var hero = collision.gameObject.GetComponent<HeroController>();

            switch (type)
            {
                case PowerUpType.Fire:
                    hero.PowerUp();
                    break;
                case PowerUpType.Bomb:
                    GameManager.Instance.bombCount++;
                    break;
            }

            PlaySFX();
            Destroy(gameObject);
        }
    }
}
