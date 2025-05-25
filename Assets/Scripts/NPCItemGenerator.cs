using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCItemGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct Class
    {
        public string className;
        public Sprite classSprite;
    }
    [System.Serializable]
    public struct ItemType
    {
        public string type;
        public bool isArmor;
    }

    [System.Serializable]
    public class NPC
    {
        public string name;
        public Class npcClass;
        public int damage;
        public int health;
        public string personality;
    }
    [System.Serializable]
    public class Item
    {
        public ItemType type;
        public int damageDefence;
        public int durability;
        public string ability;
        public string rarity;
    }

    [Header("NPC")]
    public List<string> names;
    public List<Class> classes;
    public Vector2Int damage;
    public Vector2Int health;
    public List<string> personalities;

    [Header("Item")]
    public List<ItemType> types;
    public Vector2Int damageDefence;
    public Vector2Int durability;
    public List<string> abilities;
    public List<string> rarities;
    public int rarityThreshold;

    private static NPCItemGenerator instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public NPC GenerateNPC()
    {
        NPC npc = new NPC();
        npc.name = names[Random.Range(0, names.Count)];
        npc.npcClass = classes[Random.Range(0, classes.Count)];
        npc.personality = personalities[Random.Range(0, personalities.Count)];
        npc.damage = Random.Range(damage.x, damage.y);
        npc.health = Random.Range(health.x, health.y);
        return npc;
    }

    public Item GenerateItem()
    {
        Item item = new Item();
        int rarity = Random.Range(0, rarities.Count);

        item.type = types[Random.Range(0, types.Count)];
        item.damageDefence = Random.Range(damageDefence.x, damageDefence.y);
        item.durability = Random.Range(durability.x, durability.y);

        if (rarity > rarityThreshold)
            item.ability = abilities[Random.Range(0, abilities.Count)];

        item.rarity = rarities[rarity];

        return item;
    }

    public static NPCItemGenerator GetInstance() => instance;
}
