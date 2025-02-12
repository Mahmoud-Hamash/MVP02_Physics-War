using System;
using System.Collections;
using System.Collections.Generic;
using Meta.WitAi.TTS.Data; // Ensure this is included to use Dictionary
using UnityEngine;
using Meta.WitAi.TTS.Utilities;

public class Teacher : MonoBehaviour
{
    [SerializeField] private TTSSpeaker _ttsSpeaker;  // Reference to TTSSpeaker to speak the text
    [SerializeField] private int currentEvent = 1;  // Keep track of which event is currently active
    private Animator _animator;

    private readonly Dictionary<int, string[]> eventTexts = new Dictionary<int, string[]>
    {
        { 1, new[] { "Okay, first things first: the counterweight. The heavier it is, the more distance our projectile will travel. Now, take a look at this equation—it helps explain exactly how that works." } },


        { 2, new[] { "Alright, now let's put the heavy projectile in place. Its weight matters—heavier ones need more energy to launch. Look at the equation again!" } }, // formula near counterweight


        { 3, new[] { "Try the medium-weight projectile to see if you can destroy the first tower!" } },// formula, near projectile place holder


        { 4, new[] { "Great shot, warrior! Now, hit the second tower. Greater distance needs what? Think and fire!" } },


        { 5, new[] { "Well done, warrior! You've mastered the trebuchet—you're ready to go to war!" } }

    };
    
    private bool isSpeaking = false;  // To prevent multiple calls to Speak() while already speaking

    public GameObject ArrowStep2;
    public GameObject FormulaStep2;
    public GameObject ArrowStep3;
    public GameObject FormulaStep3;
    
    //[Obsolete("Obsolete")]
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _ttsSpeaker = GetComponentInChildren<TTSSpeaker>();
        _animator.SetInteger("status", 1);
        
        if (_ttsSpeaker != null)
        {
            // Subscribe to TTS events
            _ttsSpeaker.Events.OnPlaybackStart.AddListener(OnSpeechStart);
            _ttsSpeaker.Events.OnPlaybackComplete.AddListener(OnSpeechFinished);
        }

        // Optionally, you can start speaking the intro text here:
        Speak("Welcome to this weapon tutorial! Get ready to learn some amazing physics. This medieval weapon used physics to smash fortresses. Ready to learn?");
    }

    private void OnSpeechStart(TTSSpeaker arg0, TTSClipData arg1)
    {
        Debug.Log($"Phrase started: {currentEvent}");
    }

    private void OnSpeechFinished(TTSSpeaker arg0, TTSClipData arg1)
    {
        Debug.Log($"Phrase finished: {currentEvent}");
        // currentEvent++;

        switch (currentEvent)
        {
            case 1: StartCoroutine(DelayedTrigger(1)); break;
            case 2: StartCoroutine(DelayedTrigger(2)); break;
        }
    }
    
    // Ensure some delay between phrases
    private IEnumerator DelayedTrigger(int nextEventId)
    {
        yield return new WaitForSeconds(1f);  // Wait for 3 seconds
        TriggerEvent(nextEventId);
    }

    private void Update()
    {
        // Listen for Enter key press
        if (Input.GetKeyDown(KeyCode.Return) && !isSpeaking)  // Check if we're not currently speaking
        {
            TriggerEvent(currentEvent);
        }
    }

    public int GetCurrentEvent()
    {
        return currentEvent;
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
            if (eventId == 1)
            {
                ShowNearFormulaCounterWeightStep2();
            } else if (eventId == 2)
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
                // currentEvent = 1;  // Reset to the first event (or disable this if you want to stop)
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
