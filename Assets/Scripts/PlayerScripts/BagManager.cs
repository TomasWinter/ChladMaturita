using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    public static BagManager Instance { get; private set; }

    [SerializeField] Rigidbody rigidBody;
    [SerializeField] GameObject bagPrefab;
    [SerializeField] float throwForce = 3;

    BagInfo currentBag = null;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (currentBag != null && Input.GetKeyDown(Settings.Keybinds.ThrowBag))
        {
            GameObject bag = Instantiate(bagPrefab,transform.position,transform.rotation);
            bag.GetComponent<LootBag>().SetInfo(currentBag);

            Vector3 bagForce = (transform.forward + transform.up/3) * throwForce * (2/currentBag.Weight) + rigidBody.velocity;
            bag.GetComponent<Rigidbody>().AddForce(bagForce, ForceMode.Impulse);

            PlrMovementScript.Instance.RemoveSpeedModifier(1 / currentBag.Weight);

            currentBag = null;
            PlayerGuiManager.Instance.SetBag(null);
        }
    }

    public bool SetBag(BagInfo bagInfo)
    {
        if (currentBag == null)
        {
            currentBag = bagInfo;
            PlrMovementScript.Instance.AddSpeedModifier(1 / currentBag.Weight);
            PlayerGuiManager.Instance.SetBag(bagInfo);
            return true;
        }
        else
        {
            return false;
        }
    }
}
