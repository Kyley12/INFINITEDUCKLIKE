using System.Collections;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public float cutsceneDuration = 10f; // Duration before auto-ending cutscene
    private bool isSkipped = false;

    private void Start()
    {
        EventBus.Publish(CutsceneEvents.CutsceneStart); // Notify that cutscene has started
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        float timer = 0f;
        while (timer < cutsceneDuration && !isSkipped)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        EndCutscene();
    }

    private void Update()
    {
        if (Input.anyKeyDown && !isSkipped)
        {
            SkipCutscene();
        }
    }

    private void SkipCutscene()
    {
        isSkipped = true;
        EventBus.Publish(CutsceneEvents.CutsceneSkip);
    }

    private void EndCutscene()
    {
        EventBus.Publish(CutsceneEvents.CutsceneEnd);
    }
}
