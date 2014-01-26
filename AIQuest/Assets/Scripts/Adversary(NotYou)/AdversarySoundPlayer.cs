using UnityEngine;
using System.Collections.Generic;

public class AdversarySoundPlayer : MonoBehaviour
{
    public List<AudioClip> adversarySounds;

    private AudioSource soundPlayer;

    public void Start()
    {
        soundPlayer = this.gameObject.GetComponent<AudioSource>();
    }

    public void play(int index)
    {
        if(index >= 0 && index < adversarySounds.Count)
            soundPlayer.clip = adversarySounds[index];
        if(!soundPlayer.isPlaying)
            soundPlayer.Play();
    }
}
