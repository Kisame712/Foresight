using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySelector : MonoBehaviour
{
    Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    public void SetSelectedIndex(int index)
    {
        player.ability = index;
        player.isAbilitySelected = true;
    }
    
    public void ActivateNullifyEffect()
    {
        player.nullifyEffect.SetActive(true);
    }

    public void ActivateLightParticle()
    {
        player.lightParticle.SetActive(true);
    }
}
