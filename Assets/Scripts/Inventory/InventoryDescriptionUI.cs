using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDescriptionUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;

    private void Awake()
    {
        RestDescription();
    }

    public void RestDescription()
    {
        itemImage.gameObject.SetActive(false);
        titleText.text = "";
        descriptionText.text = "";
    }
    
    public void SetDescription(Sprite itemSprite, string title, string description)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = itemSprite;
        titleText.text = title;
        descriptionText.text = description;
    }
}
