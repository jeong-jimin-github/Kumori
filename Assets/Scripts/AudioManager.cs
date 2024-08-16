using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using System.IO;
using static UnityEngine.Rendering.PostProcessing.HistogramMonitor;

public class AudioManager : MonoBehaviour
{
    FMOD.ChannelGroup sfxChannelGroup;
    FMOD.Sound sfx;
    FMOD.Channel sfxChannel;
    private Coroutine fadeOutCoroutine;

    public void LoadAudio(string FileName, string audioType)
    {
        sfxChannelGroup = new FMOD.ChannelGroup();
        sfxChannel = new FMOD.Channel();
        sfx = new FMOD.Sound();

        FMODUnity.RuntimeManager.CoreSystem.createSound(Path.Combine(FileName + "." + audioType), FMOD.MODE.CREATESAMPLE, out sfx);

        sfxChannel.setChannelGroup(sfxChannelGroup);

        sfxChannel.stop();
    }

    public void PlayAudio()
    {
        FMODUnity.RuntimeManager.CoreSystem.playSound(sfx, sfxChannelGroup, false, out sfxChannel);
        sfxChannel.setPaused(true);
        sfxChannel.setVolume(1);
        sfxChannel.setPaused(false);
    }

    public void Pause(bool isPause)
    {
        if (isPause == true)
        {
            sfxChannel.setPaused(true);
        }

        else
        {
            sfxChannel.setPaused(false);
        }
    }


    public void Stop(System.Action onFadeOutComplete)
    {
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
        }
        fadeOutCoroutine = StartCoroutine(FadeOut(sfxChannel, 2f, onFadeOutComplete)); // Adjust the fade-out duration as needed
    }

    public void cstop()
    {
        sfxChannel.setVolume(0f);
        sfxChannel.stop();
    }

    private IEnumerator FadeOut(FMOD.Channel channel, float duration, System.Action onFadeOutComplete)
    {
        float startVolume;
        channel.getVolume(out startVolume);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
            channel.setVolume(newVolume);
            yield return null;
        }

        channel.setVolume(0f);
        channel.stop();

        onFadeOutComplete?.Invoke();
    }
}
