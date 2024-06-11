using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableObjects : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject attachedObject;
    public void OnPointerClick(PointerEventData eventData)
    {
        Show(); 
    }

    private void Show()
    {
        this.gameObject.SetActive(false);
        attachedObject.SetActive(true);
    }
    
}
