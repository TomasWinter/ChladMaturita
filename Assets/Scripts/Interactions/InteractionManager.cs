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
            if (Physics.Raycast(
                transform.parent.position + new Vector3(0,0.5f,0), 
                transform.forward, out RaycastHit hit, distance) && 
                (interactable = hit.collider.
                GetComponent<Interactable>()) != null)
            {
                if (currentInteractable != interactable && 
                    interactable.IsEnabled)
                {
                    currentInteractable = interactable;
                    ShowGUI(hit,interactable);
                    currentInteractable.SetPointing(true);
                }
                else if (currentInteractable != null &&
                    !currentInteractable.IsEnabled)
                    ExitInteract();
            }
            else
                ExitInteract();
        }
        else if (currentInteractable != null && 
            (!currentInteractable.IsEnabled || 
            (currentInteractable.transform.position - 
            transform.parent.position)
            .magnitude > distance))
            ExitInteract();

        if (currentInteractable != null && 
            Input.GetKey(Settings.Keybinds.Interact) && 
            currentInteractable.IsEnabled)
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
                PlayerGuiManager.Instance.UpdateInteractBar(true, 
                    currentTime/currentInteractable.InteractionTime);
        }
        else if (forceStay)
        {
            currentInteractable.IsInteracting(false);
            currentTime = 0;
            forceStay = false;
            PlayerGuiManager.Instance.UpdateInteractBar(false, 0);
        }
    }

    private void ShowGUI(RaycastHit hit, Interactable i)
    {
        string GuiText = 
            (i.InteractionTime > 0 ? "Hold" : "Press") + 
            " # to "
            + i.InteractionText;
        PlayerGuiManager.Instance.DisplayInteract(true, 
            GuiText, 
            Settings.Keybinds.Interact);
    }

    private void HideGui()
    {
        PlayerGuiManager.Instance.DisplayInteract(false);
        PlayerGuiManager.Instance.UpdateInteractBar(false, 0);
    }

    private void ExitInteract()
    {
        if (currentInteractable != null)
        {
            currentInteractable.IsInteracting(false);
            forceStay = false;
            currentTime = 0;
            currentInteractable.SetPointing(false);
            currentInteractable = null;
            HideGui();
        }
        
    }
}
