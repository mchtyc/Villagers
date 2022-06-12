using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteractions : MonoBehaviour
{
    [SerializeField] Button SpawnBtn;
    [SerializeField] Button PickItemBtn;
    [SerializeField] Button GatherItemBtn;
    [SerializeField] Button GetHammerBtn;
    [SerializeField] Button HitItemBtn;

    // Start is called before the first frame update
    void Start()
    {
        DisablePickBtn();
        DisableGatherBtn();
        DisableHammerBtn();
        DisableHitBtn();
    }

    // Enabling buttons
    public void EnableSpawnBtn() => Enable(SpawnBtn);
    public void EnablePickBtn() => Enable(PickItemBtn);
    public void EnableGatherBtn() => Enable(GatherItemBtn);
    public void EnableHammerBtn() => Enable(GetHammerBtn);
    public void EnableHitBtn() => Enable(HitItemBtn);

    // Disabling buttons
    public void DisableSpawnBtn() => Disable(SpawnBtn);
    public void DisablePickBtn() => Disable(PickItemBtn);
    public void DisableGatherBtn() => Disable(GatherItemBtn);
    public void DisableHammerBtn() => Disable(GetHammerBtn);
    public void DisableHitBtn() => Disable(HitItemBtn);
    public void DisableAllBtns()
    {
        DisableSpawnBtn();
        DisablePickBtn();
        DisableGatherBtn();
        DisableHammerBtn();
        DisableHitBtn();
    }

    public void Disable(Button _btn)
    {
        if (_btn.interactable)
        {
            _btn.interactable = false;

            StartCoroutine(Disabling(_btn.transform));
        }
    }

    IEnumerator Disabling(Transform _btn)
    {
        float disabled_scale = 0.8f;

        for(int i = 1; i < 11; i++)
        {
            _btn.localScale = Vector3.Lerp(_btn.localScale, Vector3.one * disabled_scale, i / 10f);

            yield return new WaitForSeconds(0.05f);
        }
    }

    public void Enable(Button _btn)
    {
        if (!_btn.interactable)
        {
            StartCoroutine(Enabling(_btn.transform));
            _btn.interactable = true;
        }
    }

    IEnumerator Enabling(Transform _btn)
    {
        float enabled_scale = 1f;

        for (int i = 1; i < 11; i++)
        {
            _btn.localScale = Vector3.Lerp(_btn.localScale, Vector3.one * enabled_scale, i / 10f);

            yield return new WaitForSeconds(0.05f);
        }
    }
}
