using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventioryCell : MonoBehaviour
{
    public bool isTaken;
    public UIItem item;
    public void Add(GameObject item)
    {
        item.GetComponent<RectTransform>().SetParent(this.GetComponent<RectTransform>());
        item.GetComponent<RectTransform>().localPosition = Vector3.zero;
        this.item = item.GetComponent<UIItem>();
        
        item.GetComponent<RectTransform>().localScale = Vector3.one;
        item.GetComponent<RectTransform>().localEulerAngles = Vector3.zero;
        item.GetComponent<Image>().color = Color.white;
        if (item.GetComponent<UIItem>().itemType != ItemType.letter)
        item.GetComponent<UIItem>().enabled= false;
        item.GetComponent<UIItem>().InInventory= true;
        isTaken = true;
    }
    public void Remove()
    {
       Destroy(this.GetComponent<RectTransform>().GetChild(0).gameObject);
        isTaken = false;
    }

}
