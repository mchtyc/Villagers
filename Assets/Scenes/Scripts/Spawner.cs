using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabs;
    [SerializeField] List<Transform> boundaries;
    [SerializeField] Transform itemContainer;

    [SerializeField] int itemCountToSpawn;

    public void Spawn()
    {
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        for(int i = 0; i < itemCountToSpawn; i++)
        {
            Vector3 random_pos = new Vector3(Random.Range(boundaries[0].position.x, boundaries[1].position.x), 
                                       0.2f, Random.Range(boundaries[0].position.z, boundaries[1].position.z));

            Instantiate(prefabs[Random.Range(0, prefabs.Count)], random_pos, Quaternion.identity, itemContainer);

            yield return new WaitForSeconds(0.2f);
        }
    }

}
