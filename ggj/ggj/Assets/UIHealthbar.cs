using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthbar : UITrackedObject
{
    public Image barImage;
    public Image backgroundParent;
    public Image warningIcon;
    public float hideAfterSeconds = 9999;
    public float maxHealthFillAmount = 0.46f;

    HealthController health;
    float timeSinceDamaged = 0;

    public void Setup (HealthController health)
    {
        this.health = health;

        health.onDamage += OnDamageChanged;
        health.onRegen += OnDamageChanged;
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
            health.onRegen -= OnDamageChanged;
        }
    }

    private void OnDamageChanged(float damage)
    {
        float healthP = health.Health / health.HealthMax;
        barImage.fillAmount = maxHealthFillAmount * healthP;
        timeSinceDamaged = 0;
        backgroundParent.gameObject.SetActive(true);
        if (warningIcon)
        {
            warningIcon.gameObject.SetActive(healthP < 0.5f);
        }
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
