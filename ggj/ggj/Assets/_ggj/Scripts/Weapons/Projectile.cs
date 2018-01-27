using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : BaseObject
{
    public float lifetime = 1.0f;
    public float damage;
    public List<AudioSource> audioBlastOptions;

    public ExplosionObject explosion;
    public Vector3 Velocity
    {
        get
        {
            return rig.velocity;
        }
    }

    protected System.Action<Projectile> onDeath;

    private float currentLifetime = 0;
    private Rigidbody rig;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    public override void Setup()
    {
        gameObject.SetActive(true);
        currentLifetime = lifetime;
    }

    public virtual void SetupProjectile(System.Action<Projectile> onDeath)
    {
        this.onDeath = onDeath;
        this.rig = GetComponent<Rigidbody>();
        gameObject.SetActive(true);

        PlayRandomBlastSound();

        Setup();
    }

    void Update()
    {
        currentLifetime -= Time.deltaTime;
        if (currentLifetime <= 0)
        {
            Die();
        }
    }

    public virtual void SetVelocity(Vector3 velocity)
    {
        rig.velocity = velocity;
    }
    public virtual void Shoot(Vector3 force)
    {
        SetVelocity(Vector3.zero);
        rig.AddForce(force, ForceMode.Impulse);
    }


    public virtual void Die()
    {
        gameObject.SetActive(false);
        if (onDeath != null)
        {
            onDeath.Invoke(this);
        }
    }

    private void PlayRandomBlastSound()
    {
        if (audioBlastOptions.Count > 0)
        {
            for (int i = 0; i < audioBlastOptions.Count; i++)
            {
                audioBlastOptions[i].gameObject.SetActive(false);
            }
            audioBlastOptions[Random.Range(0, audioBlastOptions.Count)].gameObject.SetActive(true);
        }
    }

    public void HitSomething()
    {
        if (explosion != null)
        {
            explosion.Setup();
        }
        Die();
    }
}
