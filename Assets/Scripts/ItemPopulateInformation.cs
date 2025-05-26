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
        ability.text = item.ability == "" ? "" : "Ability: " + item.ability;
        GetComponent<MeshRenderer>().material.color = StringToColor(item.rarity);
    }

    private Color StringToColor(string color)
    {
        switch (color)
        {
            case "white": return Color.white;
            case "green": return Color.green;
            case "blue": return Color.blue;
            case "purple": return new Color(0.615f, 0f, 1f);
            case "orange": return new Color(1f, 0.674f, 0.11f);
            default: return Color.white;
        }
    }
}
