using UnityEngine;

public interface IEvent
{
    public int GetValidOptions();
    public string GetDescriptionText();
    public Sprite GetEventImage();

    public void Option1();
    public string GetButtonText_Option1();
    public void Option2();
    public string GetButtonText_Option2();
    public void Option3();
    public string GetButtonText_Option3();
}
