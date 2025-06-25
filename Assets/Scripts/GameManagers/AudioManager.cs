using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audios")]
    public AudioSource BreakingROWScoreUp;
    public AudioSource RocketAudio;
    public AudioSource BombAudio;

    [Header("Objects to track")]
    public GameObject Bomb;
    public GameObject Rocket;

    private GameObject _bombTracker;
    private GameObject _rocketTracker;
    private bool _bombAudioStarted;
    private bool _rocketAudioStarted;

    // Start is called before the first frame update
    void Start()
    {
        _bombAudioStarted = false;
        _rocketAudioStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Bomb Audio
        if(ObjectPool.SharedInstance.GetSpecifiedActiveObject(Bomb) != null && _bombAudioStarted == false)
        {
            Debug.Log("Bomb Audio Started");
            _bombAudioStarted = true;
            _bombTracker = ObjectPool.SharedInstance.GetSpecifiedActiveObject(Bomb);
            BombAudio.Play();
        }
        if (_bombTracker != null && !_bombTracker.activeInHierarchy)
        {
            _bombAudioStarted = false;
        }

        //Rocket Audio
        if (ObjectPool.SharedInstance.GetSpecifiedActiveObject(Rocket) != null && _rocketAudioStarted == false)
        {
            Debug.Log("Rocket Audio Started");
            _rocketAudioStarted = true;
            _rocketTracker = ObjectPool.SharedInstance.GetSpecifiedActiveObject(Rocket);
            RocketAudio.volume = 1.00f;
            RocketAudio.Play();
        }
        if (_rocketTracker != null && !_rocketTracker.activeInHierarchy)
        {
            if (RocketAudio.time >= 3.00f)
            {
                RocketAudio.volume -= 0.005f;
                if (RocketAudio.volume <= 0)
                {
                    RocketAudio.Stop();
                }
            }
            _rocketAudioStarted = false;
        }



    }
}
