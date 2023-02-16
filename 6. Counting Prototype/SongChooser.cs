using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SongChooser : MonoBehaviour
{
    public AudioClip christmasSong;
    public AudioClip infinitySong;
    public AudioClip theBestOfNature;
    public AudioClip strangerThings;
    public AudioClip lofiSummer;
    public TextMeshProUGUI labelText;

    public AudioSource audioSource;

    public void ChooseSong(int val)
    {
        if (val == 0)
        {
            audioSource.clip = christmasSong;
            labelText.text = "Christmas song";
        }

        if (val == 1)
        {
            audioSource.clip = infinitySong;
            labelText.text = "Password infinity";
        }

        if (val == 2)
        {
            audioSource.clip = theBestOfNature;
            labelText.text = "The best of nature";
        }

        if (val == 3)
        {
            audioSource.clip = strangerThings;
            labelText.text = "Stranger things";
        }

        if (val == 4)
        {
            audioSource.clip = lofiSummer;
            labelText.text = "Lofi summer";
        }

        audioSource.Play();

    }
}
