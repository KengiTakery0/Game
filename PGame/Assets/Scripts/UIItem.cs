using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ItemType
{
    none,
    bread,
    cheese,
    pepperoni,
    letter,
    Buter
}

public class UIItem : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] public ItemType itemType;
    [SerializeField] private bool isRequared;
    private Canvas canvas;
    private RectTransform m_RectTransform;
    private MainManager mainManager;
    public bool InInventory;
    [SerializeField] GameObject Letter;
    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        mainManager = FindObjectOfType<MainManager>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

    }
    public void OnDrag(PointerEventData eventData)
    {
        if (itemType == ItemType.letter) return;
       m_RectTransform.anchoredPosition += eventData.delta/ canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
      
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isRequared) return;
        if(InInventory && itemType == ItemType.letter)
        {
            Letter.gameObject.SetActive(true);
        }
        else
        mainManager.AddToInventory(this.gameObject);
    }
}
