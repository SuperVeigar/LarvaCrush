using UnityEngine;
using System.Collections.Generic;

namespace SuperVeigar
{
    public enum ItemType
    {
        Tail = 0,
    }

    public class ItemService : SingletonBehaviour<ItemService>
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject itemTail;

        private List<Item> itemTails = new List<Item>();

        public Item GetAvailableItem(ItemType type)
        {
            List<Item> list = null;

            switch (type)
            {
                case ItemType.Tail:
                    list = itemTails;
                    break;
            }

            if (list == null)
            {
                return null;
            }

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].gameObject.activeInHierarchy == false)
                {
                    return list[i];
                }
            }

            Item item = Instantiate(GetPrefab(type), transform).GetComponent<Item>();
            list.Add(item);
            return item;
        }

        private GameObject GetPrefab(ItemType type)
        {
            switch (type)
            {
                case ItemType.Tail:
                    return itemTail;
                default:
                    return null;
            }
        }
    }
}