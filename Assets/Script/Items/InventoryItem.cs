using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TMP_Text quantity;
    [SerializeField]
    private Image borderImg;

    public event Action<InventoryItem> OnItemClicked;
    public event Action<InventoryItem> OnRightMouseBtnClick;

    private bool empty = true;

    public void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        empty = true;
    }

    public void Deselect()
    {
        borderImg.enabled = false;
    }

    public void SetData(Sprite sprite, int quantity)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.quantity.text = quantity.ToString();
        empty = false;
    }

    public void Select()
    {
        borderImg.enabled = true;
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }
}
