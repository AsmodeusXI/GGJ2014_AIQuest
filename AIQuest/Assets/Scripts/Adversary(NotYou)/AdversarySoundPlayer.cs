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

	public void playMonsterUnlocked(Monster currMonster, bool spriteSwitch) {
		switch (currMonster.type) {
		case Monster.MonsterType.skeleton:
			int clipNumberSkele = UnityEngine.Random.Range(1,4);
			if (spriteSwitch) {
				PlayClipAudio2("Speech/Jane/JaneSkele" + clipNumberSkele);
			} else {
				PlayClipAudio2("Speech/John/JohnSkele" + clipNumberSkele);
			}
			break;
		case Monster.MonsterType.orc:
			int clipNumberOrc = UnityEngine.Random.Range(1,4);
			if (spriteSwitch) {
				PlayClipAudio2("Speech/Jane/JaneOrc" + clipNumberOrc);
			} else {
				PlayClipAudio2("Speech/John/JohnOrc" + clipNumberOrc);
			}
			break;
		case Monster.MonsterType.dragon:
			int clipNumberDragon = UnityEngine.Random.Range(1,4);
			if (spriteSwitch) {
				PlayClipAudio2("Speech/Jane/JaneDragon" + clipNumberDragon);
			} else {
				PlayClipAudio2("Speech/John/JohnDragon" + clipNumberDragon);
			}
			break;
		case Monster.MonsterType.lich:
			if (spriteSwitch) {
				PlayClipAudio2("Speech/Jane/JaneLich1");
			} else {
				PlayClipAudio2("Speech/John/JohnLich1");
			}
			break;
		case Monster.MonsterType.kraken:
			Social.ReportProgress("Artful.Kraken.Unleashed",100.0, success => {
				Debug.Log(success ? "Reported kraken achievement successfully" : "Failed to report achievement");
			});
			if (spriteSwitch) {
				PlayClipAudio2("Speech/Jane/JaneKraken1");
			} else {
				PlayClipAudio2("Speech/John/JohnKraken1");
			}
			break;
		}
	}

	public void PlayClipAudio2(string clipName) {
		soundPlayer.Stop();
		soundPlayer.Pause();
		soundPlayer.clip = null;
		soundPlayer.clip = Resources.Load(clipName)as AudioClip;
		soundPlayer.Play();
	}
}
