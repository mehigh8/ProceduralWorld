using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPopulateInformation : MonoBehaviour
{
    public TMP_Text type;
    public TMP_Text damageDefence;
    public TMP_Text durability;
    public TMP_Text ability;

    public void Populate(NPCItemGenerator.Item item)
    {
        type.text = "<color=" + item.rarity + ">" + item.type.type + "</color>";
        damageDefence.text = (item.type.isArmor ? "Defence: " : "Damage ") + item.damageDefence;
        durability.text = "Durability: " + item.durability;
        ability.text = "Ability: " + item.ability;
    }
}
