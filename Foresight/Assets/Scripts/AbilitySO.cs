using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability", fileName = "NewAbility")]
public class AbilitySO : ScriptableObject
{
    [SerializeField] string abilityName;
    [SerializeField] int abilityIndex;

    public int GetAbilityIndex()
    {
        return abilityIndex;
    }

    public string GetAbilityName()
    {
        return abilityName;
    }
    
}
