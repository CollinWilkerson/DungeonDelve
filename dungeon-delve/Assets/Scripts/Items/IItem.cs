
public interface IItem
{
    public string ReturnName();
    public string GetDescription();
    public bool HasTarget();
    public void UseItem(MercenaryController merc);
}
