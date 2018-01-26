using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthbar : UITrackedObject
{
    public Image barImage;
    public Image backgroundParent;
    public float hideAfterSeconds = 9999;

    HealthController health;
    float timeSinceDamaged = 0;

    public void Setup (HealthController health)
    {
        this.health = health;

        health.onDamage += OnDamageChanged;
        health.onDeath += OnDeath;

        OnDamageChanged(0);
        Track(health.transform);
    }

    private void OnDeath()
    {
        if (this != null)
        {
            Destroy(this.gameObject);

            health.onDamage -= OnDamageChanged;
            health.onDeath -= OnDeath;
        }
    }

    private void OnDamageChanged(float damage)
    {
        barImage.fillAmount = health.Health / health.HealthMax;
        timeSinceDamaged = 0;
        backgroundParent.gameObject.SetActive(true);
    }

    void Update()
    {
        if (timeSinceDamaged > hideAfterSeconds)
        {
            backgroundParent.gameObject.SetActive(false);
        }
        else
        {
            timeSinceDamaged += Time.deltaTime;
        }
    }
}
