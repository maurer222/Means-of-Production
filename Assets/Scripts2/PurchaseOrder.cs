using System.Collections.Generic;

public class PurchaseOrder
{
    public int OrderID { get; private set; }
    public List<Task> Tasks { get; private set; }
    public bool IsCompleted { get; private set; }

    public PurchaseOrder(int orderId, List<Task> tasks)
    {
        OrderID = orderId;
        Tasks = tasks;
        IsCompleted = false;
    }

    public void CompleteOrder()
    {
        IsCompleted = true;
        // Additional logic for completing order
    }
}