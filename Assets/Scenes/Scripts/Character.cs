using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Transform itemContainer, itemRoot;
    [SerializeField] float speed = 1f;


    Animator animator;
    bool isRunning;
    Transform currentTarget;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isRunning)
            Run();
    }

    void Run()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if(Vector3.Distance(transform.position, currentTarget.position) <= 2f)
        {
            PickUp();
            isRunning = false;
        }
    }

    void PickUp()
    {
        animator.SetTrigger("pickup");

        StartCoroutine(PickingUp());
    }

    IEnumerator PickingUp()
    {
        yield return new WaitForSeconds(1f);

        currentTarget.parent = itemRoot;
        currentTarget.localPosition = Vector3.zero;
        currentTarget.rotation = itemRoot.rotation;
    }

    public void OnPickupClicked()
    {
        if(itemContainer.childCount > 0)
        {
            currentTarget = itemContainer.GetChild(0);
            StartCoroutine(FacingTarget(currentTarget.position));
            
            isRunning = true;
            animator.SetTrigger("run");
        }
    }

    IEnumerator FacingTarget(Vector3 _target)
    {
        Quaternion new_rot = Quaternion.LookRotation(_target - transform.position, transform.up);

        for (int i = 1; i < 21; i++)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, new_rot, i / 20);

            yield return new WaitForSeconds(0.01f);
        }
    }
}
