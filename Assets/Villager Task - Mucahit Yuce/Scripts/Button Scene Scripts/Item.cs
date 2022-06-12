using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ItemData data;
    [SerializeField] Transform collectArea;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.color = data.color;
        if(GameObject.FindGameObjectWithTag(data.tag))
            collectArea = GameObject.FindGameObjectWithTag(data.tag).transform;

        transform.localScale = Vector3.one * 0.5f;
        StartCoroutine(SpawningAnimation());
    }

    // Little grow animation of item when the first time it instantiated
    IEnumerator SpawningAnimation()
    {
        for (int i = 1; i < 20; i++)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 1, i / 10f), transform.localScale.y,
                                               Mathf.Lerp(transform.localScale.z, 1, i / 10f));

            yield return null;
        }
    }

    // Calculates to tell character where exactly to put item in its collection area 
    public Vector3 CalculateDestination()
    {
        int item_count_in_area = collectArea.childCount - 1;
        Vector3 local_pos = new Vector3(((item_count_in_area % 4) * 1.25f) + 0.625f, 
                                        ((item_count_in_area / 16) * 0.6f) + 0.3f, 
                                       (-((item_count_in_area / 4) % 4) * 1.25f) - 0.625f);

        return local_pos + collectArea.GetChild(0).position;
    }

    // Character script needs it when it put the item into its collection area to make parenting relation 
    public Transform GetCollectionArea() => collectArea;

    // Required in CharacterAI
    public Types GetType() => data.type;
}
