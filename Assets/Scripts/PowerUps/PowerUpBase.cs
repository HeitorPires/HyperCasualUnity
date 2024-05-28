using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : CollectableItemBase
{
    [Header("Power Up")]
    public float duration = 2.5f;

    protected override void OnCollect()
    {
        base.OnCollect();
        StartPowerUp();
    }

    protected virtual void StartPowerUp()
    {
        Debug.Log("Start Power Up");
        Invoke(nameof(EndPowerUp), duration);
    }

    protected virtual void EndPowerUp()
    {
        Debug.Log("End Power Up");
    }

}
