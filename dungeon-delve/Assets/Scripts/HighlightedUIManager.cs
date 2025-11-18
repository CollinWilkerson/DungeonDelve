using UnityEngine;
using UnityEngine.EventSystems;

//specail thanks to GamesPlusJames at https://www.youtube.com/watch?v=SXBgBmUcTe0 for the help on this
public class HighlightedUIManager
{
    public static void SelectUIGameObject(GameObject objectToSelect)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(objectToSelect);
    }
}
