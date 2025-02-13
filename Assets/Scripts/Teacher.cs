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
        { 1, new[] { "First things first: the counterweight. The heavier it is, the more distance our projectile will travel: take a look at this equation!" } },


        { 2, new[] { "Alright, now let's put the heavy projectile in place. Its weight matters: heavier ones need more energy to launch. Look at the equation again!" } }, // formula near counterweight


        { 3, new[] { "Now try the medium-weight projectile to see if you can destroy the first tower!" } },// formula, near projectile place holder


        { 4, new[] { "Great shot, warrior! Now, hit the second tower. Greater distance needs what? Think and fire: no hints this time!" } },


        { 5, new[] { "Well done, warrior! You've mastered the trebuchet—you're ready to go to war!" } }

    };
    
    private bool isSpeaking = false;  // To prevent multiple calls to Speak() while already speaking

    public GameObject arrowStep1;
    public GameObject arrowStep2;
    public GameObject arrowStep3;
    public GameObject formula;
    public Banner banner;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _ttsSpeaker = GetComponentInChildren<TTSSpeaker>();
        
        if (_ttsSpeaker != null)
        {
            // Subscribe to TTS events
            _ttsSpeaker.Events.OnPlaybackStart.AddListener(OnSpeechStart);
            _ttsSpeaker.Events.OnPlaybackComplete.AddListener(OnSpeechFinished);
        }

        // Optionally, you can start speaking the intro text here:
        Speak("Welcome to this medieval weapon tutorial! Get ready to learn some amazing physics. The trebuchet used physics to smash fortresses. Ready to learn?");
    }

    private void OnSpeechStart(TTSSpeaker arg0, TTSClipData arg1)
    {
        Debug.Log($"Phrase started: {currentEvent}");
        _animator.SetInteger("status", 0);
    }


    private void OnSpeechFinished(TTSSpeaker arg0, TTSClipData arg1)
    {
        Debug.Log($"Phrase finished: {currentEvent}");
        _animator.SetInteger("status", 2);  // set idle
        // currentEvent++;
        
        switch (currentEvent)
        {
            case 1: StartCoroutine(DelayedTrigger(1)); break;
        }
    }
    
    // Ensure some delay between phrases
    private IEnumerator DelayedTrigger(int nextEventId)
    {
        yield return new WaitForSeconds(1.5f);  // Wait for _ seconds
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
            if (eventId == 1)   // show arrow1, formula
            {
                arrowStep1.SetActive(true);
                formula.SetActive(true);
            } 
            else if (eventId == 2)  // hide arrow1, show arrow2
            {
                arrowStep1.SetActive(false);
                arrowStep2.SetActive(true);
            }
            else if (eventId == 3)  // hide arrow2, show arrow3
            {
                arrowStep2.SetActive(false);
                arrowStep3.SetActive(true);
            }
            else if (eventId == 4)  // hide arrow3
            {
                arrowStep3.SetActive(false);
                banner.ShowBanner(0);
            }
            else if (eventId == 5)  // hide formula, show banner
            {
                formula.SetActive(false);
                banner.ShowBanner(1);
            }
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
