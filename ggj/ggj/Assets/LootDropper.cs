using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropper : MonoBehaviour {

    public List<LootItem> lootItems;
    public int minLootDropped = 1;
    public int maxLootDropped = 10;

    public void DropLoot(Vector3 originOfLootDrop)
    {
        int lootCount = UnityEngine.Random.Range(minLootDropped, maxLootDropped);
        for (int i = 0; i < lootCount; i++)
        {
            LootItem li = Instantiate(lootItems[UnityEngine.Random.Range(0, lootItems.Count)]);

            Vector3 randomMovement = UnityEngine.Random.onUnitSphere * 60;
            li.transform.position =  new Vector3(originOfLootDrop.x + randomMovement.x, 0, originOfLootDrop.z + randomMovement.z);
            li.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 350), 0);
        }
    }
}
