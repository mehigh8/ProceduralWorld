using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCPopulateInformation : MonoBehaviour
{
    public TMP_Text name;
    public TMP_Text classText;
    public Image clasImg;
    public TMP_Text personality;
    public TMP_Text damage;
    public TMP_Text health;
    
    public void Populate(NPCItemGenerator.NPC npc)
    {
        name.text = npc.name;
        classText.text = npc.npcClass.className;
        clasImg.sprite = npc.npcClass.classSprite;
        personality.text = npc.personality;
        damage.text = "Damage: " + npc.damage;
        health.text = "Health: " + npc.health;
    }
}
