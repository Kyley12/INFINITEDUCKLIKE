using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI cutsceneText;

    private void OnEnable()
    {
        EventBus.Subscribe(CutsceneEvents.CutsceneStart, OnCutsceneStart);
        EventBus.Subscribe(CutsceneEvents.CutsceneSkip, OnCutsceneSkip);
        EventBus.Subscribe(CutsceneEvents.CutsceneEnd, OnCutsceneEnd);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(CutsceneEvents.CutsceneStart, OnCutsceneStart);
        EventBus.Unsubscribe(CutsceneEvents.CutsceneSkip, OnCutsceneSkip);
        EventBus.Unsubscribe(CutsceneEvents.CutsceneEnd, OnCutsceneEnd);
    }

    private void OnCutsceneStart(object data)
    {
        cutsceneText.text = "Booting system...";
    }

    private void OnCutsceneSkip(object data)
    {
        cutsceneText.text = "Cutscene skipped.";
    }

    private void OnCutsceneEnd(object data)
    {
        cutsceneText.text = "Cutscene ended. Loading menu...";
        LoadMainMenu();
    }

    private void LoadMainMenu()
    {
        // Transition to start menu UI
        Debug.Log("Main menu loaded.");
    }
}
