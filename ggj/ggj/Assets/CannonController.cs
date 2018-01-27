using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonController : BaseShipController {
    
    public float firePower = 1000;

    public Transform projectileSpawnTransform;

    public Projectile templateProjectile;

	public int loadedAmmo = 0;

    ProjectilePoolManager poolManager;

	public GameObject NoAmmo;

	public AudioSource cannonFire;

	public GameObject aimAid;

    void Start()
    {
        poolManager = new ProjectilePoolManager(5);
        poolManager.Setup();
    }
		

    public void Fire()
    {
        if (loadedAmmo > 0)
        {
            Projectile p = poolManager.AddProjectile(templateProjectile);

            p.transform.position = projectileSpawnTransform.position;
            p.transform.rotation = projectileSpawnTransform.rotation;
            p.transform.localScale = Vector3.one;

            p.SetVelocity(firePower * projectileSpawnTransform.forward);

            loadedAmmo--;
            if (cannonFire != null)
            {
                cannonFire.Play();
            }
        }
        else
        {
            //StartCoroutine(OutOfAmmo());
        }
    }

    public override void ActionButton()
    {
        base.ActionButton();

        Fire();
    }

	//public void OutOfAmmo () {
	//	NoAmmo.SetActive (true);

	//}

	void OnTriggerEnter(Collider other)
	{
		RPlayerController player = other.GetComponent<RPlayerController>();
		if (player != null)
        {
            //Debug.Log("player");
            if (player.IsCarryingItem)
            {
                //Debug.Log("Carrying CarryingItem");

				if (player.CarryingItem is CannonBallItem) {
					if (loadedAmmo < 1) {
						//Debug.Log ("Carrying ball");
						CarryItem item = player.CarryingItem;
						player.DropItem (item);
						Destroy (item.gameObject);
						loadedAmmo++;

					}
				}
			}	
		}
	}
//	void OnTriggerStay(Collider other)
//	{
//		RPlayerController player = other.GetComponent<RPlayerController>();
//		if (player != null) {
//			if (loadedAmmo > 0) {
//				aimAid.SetActive (true);
//			} else {
//				aimAid.SetActive (false);
//
//		}
//	}

	IEnumerator OutOfAmmo () {
		//NoAmmo.SetActive (true);

		yield return new WaitForSeconds(1f);
		//NoAmmo.SetActive (false);
	}

    protected override void Update()
    {
        base.Update();

        if (aimAid != null)
        {
            if (loadedAmmo > 0)
            {
                aimAid.SetActive(true);
            }
            else
            {
                aimAid.SetActive(false);
            }
        }
    }
}
