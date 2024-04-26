
public interface IItemContainer
{
    // int ItemCount(ItemData item);
    bool ContainsItem(ItemData item);
    bool AddItem(ItemData item);
    bool RemoveItem(ItemData item);
    bool IsFull();
    // void Clear();
}
