using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolumeController : MonoBehaviour
{
    private float bajaKiller = 1f;
    public AudioSource[] AudioSources;
    public AudioSource[] bulletSounds;
    public SpawnPlayers sp;
    public bool hold = false;

    private static float musicVolume = 1f;
    // Start is called before the first frame update

    public void PlayGunShots()
    {
        hold = true;
        AudioSources[15].Play();
    }
    public void StopGunShots()
    {
        hold = false;
        AudioSources[15].Stop();
    }
    void Start()
    {
        try
        {
            if (MainMenu.instance.GetCurrentLevel() == 1)
            {
                AudioSources[0].Play();
                AudioSources[1].Play();
                AudioSources[2].Play();
                AudioSources[3].Play();
                AudioSources[4].Play();
                AudioSources[5].Play();
                AudioSources[6].Play();
                AudioSources[7].Play();
                AudioSources[8].Play();
                AudioSources[9].Play();
                AudioSources[10].Play();
                AudioSources[11].Play();
                AudioSources[12].Play();
                AudioSources[13].Play();
                AudioSources[14].Play();
                //StartCoroutine(DelayHornBaja(96f));
            }
            else
            {
                AudioSources[1].Play();
                AudioSources[2].Play();
                AudioSources[3].Play();
                AudioSources[4].Play();
                AudioSources[5].Play();
                AudioSources[6].Play();
                AudioSources[7].Play();
                AudioSources[8].Play();
                AudioSources[9].Play();
                AudioSources[10].Play();
                AudioSources[11].Play();
                AudioSources[12].Play();
                AudioSources[13].Play();
                AudioSources[14].Play();
                
            }
        }
        catch { }
    }

/*    public IEnumerator DelayHornBaja(float time) 
    {
        yield return new WaitForSeconds(time);
        bajaKiller = 0f;
   
    }*/
    // Update is called once per frame
    void Update()
    {
        if (hold) 
        {
            if (sp.spawnedPlayer.GetComponent<Shooting>().currentBullets < 1)
            {
                AudioSources[15].Stop();
            }
            else
            {
                if (!AudioSources[15].isPlaying) 
                {
                    AudioSources[15].Play();
                }
            }
        }


        try
        {
            AudioSources[0].volume = musicVolume;
            AudioSources[1].volume = musicVolume; //- bajaKiller;
            AudioSources[2].volume = musicVolume;
            AudioSources[3].volume = musicVolume;
            AudioSources[4].volume = musicVolume;
            AudioSources[5].volume = musicVolume;
            AudioSources[6].volume = musicVolume;
            AudioSources[7].volume = musicVolume;
            AudioSources[8].volume = musicVolume;
            AudioSources[9].volume = musicVolume;
            AudioSources[10].volume = musicVolume;
            AudioSources[11].volume = musicVolume;
            AudioSources[12].volume = musicVolume;
            AudioSources[13].volume = musicVolume;
            AudioSources[14].volume = musicVolume;
            AudioSources[15].volume = musicVolume;
        }
        catch 
        {
           
        }
    }

    public void updateVolume(float volume) 
    {
        musicVolume = volume;
    }

}
