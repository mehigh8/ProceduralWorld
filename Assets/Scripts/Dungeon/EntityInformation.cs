using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntityInformation : MonoBehaviour
{
    public enum EntityType
    {
        Trap = 0,
        GoldChest = 1,
        ItemChest = 2,
        Enemy = 3
    }

    public float range;

    public EntityType entityType;

    public int gold;
    public NPCItemGenerator.Item item;
    public NPCItemGenerator.NPC enemy;

    private GameObject canvas;

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && Vector3.Distance(transform.position, player.transform.position) < range)
            DisplayInformation();
    }

    public void LoadGold(int gold)
    {
        this.gold = gold;
        entityType = EntityType.GoldChest;
    }

    public void LoadItem(NPCItemGenerator.Item item)
    {
        this.item = item;
        entityType = EntityType.ItemChest;
    }

    public void LoadEnemy(NPCItemGenerator.NPC enemy)
    {
        this.enemy = enemy;
        entityType = EntityType.Enemy;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void DisplayInformation()
    {
        TMP_Text[] texts = canvas.GetComponentsInChildren<TMP_Text>();
        foreach (var text in texts)
            text.text = string.Empty;

        switch (entityType)
        {
            case EntityType.Trap:
                texts[0].text = "Trap";
                break;
            case EntityType.GoldChest:
                texts[0].text = "Gold: " + gold;
                break;
            case EntityType.ItemChest:
                texts[0].text = "<color=" + item.rarity + ">" + item.type.type + "</color>";
                texts[1].text = (item.type.isArmor ? "Defence: " : "Damage ") + item.damageDefence;
                texts[2].text = "Durability: " + item.durability;
                texts[3].text = item.ability == "" ? "" : "Ability: " + item.ability;
                break;
            case EntityType.Enemy:
                texts[0].text = enemy.name;
                texts[1].text = enemy.npcClass.className;
                texts[2].text = "Health: " + enemy.health;
                texts[3].text = "Damage: " + enemy.damage;
                break;
        }
    }
}
