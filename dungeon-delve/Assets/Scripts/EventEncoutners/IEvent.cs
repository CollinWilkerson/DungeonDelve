using UnityEngine;

public interface IEvent
{
    public int GetValidOptions();
    public string GetDescriptionText();
    public Sprite GetEventImage();

    public void Option1();
    public void Option2();
    public void Option3();
}
