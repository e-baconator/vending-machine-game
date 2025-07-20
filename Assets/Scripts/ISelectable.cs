using UnityEngine;

public interface ISelectable
{
    public string GetItemID();
    public void SetChildPosition(Transform newChild);
    public void Use();
}
