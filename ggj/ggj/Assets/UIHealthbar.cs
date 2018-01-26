using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthbar : MonoBehaviour
{
    public Image barImage;
    public Vector3 offset;

    RPlayerController myPlayer;
    float timeSinceDamaged = 0;

    public void CreateForPlayer (RPlayerController player)
    {
        myPlayer = player;

        myPlayer.health.onDamage += OnDamageChanged;
        myPlayer.health.onDeath += OnDeath;
    }

    private void OnDeath()
    {
        if (this != null)
        {
            Destroy(this.gameObject);

            myPlayer.health.onDamage -= OnDamageChanged;
            myPlayer.health.onDeath -= OnDeath;
        }
    }

    private void OnDamageChanged(float damage)
    {
        barImage.fillAmount = myPlayer.health.Health / myPlayer.health.HealthMax;
        timeSinceDamaged = 0;
    }

    void Update()
    {
        if (myPlayer != null)
        {
            transform.position = myPlayer.transform.position + offset;
        }

        if (timeSinceDamaged > 3.0f)
        {
            transform.gameObject.SetActive(false);
        }
        else
        {
            timeSinceDamaged += Time.deltaTime;
        }
    }
}
