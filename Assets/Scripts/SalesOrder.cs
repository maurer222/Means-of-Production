using System.Collections.Generic;

public class SalesOrder
{
    public int OrderID { get; private set; }
    public List<Task> Tasks { get; private set; }
    public bool IsCompleted { get; private set; }

    public SalesOrder(int orderId, List<Task> tasks)
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