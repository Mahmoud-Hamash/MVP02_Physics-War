using System.Collections.Generic;  // Ensure this is included to use Dictionary
using UnityEngine;
using Meta.WitAi.TTS.Utilities;

public class Teacher : MonoBehaviour
{
    [SerializeField] private TTSSpeaker _ttsSpeaker;  // Reference to TTSSpeaker to speak the text
    private Animator _animator;

    private readonly Dictionary<int, string[]> eventTexts = new Dictionary<int, string[]>
    {
        { 1, new[] { "Ever seen a giant slingshot throw huge stones over castle walls? That's a trebuchet!", 
                     "A falling weight creates energy, launching projectiles. Same physics as cranes & rockets! Ready to learn?", 
                     "Okay, first things first: let's load up the counterweight. Grab some stones and place them in the counterweight." } },

        { 2, new[] { "Now, the heavier that counterweight gets, the further our projectile's going to fly.", 
                     "So, more stones in the counterweight means more distance for our projectile. Now, take a look at this equation – it helps explain exactly how that works." } },

        { 3, new[] { "Alright, now let's put our projectile in place. Notice how much it weighs.", 
                     "Its weight matters because a heavier one needs more energy to launch, affecting how far it travels—a lighter projectile flies further with the same counterweight." } },

        { 4, new[] { "There are other factors, but we've kept them constant so you can focus on the counterweight and projectile masses.", 
                     "Now, try and hit that target 3 meters away by pulling the trebuchet handle." } },

        { 5, new[] { "A fine shot, warrior!", 
                     "Now, warrior, for our next challenge! We'll try to hit this second target. Remember what you've learned: greater distance requires... what? Think on it, and then let fly!" } }
    };

    private int currentEvent = 1;  // Keep track of which event is currently active
    private bool isSpeaking = false;  // To prevent multiple calls to Speak() while already speaking

    public GameObject ArrowStep2;
    public GameObject FormulaStep2;
    public GameObject ArrowStep3;
    public GameObject FormulaStep3;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetInteger("status", 1);

        // Optionally, you can start speaking the intro text here:
        Speak("Welcome to the trebuchet tutorial! Get ready to learn some amazing physics.");
    }

    private void Update()
    {
        // Listen for Enter key press
        if (Input.GetKeyDown(KeyCode.Return) && !isSpeaking)  // Check if we're not currently speaking
        {
            TriggerEvent(currentEvent);
        }
    }

    public void TriggerEvent(int eventId)
    {
        if (eventTexts.ContainsKey(eventId))
        {
            Debug.Log($"[Teacher] Event {eventId} triggered. Speaking now...");
            isSpeaking = true;  // Prevent multiple triggers while speaking

            // Speak all texts for the event
            foreach (string text in eventTexts[eventId])
            {
                Speak(text);
            }
            
            // HARDCODED START
            if (eventId == 2)
            {
                ShowNearFormulaCounterWeightStep2();
            } else if (eventId == 3)
            {
                Debug.Log("EVENT 3 hides step 2 and shows step 3");
                HideNearFormulaCounterWeightStep2();
                ShowNearFormulaProjectileStep3();
            }
            else if (eventId == 4)
                HideNearFormulaProjectileStep3();
            // HARDCODED END

            // After finishing the event, move to the next one
            currentEvent++;

            // Ensure we don't go beyond the available events
            if (currentEvent > eventTexts.Count)
            {
                Debug.Log("[Teacher] All events completed.");
                currentEvent = 1;  // Reset to the first event (or disable this if you want to stop)
            }
        }
        else
        {
            Debug.LogWarning($"[Teacher] No dialogue found for Event {eventId}");
        }
    }

    // HARDCODED START
    private void ShowNearFormulaCounterWeightStep2()
    {
        ArrowStep2.SetActive(true);
        FormulaStep2.SetActive(true);
    }

    private void HideNearFormulaCounterWeightStep2()
    {
        ArrowStep2.SetActive(false);
        FormulaStep2.SetActive(false);
    }
    
    private void ShowNearFormulaProjectileStep3()
    {
        ArrowStep3.SetActive(true);
        FormulaStep3.SetActive(true);
    }

    private void HideNearFormulaProjectileStep3()
    {
        ArrowStep3.SetActive(false);
        FormulaStep3.SetActive(false);
    }
    // HARDCODED END

    private void Speak(string text)
    {
        if (_ttsSpeaker != null)
        {
            Debug.Log($"[Teacher] Speaking: {text}");
            _ttsSpeaker.Speak(text);  // Speak the text using TTSSpeaker
            isSpeaking = false;  // Allow new event triggers after the speech is finished
        }
        else
        {
            Debug.LogError("[Teacher] TTSSpeaker is not assigned!");
        }
    }
}
