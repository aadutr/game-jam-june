using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        // For when new equipment can be picked up
        // EquipmentManager.instance.onEquipmentChanged += onEquipmentChanged;
    }

    public override void Die ()
    {
        base.Die();
        // Kill the player in some way
        PlayerManager.instance.KillPlayer();
    }
}
