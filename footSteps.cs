using System.Collections;
using UnityEngine;

public class footSteps : MonoBehaviour
{
    public AudioSource footstepSource;

    public AudioClip grassFootstep;
    public AudioClip sandFootstep;
    public AudioClip waterFootstep;
    public AudioClip snowFootstep;
    public AudioClip bridgeFootstep;

    public Transform footDetect;

    public KeyCode forwardKey = KeyCode.W;
    public KeyCode backKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

    public float rayDistance = 2f;
    public float fadeTime = 0.01f;

    private string currentSurface = "GRASS";
    private Coroutine fadeCoroutine;
    private float targetVolume = 1f;

    void Update()
    {
        DetectSurface();

        bool isWalking =
            Input.GetKey(forwardKey) ||
            Input.GetKey(backKey) ||
            Input.GetKey(leftKey) ||
            Input.GetKey(rightKey);

        if (isWalking)
        {
            PlayFootstep();

            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeAudio(targetVolume));
        }
        else
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeAudio(0f));
        }
    }

    void DetectSurface()
    {
        RaycastHit hit;
        Vector3 rayStart = footDetect != null ? footDetect.position : transform.position;

        if (Physics.Raycast(rayStart, Vector3.down, out hit, rayDistance))
        {
            currentSurface = hit.collider.gameObject.name;
            // Debug.Log("Standing on: " + currentSurface);
        }
    }

    void PlayFootstep()
    {
        AudioClip clip = GetClipForSurface();

        if (clip == null || footstepSource == null)
            return;

        if (footstepSource.clip != clip)
        {
            footstepSource.Stop();
            footstepSource.clip = clip;
            footstepSource.volume = 0f;
        }

        footstepSource.loop = true;

        if (!footstepSource.isPlaying)
        {
            footstepSource.volume = 0f;
            footstepSource.Play();
        }
    }

    IEnumerator FadeAudio(float target)
    {
        float startVolume = footstepSource.volume;
        float time = 0f;

        while (time < fadeTime)
        {
            time += Time.deltaTime;

            footstepSource.volume =
                Mathf.Lerp(startVolume, target, time / fadeTime);

            yield return null;
        }

        footstepSource.volume = target;

        if (target == 0f)
        {
            footstepSource.Stop();
        }
    }

    AudioClip GetClipForSurface()
    {
        switch (currentSurface)
        {
            case "GRASS": return grassFootstep;
            case "SAND": return sandFootstep;
            case "WATER": return waterFootstep;
            case "SNOW": return snowFootstep;
            case "BRIDGE": return bridgeFootstep;
            default: return grassFootstep;
        }
    }
}
