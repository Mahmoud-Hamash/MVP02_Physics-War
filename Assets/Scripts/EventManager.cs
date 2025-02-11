using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Teacher teacherScript;  // Reference to the Teacher script
    private int currentEvent = 1;  // Start with Event 1
    private const int maxEvents = 5;  // Number of events

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  // Press Space to trigger the next event
        {
            TriggerNextEvent();
        }
    }

    private void TriggerNextEvent()
    {
        if (currentEvent <= maxEvents)  // Ensure we don’t go beyond the last event
        {
            Debug.Log($"[EventManager] Triggering Event {currentEvent}");

            if (teacherScript != null)
            {
                teacherScript.TriggerEvent(currentEvent); // Call the teacher script
                Debug.Log($"[EventManager] Teacher should now speak for Event {currentEvent}");
            }
            else
            {
                Debug.LogError("[EventManager] Teacher script is not assigned!");
            }

            currentEvent++;  // Move to the next event
        }
        else
        {
            Debug.Log("[EventManager] All events completed!");
        }
    }
}