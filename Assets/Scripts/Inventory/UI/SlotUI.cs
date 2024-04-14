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
        ClearSlot();
        
        button = GetComponent<Button>();
        button.onClick.AddListener(OnSlotClicked);
        
        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }
    
    public void Init(Slot slot)
    {
        assignedSlot = slot;
        
    }
    
    public void UpdateSlotUI(Slot slot)
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
    
    public void UpdateSlotUI()
    {
        if (assignedSlot.Item != null) UpdateSlotUI(assignedSlot);
        // else ClearSlot();
    }

    public void ClearSlot()
    {
        assignedSlot?.ClearSlot();
        itemIcon.sprite = null;
        itemIcon.color = Color.clear;
        itemAmount.text = "";
    }
    
    public void OnSlotClicked()
    {
        ParentDisplay?.SlotClicked(this);
    }
}
