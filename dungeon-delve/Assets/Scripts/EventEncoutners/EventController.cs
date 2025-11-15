using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventController : MonoBehaviour
{
    [SerializeField] private Button[] optionButtons;
    [SerializeField] private TextMeshProUGUI eventText;
    private IEvent selectedEvent;

    private void Awake()
    {
        IEvent[] validEvents;
        validEvents = GetComponentsInChildren<IEvent>();
        selectedEvent = validEvents[Random.Range(0, validEvents.Length)];
    }

    private void Start()
    {
        SetValidButtonsActive();
        AddButtonEvents();
        eventText.text = selectedEvent.GetDescriptionText();
    }

    private void AddButtonEvents()
    {
        //this isnt very open to extension but I dont have a better idea rn
        optionButtons[0].onClick.AddListener(selectedEvent.Option1);
        optionButtons[1].onClick.AddListener(selectedEvent.Option2);
        optionButtons[2].onClick.AddListener(selectedEvent.Option3);
    }

    private void SetValidButtonsActive()
    {
        foreach (Button b in optionButtons)
        {
            b.gameObject.SetActive(false);
        }
        for (int i = 0; i < selectedEvent.GetValidOptions(); i++)
        {
            optionButtons[i].gameObject.SetActive(true);
        }
    }
}
