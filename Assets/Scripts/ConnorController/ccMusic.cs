using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ccMusic : MonoBehaviour {

    AudioSource musicSource;
    public AudioClip bgTrack1, bgTrack2;
    bool rngSwitch = false;

    private void Start()
    {
        //if (GetComponent<ccLocks>().musicSwitch == true)
        //{
        RNG();
        //}
    }

    //void PlayNextSong()
    //{
    //    musicSource.clip = music[Random.Range(0, music.Length)];
    //    musicSource.Play();
    //    Debug.Log("mama mia");
    //    Invoke("PlayNextSong", musicSource.clip.length);
    //}
    private void Update()
    {
        if (GetComponent<ccLocks>().musicSwitch == true && rngSwitch == false)
        {
            rngSwitch = true;
            if (GetComponent<AudioSource>().clip != null)
            {
                Debug.Log("Restart");
                GetComponent<AudioSource>().Play();
            }
            else
            {
                RNG();
            }
        }
        else if (GetComponent<ccLocks>().musicSwitch == false)
        {
            rngSwitch = false;
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = null;
        }
    }

    void RNG()
    {
        if (rngSwitch == true)
        {
            float bgRand = Random.Range(1f, 2f);
            float bgNum = Mathf.Round(bgRand);
            Debug.Log(string.Format("Number: {0}", bgRand));

            if (bgNum == 1)
            {
                StartCoroutine(PlayTrackOne());
            }
            else if (bgNum == 2)
            {
                StartCoroutine(PlayTrackTwo());
            }
        }

    }

    IEnumerator PlayTrackOne()
    {
        yield return new WaitForEndOfFrame();
        GetComponent<AudioSource>().clip = bgTrack1;
        GetComponent<AudioSource>().Play();
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        RNG(); 
    }

    IEnumerator PlayTrackTwo()
    {
        yield return new WaitForEndOfFrame();
        GetComponent<AudioSource>().clip = bgTrack2;
        GetComponent<AudioSource>().Play();
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        RNG();
    }
}
