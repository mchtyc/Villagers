using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.color = data.color;
        transform.localScale = Vector3.one * 0.5f;
        StartCoroutine(SpawningAnimation());
    }

    IEnumerator SpawningAnimation()
    {
        for (int i = 1; i < 20; i++)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 1, i / 10f), transform.localScale.y,
                                               Mathf.Lerp(transform.localScale.z, 1, i / 10f));

            yield return null;
        }
    }
}
