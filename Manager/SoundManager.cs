using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {

    protected SoundManager() { }

    public AudioSource AudioSAmbiant;
    public AudioClip AmbianteClip;

    public List<TypeSound> listSound;

    private Dictionary<GameTennis.Sound_Type, AudioClip[]> dicoSound = new Dictionary<GameTennis.Sound_Type, AudioClip[]>();
    private AudioSource audioS;

    private bool soundActivate;
    private bool effectActivate;

    [System.Serializable]
    public struct TypeSound
    {
        public GameTennis.Sound_Type type;
        public AudioClip[] sound;
    }

    // Use this for initialization
    void Start ()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
                soundActivate = false;
            else
                soundActivate = true;
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
            soundActivate = true;
        }


        if (PlayerPrefs.HasKey("Effect"))
        {
            if (PlayerPrefs.GetInt("Effect") == 0)
                effectActivate = false;
            else
                effectActivate = true;
        }
        else
        {
            PlayerPrefs.SetInt("Effect", 1);
            effectActivate = true;
        }

        DontDestroyOnLoad(gameObject);

        audioS = GetComponent<AudioSource>();

	    for(int i=0; i< listSound.Count; i++)
        {
            if (!dicoSound.ContainsKey(listSound[i].type))
                dicoSound.Add(listSound[i].type, listSound[i].sound);
            else
                Debug.LogError("Deja cette clée dans le dico :: " + listSound[i].type);
        }

        //StartAmbiante
        AudioSAmbiant.clip = AmbianteClip;
        if (soundActivate)
            AudioSAmbiant.Play();
	}

    public void playerSoundOneTime(GameTennis.Sound_Type type, float volume = 1)
    {
        if (effectActivate)
        {
            if (dicoSound[type].Length > 1)
                audioS.clip = dicoSound[type][Random.Range(0, dicoSound[type].Length)];
            else
                audioS.clip = dicoSound[type][0];

            audioS.volume = Mathf.Clamp01(volume);
            audioS.Play();
        }
    }

    public void PlaySoundOnObject(AudioSource audioSourceObj , GameTennis.Sound_Type type , float volume = 1)
    {
        if (effectActivate)
        {
            if (dicoSound[type].Length > 1)
                audioSourceObj.clip = dicoSound[type][Random.Range(0, dicoSound[type].Length)];
            else
                audioSourceObj.clip = dicoSound[type][0];

            audioSourceObj.volume = Mathf.Clamp01(volume);
            audioSourceObj.Play();
        }
    }
	
    public bool GetBoolSound()
    {
        return soundActivate;
    }

    public bool GetBoolEffect()
    {
        return effectActivate;
    }

    public bool ManageSound()
    {
        soundActivate = !soundActivate;
        if (soundActivate)
        {
            PlayerPrefs.SetInt("Sound", 1);
            AudioSAmbiant.Play();
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 0);
            AudioSAmbiant.Stop();
        }

        return soundActivate;
    }

    public bool ManageEffect()
    {
        effectActivate = !effectActivate;
        if (effectActivate)
        {
            PlayerPrefs.SetInt("Effect", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Effect", 0);
        }

        return effectActivate;
    }
}
