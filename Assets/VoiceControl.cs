using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceControl : MonoBehaviour
{
    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;

    public PlayerController player; 

    void Start()
    {
        // Bind the "Jump" keyword to PlayerController.Jump
        keywordActions.Add("jump", () => player.Jump());

        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        keywordRecognizer.Start();
    }

    // Runs when a spoken keyword is heard
    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Action keywordAction;
        Debug.Log("Heard: " + args.text);
        if (keywordActions.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
