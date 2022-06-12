using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabs;
    [SerializeField] List<Transform> boundaries;
    [SerializeField] Transform itemContainer;
    [SerializeField] GameObject hammer;
    
    ButtonInteractions btnInteractions;

    void Start() => btnInteractions = GetComponent<ButtonInteractions>();

    // Spawner generates random items which has different colors at random locations in between boundaries
    public void SpawnBtnClicked()
    {
        HandleBtnInteractions();

        Vector3 random_pos = new Vector3(Random.Range(boundaries[0].position.x, boundaries[1].position.x),
                                   0.2f, Random.Range(boundaries[0].position.z, boundaries[1].position.z));

        Instantiate(prefabs[Random.Range(0, prefabs.Count)], random_pos, Quaternion.identity, itemContainer);
    }

    void HandleBtnInteractions()
    {
        if (hammer.activeSelf)
        {
            btnInteractions.EnableHitBtn();
            btnInteractions.DisableHammerBtn();
        }
        else
        {
            btnInteractions.EnableHammerBtn();
            btnInteractions.DisableHitBtn();
        }
        
        btnInteractions.EnablePickBtn();
        btnInteractions.DisableGatherBtn();
        btnInteractions.DisableSpawnBtn();
    }
}
