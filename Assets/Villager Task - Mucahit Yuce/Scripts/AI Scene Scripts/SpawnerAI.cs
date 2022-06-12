using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAI : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabs;
    [SerializeField] List<Transform> boundaries;
    [SerializeField] Transform itemContainer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        yield return new WaitForSeconds(5f);
        Spawn();

        while (true)
        {
            yield return new WaitForSeconds(10f);
            Spawn();
        }
    }

    // SpawnerAI generates random items automatically at intervals
    public void Spawn()
    {
        Vector3 random_pos = new Vector3(Random.Range(boundaries[0].position.x, boundaries[1].position.x),
                                   0.2f, Random.Range(boundaries[0].position.z, boundaries[1].position.z));

        Instantiate(prefabs[Random.Range(0, prefabs.Count)], random_pos, Quaternion.identity, itemContainer);
    }
}
