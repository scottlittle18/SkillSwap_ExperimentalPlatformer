/// <summary>
/// Interface for making an object Draggable
/// </summary>
public interface IDraggable
{
    void SetInitialStartingPoint();
    void StartBeingDraggedByCursor();
}

public interface IDroppable
{
    void OnTriggerStay();
}
