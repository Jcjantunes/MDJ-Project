using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class PowerupEffect : ScriptableObject
{
    public float powerUpDuration = 5f;
    public GameObject powerUpVar;
    public GameObject targetVar; 

    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public Sprite originalSprite;

    public static event Action OnPowerup;
    public static event Action OnPowerdown;

    public IEnumerator Apply(GameObject target, GameObject powerUp) {
        powerUpVar = powerUp;
        targetVar = target;

        spriteRenderer = targetVar.GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
        newSprite = Resources.Load<Sprite>("pixil-frame-0 (8)");

        EnablePowerup();
        yield return new WaitForSeconds(powerUpDuration);
        DisablePowerup();
        DestroyPowerup();
    }

    public virtual void EnablePowerup() {
        OnPowerup?.Invoke();
        spriteRenderer.sprite = newSprite;
        targetVar.transform.Find("PowerupEffect").gameObject.SetActive(true);
    }
    public virtual void DisablePowerup() {
        OnPowerdown?.Invoke();
        spriteRenderer.sprite = originalSprite;
        targetVar.transform.Find("PowerupEffect").gameObject.SetActive(false);
    }
    public void DestroyPowerup() {
        Destroy(powerUpVar);
    }
}
