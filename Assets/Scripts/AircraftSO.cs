using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Aircraft", menuName = "Custom/New Aircraft")]
public class AircraftSO : ScriptableObject
{
    public int hp;
    public float basicSpeed;

    public int score;

    public GameObject explosion;
    public float explosionScale = 1f;

    public AudioClip explosionSFX;

    public void CastExplosion(Vector3 position)
    {
        if (!explosion)
            return;

        var exp = Instantiate(explosion, position, Quaternion.identity);
        exp.transform.localScale = Vector3.one * explosionScale;
        Destroy(exp, exp.GetComponent<ParticleSystem>().main.duration);

        if (explosionSFX)
        {
            AudioSource.PlayClipAtPoint(explosionSFX, position);
        }
    }
}
