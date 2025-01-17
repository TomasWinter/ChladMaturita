using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }

    [SerializeField] float distance = 2;
    [SerializeField] GameObject pointer;

    bool forceStay = false;
    float currentTime = 0;
    Interactable currentInteractable;
    GameObject currentPointer;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (!forceStay)
        {
            Interactable interactable;
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distance) && (interactable = hit.collider.GetComponent<Interactable>()) != null)
            {
                if (currentInteractable != interactable && interactable.IsEnabled)
                {
                    currentInteractable = interactable;
                    ShowGUI(ref hit,ref interactable);
                    currentInteractable.SetPointing(true);
                }
                else if (currentInteractable != null && !currentInteractable.IsEnabled)
                    ExitInteract();
            }
            else
                ExitInteract();
        }
        else if (currentInteractable != null && !currentInteractable.IsEnabled)
            ExitInteract();

        if (currentInteractable != null && Input.GetKey(Settings.Instance.Keybinds.Interact) && currentInteractable.IsEnabled)
        {
            if (currentTime == 0)
                currentInteractable.IsInteracting(true);
            forceStay = true;
            currentTime += Time.deltaTime;
            if (currentTime >= currentInteractable.InteractionTime)
            {
                currentInteractable.Interacted(true);
                currentTime = 0;
            }
            else
                PlayerGuiManager.Instance.UpdateInteractBar(true, currentTime/currentInteractable.InteractionTime);
        }
        else if (forceStay)
        {
            currentInteractable.IsInteracting(false);
            currentTime = 0;
            forceStay = false;
            PlayerGuiManager.Instance.UpdateInteractBar(false, 0);
        }
    }

    private void ShowGUI(ref RaycastHit hit, ref Interactable i)
    {
        //currentPointer = Instantiate(pointer, hit.transform.position + new Vector3(0,0.3f,0), Quaternion.identity);

        string GuiText = (i.InteractionTime > 0 ? "Hold" : "Press") + " # to " + i.InteractionText;

        PlayerGuiManager.Instance.DisplayInteract(true, GuiText, Settings.Instance.Keybinds.Interact);
    }

    private void HideGui()
    {
        PlayerGuiManager.Instance.DisplayInteract(false);
        PlayerGuiManager.Instance.UpdateInteractBar(false, 0);
        //Destroy(currentPointer);
    }

    private void ExitInteract()
    {
        if (currentInteractable != null)
        {
            currentInteractable.IsInteracting(false);
            forceStay = false;
            currentInteractable.SetPointing(false);
            currentInteractable = null;
            HideGui();
        }
        
    }
}
