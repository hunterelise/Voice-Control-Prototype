using UnityEngine;
using UnityEngine.Windows.Speech;
using TMPro;
using System;
using System.Linq;

public class RepeatAfterMe : MonoBehaviour
{
    [Tooltip("Words for the player to repeat")]
    public string[] words = { "apple", "banana", "cherry", "date", "grape" };

    [Header("UI")]
    public TMP_Text wordText;

    private KeywordRecognizer keywordRecognizer;
    private string currentWord;

    void Start()
    {
        StartVoiceRecognition(); // Start listening to what the player says
        PickRandomWord();        // Show a random word for the player to repeat
    }

    void StartVoiceRecognition()
    {
        // If already listening, stop and clean up
        if (keywordRecognizer != null)
        {
            keywordRecognizer.OnPhraseRecognized -= OnWordSpoken;
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }

        // Set up voice recognition for all words
        string[] lowercaseWords = words.Select(w => w.ToLower()).ToArray();
        keywordRecognizer = new KeywordRecognizer(lowercaseWords);
        keywordRecognizer.OnPhraseRecognized += OnWordSpoken;
        keywordRecognizer.Start();

        Debug.Log("voice recognition started");
    }

    void PickRandomWord()
    {
        if (words.Length == 0)
        {
            Debug.LogWarning("No words to show!");
            return;
        }

        // Pick a random word and display it
        currentWord = words[UnityEngine.Random.Range(0, words.Length)].ToLower();
        Debug.Log("repeat this word: " + currentWord);

        if (wordText != null)
        {
            wordText.text = "Say: " + char.ToUpper(currentWord[0]) + currentWord.Substring(1);
            wordText.color = Color.white;
        }
    }

    private void OnWordSpoken(PhraseRecognizedEventArgs args)
    {
        // Called when the player says something
        string spoken = args.text.ToLower();
        Debug.Log("heard: " + spoken);

        // Check if the player said the correct word
        if (spoken == currentWord)
        {
            Debug.Log("correct!");
            if (wordText != null) wordText.color = Color.green;

            // Wait 1 second then show a new word
            Invoke(nameof(PickRandomWord), 1f);
        }
        else
        {
            Debug.Log("try again: " + currentWord);
        }
    }

    void OnDestroy()
    {
        // Stop listening when the object is removed
        if (keywordRecognizer != null)
        {
            keywordRecognizer.OnPhraseRecognized -= OnWordSpoken;
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }
}
