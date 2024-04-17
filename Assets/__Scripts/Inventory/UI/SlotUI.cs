using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemAmount;
    [SerializeField] private Slot assignedSlot;

    private Button button;
    public Slot AssignedSlot => assignedSlot;
    public InventoryDisplay ParentDisplay { get; private set; }
    
    private void Awake()
    {
        ClearSlotUI();
        
        button = GetComponent<Button>();
        button.onClick.AddListener(OnSlotClicked);
        
        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }
    
    public void ClearSlotUI()
    {
        assignedSlot?.ClearSlot();
        itemIcon.sprite = null;
        itemIcon.color = Color.clear;
        itemAmount.text = "";
    }
    
    public void AssignSlot(Slot slot)   // Start
    {
        assignedSlot = slot;
        
    }
    
    public void SetSlotUI(Slot slot)
    {
        if (slot.Item != null)
        {
            itemIcon.sprite = slot.Item.icon;
            itemIcon.color = Color.white;
            itemAmount.text = slot.Item.stackable ? slot.StackAmount.ToString() : "";
        }
        else
        {
            itemIcon.sprite = null;
            itemIcon.color = Color.clear;
            itemAmount.text = "";
        }
        
        
    }
    
    public void SetSlotUI()
    {
        if (assignedSlot.Item != null) SetSlotUI(assignedSlot);
        // else ClearSlot();
    }
    
    
    public void OnSlotClicked()
    {
        ParentDisplay?.SlotClicked(this);
    }
}
