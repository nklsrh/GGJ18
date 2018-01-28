using UnityEngine;
using System.Collections;

public class ExplosionObject : BaseObject
{
    [SerializeField]
    ParticleSystem[] particleSystems;

    [SerializeField]
    AudioSource audioExplosion;

    public override void Setup()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Play();
			StartCoroutine (Explostion (particleSystems[i].startDelay));

        }


       // audioExplosion.Play();
    }

	IEnumerator Explostion (float delay) {

		yield return new WaitForSeconds (delay);
		audioExplosion.Play();

	}


    public override void Logic()
    {
    }
}
