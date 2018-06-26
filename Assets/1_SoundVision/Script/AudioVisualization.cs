using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LibPDBinding;



public class AudioVisualization : MonoBehaviour
{
    public AudioReader audioReader;

    public enum AudioVisualization_Type { none = 0, linear, circle, tunnel }
    public enum ParticleEffect_Type { none = 0, play, lifeTime, startSpeed }

    public AudioVisualization_Type _AudioVisualizationType = AudioVisualization_Type.none;
    public ParticleEffect_Type _ParticleEffectType = ParticleEffect_Type.none;
    //two AudioClips who interpolate between eachother
    public AudioClip[] clips;
    [Space]
    public GameObject voiceOver;
    [Space]
    public Material paticleMat;
    public float particleRate;

    [Space]
    public bool rotation;
    public Vector2 rotateSensitivityRange;
    public float rotateSensitivity;
    public Vector3 rotateAmmount;
    [Space]
    public bool scale;
    public Vector2 scaleSensitivityRange;
    public float scaleSensitivity;
    public float scaleAmmount;
    public float scaleSmooth;
    [Space]
    public bool color;
    public Color minColor;
    public Color maxColor;
    public Color inverted_minColor;
    public Color inverted_maxColor;
    public float maxColorAmmount;
    public float mulColorAmmount;
    public float inverted_maxColorAmmount;
    public float inverted_mulColorAmmount;
    public Gradient particleColorGradient;
    [Space]
    public bool movement;
    public float movXAmmount;
    public float movXSpeed;
    public float movYAmmount;
    public float movYSpeed;
    [Space]

    public GameObject ps_Visualization;
    public Transform parent_Visualization;
    public float objectOffSet = 0.05f;

    [Space]

    public bool centered;

    [Space]

    public Color particleColor;
    public float particleColorIntensity;
    public float audioSensibility = 0.05f;
    public float audioMultiplier = 200f;
    public float audioMax = 200f;
    public float audioSmooth = 0.9f;

    [Space]

    public bool duplicateInverted;
    public Color inverted_particleColor;
    public float inverted_particleColorIntensity;
    public float inverted_audioSensibility = 0.05f;
    public float inverted_audioMultiplier = 200f;
    public float inverted_audioMax = 200f;
    public float inverted_audioSmooth = 0.9f;

    [HideInInspector]
    private ParticleSystem[] particles;
    private ParticleSystem[] inverted_particles;


    private Vector3 originalPosition;

    // new Stuff
    private GameObject audioReaderObjectMic;
    private AudioSource audioSourceMic;
    private float[] micChannel1;
    private float[] micChannel2;

    //AudioClipA
    private float[] aChannel1;
    private float[] aChannel2;
    private float[] aChannel3;
    private float[] aChannel4;
    private float[] aSamples;
    //AudioClipB
    private float[] bChannel1;
    private float[] bChannel2;
    private float[] bChannel3;
    private float[] bChannel4;
    private float[] bSamples;
    //AudioClipZ
    private float[] zChannel1;
    private float[] zChannel2;
    private float[] zChannel3;
    private float[] zChannel4;
    private float[] zSamples;

    private bool startInterpolation = false;

    private int clipCount = 1;
    private int counterA = 0;
    private int counterB = 0;
    private int counterZ = 0;
    private float interpolationA = 1;
    private float interpolationB = 0;
    private float interpolationToZ = 100;

    private AudioSource voiceOverSource;
    public AudioClip[] voiceOverClips;

    private fromPdScript pd;

    private float gameTime = 0f;
    private bool halfProcent = false;
    private bool hiddenSecretRevealed = false;
    private bool gameOver = false;

    private float waitTimeInterpolationsZyklus = 7f;
    private float timerInterpolationsZyklus;
    private float waitTimeVoiceOver20 = 20f;
    private float timerVoiceOver;
    private float oldCorrect = 0;



    private void Awake()
    {
        GameObject pdReader = GameObject.Find("percentFromPd");
        pd = pdReader.GetComponent<fromPdScript>();

        voiceOverSource = voiceOver.GetComponent<AudioSource>();
        voiceOverClips = Resources.LoadAll<AudioClip>("Gruppe1/VoiceOver");
        print(voiceOverClips.Length);
        ChangeVoiceOver(0);

        StartCoroutine(RereadAudioClips());
        audioReaderObjectMic = GameObject.Find("ChannelMic");
        audioSourceMic = audioReaderObjectMic.GetComponent<AudioSource>();
        particles = new ParticleSystem[audioReader.audioSamples.Length];
        inverted_particles = new ParticleSystem[audioReader.audioSamples.Length];
        Debug.Log("Particles: " + audioReader.audioSamples.Length);
        if (movement) { originalPosition = transform.position; }

        for (int i = 0; i < audioReader.audioSamples.Length; i++)
        {

            float i2 = i;

            if (centered)
            {
                if (i % 2 == 0) { i2 = -i2; }
            }

            Vector3 pos = Vector3.zero;

            switch (_AudioVisualizationType)
            {
                case AudioVisualization_Type.linear:
                    pos = transform.position + transform.right * i2 * objectOffSet;
                    particles[i] = Instantiate(ps_Visualization, pos, transform.rotation, parent_Visualization).GetComponent<ParticleSystem>();
                    particles[i].name = "Particle: " + i;
                    if (duplicateInverted)
                    {
                        inverted_particles[i] = Instantiate(ps_Visualization, pos, Quaternion.Inverse(transform.rotation), parent_Visualization).GetComponent<ParticleSystem>();
                        inverted_particles[i].name = "Inverted Particle: " + i;
                    }
                    break;

                case AudioVisualization_Type.circle:
                    pos = new Vector3((Mathf.Sin((i2 / audioReader.audioSamples.Length) * Mathf.PI)) * objectOffSet, (Mathf.Cos((i2 / audioReader.audioSamples.Length) * Mathf.PI)) * objectOffSet, transform.position.z);
                    particles[i] = Instantiate(ps_Visualization, pos, Quaternion.LookRotation(pos - transform.position), parent_Visualization).GetComponent<ParticleSystem>();
                    particles[i].name = "Particle: " + i;
                    if (duplicateInverted)
                    {
                        inverted_particles[i] = Instantiate(ps_Visualization, pos, Quaternion.LookRotation(transform.position - pos), parent_Visualization).GetComponent<ParticleSystem>();
                        inverted_particles[i].name = "Inverted Particle: " + i;
                    }
                    break;
                case AudioVisualization_Type.tunnel:
                    pos = new Vector3((Mathf.Sin((i2 / audioReader.audioSamples.Length) * Mathf.PI)) * objectOffSet, (Mathf.Cos((i2 / audioReader.audioSamples.Length) * Mathf.PI)) * objectOffSet, transform.position.z);
                    particles[i] = Instantiate(ps_Visualization, pos, Quaternion.LookRotation(-Camera.main.transform.forward), parent_Visualization).GetComponent<ParticleSystem>();
                    particles[i].name = "Particle: " + i;
                    if (i >= 377)
                    {
                        particles[i].gameObject.SetActive(false);
                    }
                    var mainP = particles[i].main;
                    mainP.startSize = 2;
                    var emission = particles[i].emission;
                    emission.rateOverTime = particleRate;
                    if (duplicateInverted)
                    {
                        inverted_particles[i] = Instantiate(ps_Visualization, pos, Quaternion.LookRotation(Camera.main.transform.forward), parent_Visualization).GetComponent<ParticleSystem>();
                        inverted_particles[i].name = "Inverted Particle: " + i;
                    }
                    break;
            }

            var main = particles[i].main;
            main.startColor = particleColor;

            if (duplicateInverted)
            {
                var invmain = inverted_particles[i].main;
                invmain.startColor = inverted_particleColor;
            }
        }

    }

    private void ChangeVoiceOver(int i)
    {
        voiceOverSource.clip = voiceOverClips[i];
        voiceOverSource.Play();
        timerVoiceOver = 0;
    }

    public void InterpolationBetweenAandB()
    {
        //interpolationA -= 0.1f;
        //interpolationB += 0.1f;
        //Debug.Log("Interpolation started... Prozent = " + interpolationB * 100);
        if (!startInterpolation)
        {
            startInterpolation = true;
        }
        else
        {
            //startInterpolation = false; 
        }
    }

    private IEnumerator RereadAudioClips()
    {
        Debug.Log("####################################################################");
        Debug.Log("File: " + clips[0].name);
        //AudioClipA
        zSamples = new float[clips[0].samples * clips[0].channels];
        zChannel1 = new float[clips[0].samples];
        zChannel2 = new float[clips[0].samples];
        zChannel3 = new float[clips[0].samples];
        zChannel4 = new float[clips[0].samples];
        Debug.Log("Mystery Audio Channels: " + clips[0].channels);
        Debug.Log("Mystery Audio Samples: " + clips[0].samples);
        clips[0].GetData(zSamples, 0);
        Debug.Log("Mystery Audio Samples all channels: " + zSamples.Length);

        Debug.Log("####################################################################");
        Debug.Log("Starting Clip: " + clips[clipCount].name);
        //AudioClipA
        aSamples = new float[clips[clipCount].samples * clips[clipCount].channels];
        aChannel1 = new float[clips[clipCount].samples];
        aChannel2 = new float[clips[clipCount].samples];
        aChannel3 = new float[clips[clipCount].samples];
        aChannel4 = new float[clips[clipCount].samples];
        Debug.Log("Clib A Audio Channels: " + clips[clipCount].channels);
        Debug.Log("Clib A Audio Samples: " + clips[clipCount].samples);
        clips[clipCount].GetData(aSamples, 0);
        Debug.Log("Clib A Audio Samples all channels: " + aSamples.Length);

        if (clipCount == clips.Length - 1)
        {
            clipCount = 0;
        }
        Debug.Log("--------------------------------------------------------------------");
        Debug.Log("Following Clip: " + clips[clipCount+1].name);
        //AudioClipB
        bSamples = new float[clips[clipCount + 1].samples * clips[clipCount + 1].channels];
        bChannel1 = new float[clips[clipCount + 1].samples];
        bChannel2 = new float[clips[clipCount + 1].samples];
        bChannel3 = new float[clips[clipCount + 1].samples];
        bChannel4 = new float[clips[clipCount + 1].samples];
        Debug.Log("Clib B Channels: " + clips[clipCount + 1].channels);
        Debug.Log("Clib B Samples: " + clips[clipCount + 1].samples);
        clips[clipCount + 1].GetData(bSamples, 0);
        Debug.Log("Clib B Audio Samples all channels: " + bSamples.Length);

        yield return new WaitForSeconds(0.01f);
    }

    private void Update()
    {
        //prozentTest == pd.correct;

        gameTime += Time.deltaTime;
        //Interpolation Timer
        timerInterpolationsZyklus += Time.deltaTime;
        if (timerInterpolationsZyklus > waitTimeInterpolationsZyklus)
        {
            startInterpolation = true;
        }
        //VoiceOver Timer
        if (!voiceOverSource.isPlaying)
        {
            timerVoiceOver += Time.deltaTime;
            //if (timerVoiceOver > waitTimeVoiceOver20 && pd.correct < 20 )
            if (timerVoiceOver > waitTimeVoiceOver20 && pd.correct < 20)     
            {
                Debug.Log(waitTimeVoiceOver20 + " sekunden unter 20%");
                ChangeVoiceOver(1);
                pd.correct = 30;
            }
            else if (timerVoiceOver > waitTimeVoiceOver20 && (pd.correct >= 20 && pd.correct < 40))
            {
                Debug.Log(waitTimeVoiceOver20 + " sekunden unter 40%");
                ChangeVoiceOver(2);
                pd.correct = 50;
            }
            //### ab hier muss eigentlich die waitTime
            else if (pd.correct >= 50 && pd.correct < 100 && !halfProcent)
            {
                Debug.Log("50% erreicht");
                ChangeVoiceOver(3);
                halfProcent = true;
            }
            else if (pd.correct >= 100 && !hiddenSecretRevealed)
            {
                Debug.Log("100% erreicht");
                ChangeVoiceOver(4);
                hiddenSecretRevealed = true;
            }
        }
        // Game Ends after 3 Minutes = 180 Secounds
        if(gameTime >= 180 && !gameOver)
        {
            Debug.Log("Zeit ist um");
            ChangeVoiceOver(5);
            gameOver = true;
            //SceneManager usw
        }




#pragma warning disable CS0618 // Typ oder Element ist veraltet
        micChannel1 = audioSourceMic.GetOutputData(512, 0);
#pragma warning restore CS0618 // Typ oder Element ist veraltet
#pragma warning disable CS0618 // Typ oder Element ist veraltet
        micChannel2 = audioSourceMic.GetOutputData(512, 1);
#pragma warning restore CS0618 // Typ oder Element ist veraltet

        if (counterA >= clips[clipCount].samples)
        {
            counterA = 0;
        }
        for (int i = 0; i < 512; i++)
        {
            aChannel1[i] = aSamples[(counterA + i) * 4];
            aChannel2[i] = aSamples[(counterA + i) * 4 + 1];
            aChannel3[i] = aSamples[(counterA + i) * 4 + 2];
            aChannel4[i] = aSamples[(counterA + i) * 4 + 3];
        }

        if (counterB >= clips[clipCount + 1].samples)
        {
            counterB = 0;
        }
        for (int i = 0; i < 512; i++)
        {
            bChannel1[i] = bSamples[(counterB + i) * 4];
            bChannel2[i] = bSamples[(counterB + i) * 4 + 1];
            bChannel3[i] = bSamples[(counterB + i) * 4 + 2];
            bChannel4[i] = bSamples[(counterB + i) * 4 + 3];
        }

        if (counterZ >= clips[0].samples)
        {
            counterZ = 0;
        }
        for (int i = 0; i < 512; i++)
        {
            zChannel1[i] = zSamples[(counterZ + i) * 4];
            zChannel2[i] = zSamples[(counterZ + i) * 4 + 1];
            zChannel3[i] = zSamples[(counterZ + i) * 4 + 2];
            zChannel4[i] = zSamples[(counterZ + i) * 4 + 3];
        }

        counterA += 512;
        counterB += 512;
        counterZ += 512;


        //#pragma warning disable CS0618 // Typ oder Element ist veraltet
        //        channelThree = audioSource.GetOutputData(8192, 2);
        //#pragma warning restore CS0618 // Typ oder Element ist veraltet

        switch (_ParticleEffectType)
        {
            case ParticleEffect_Type.lifeTime:
                for (int i = 0; i < audioReader.audioSamples.Length; i++)
                {
                    if (audioReader.audioSamples[i] * audioMultiplier > audioMax) { audioReader.audioSamples[i] = audioMax / audioMultiplier; }
                    if (audioReader.audioSamples[i] > audioSensibility)
                    {
                        var main = particles[i].main;
                        main.startLifetime = Mathf.Lerp(main.startLifetime.constant, audioReader.audioSamples[i] * audioMultiplier, audioSmooth);
                    }
                    else
                    {
                        var main = particles[i].main;
                        main.startLifetime = 0f;
                    }

                    if (duplicateInverted)
                    {
                        if (audioReader.audioSamples[i] * inverted_audioMultiplier > inverted_audioMax) { audioReader.audioSamples[i] = inverted_audioMax / inverted_audioMultiplier; }
                        if (audioReader.audioSamples[i] > inverted_audioSensibility)
                        {
                            var main = inverted_particles[i].main;
                            main.startLifetime = Mathf.Lerp(main.startLifetime.constant, audioReader.audioSamples[i] * inverted_audioMultiplier, inverted_audioSmooth);
                        }
                        else
                        {
                            var main = inverted_particles[i].main;
                            main.startLifetime = 0f;
                        }
                    }
                }
                break;
            case ParticleEffect_Type.startSpeed:
                for (int i = 0; i < audioReader.audioSamples.Length; i++)
                {
                    if (aChannel1[i] * audioMultiplier > audioMax) { aChannel1[i] = audioMax / audioMultiplier; }
                    if (aChannel1[i] > audioSensibility)
                    {
                        var main = particles[i].main;
                        main.startSpeed = Mathf.Lerp(main.startSpeed.constant, aChannel1[i] * audioMultiplier, audioSmooth);
                        //übergabe der channel data an die Position
                        //#Übergabe A zu B und dann soll weiter laufen auf B und nächstes mal inverse B zu A damit nicht der Laufende Cyklus gebrochen wird. 
                        if (startInterpolation)
                        {
                            particles[i].transform.position = new Vector3(
                                                                          ((((aChannel1[i] + micChannel1[i]) * interpolationA) * 100) + (((bChannel1[i] + micChannel1[i]) * interpolationB) * 100) * ((interpolationToZ - pd.correct) / 100)) + (zChannel1[i] * pd.correct / 100 * 100),
                                                                          ((((aChannel2[i] + micChannel2[i]) * interpolationA) * 100) + (((bChannel2[i] + micChannel2[i]) * interpolationB) * 100) * ((interpolationToZ - pd.correct) / 100)) + (zChannel2[i] * pd.correct / 100 * 100),
                                                                          (((aChannel3[i] + ((micChannel1[i] + micChannel2[i])) * interpolationA) * 100) + (((bChannel3[i] + ((micChannel1[i] + micChannel2[i])) * interpolationB) * 100)) * ((interpolationToZ - pd.correct) / 100)) + (zChannel3[i] * pd.correct / 100 * 100)
                                                                         );
                            if (interpolationA < 0)
                            {
                                clipCount++;
                                StartCoroutine(RereadAudioClips());
                                startInterpolation = false;
                                interpolationA = 1;
                                interpolationB = 0;
                                timerInterpolationsZyklus = 0f;

                            }
                            interpolationA -= Time.deltaTime / 2000;
                            interpolationB += Time.deltaTime / 2000;
                        }
                        else
                        {
                            particles[i].transform.position = new Vector3((((aChannel1[i] + micChannel1[i]) * ((interpolationToZ - pd.correct) / 100)) * 100) + (zChannel1[i] * pd.correct / 100 * 100), 
                                                                          (((aChannel2[i] + micChannel2[i]) * ((interpolationToZ - pd.correct) / 100)) * 100) + (zChannel2[i] * pd.correct / 100 * 100), 
                                                                          (((aChannel3[i] + (micChannel1[i] + micChannel2[i])) * ((interpolationToZ - pd.correct) / 100)) * 100) + (zChannel3[i] * pd.correct / 100 * 100)
                                                                         );
                        }

                    }
                    else
                    {
                        var main = particles[i].main;
                        main.startSpeed = 0f;
                        if (duplicateInverted)
                        {
                            var inv_main = inverted_particles[i].main;
                            inv_main.startSpeed = 0;
                        }
                    }

                    if (duplicateInverted)
                    {
                        if (audioReader.audioSamples[i] * inverted_audioMultiplier > inverted_audioMax) { audioReader.audioSamples[i] = inverted_audioMax / inverted_audioMultiplier; }
                        if (audioReader.audioSamples[i] > inverted_audioSensibility)
                        {
                            var main = inverted_particles[i].main;
                            main.startSpeed = Mathf.Lerp(main.startSpeed.constant, audioReader.audioSamples[i] * inverted_audioMultiplier, inverted_audioSmooth);
                        }
                        else
                        {
                            var main = inverted_particles[i].main;

                        }
                    }
                }
                break;

            case ParticleEffect_Type.play:
                for (int i = 0; i < audioReader.audioSamples.Length; i++)
                {
                    if (audioReader.audioSamples[i] > audioSensibility)
                    {
                        if (particles[i].IsAlive())
                        {
                            particles[i].Play();
                        }
                    }

                    if (duplicateInverted)
                    {
                        if (audioReader.audioSamples[i] > inverted_audioSensibility)
                        {
                            if (inverted_particles[i].IsAlive())
                            {
                                inverted_particles[i].Play();
                            }
                        }
                    }
                }
                break;
        }

        if (rotation)
        {
            float sum = 0f;

            for (int i = (int)rotateSensitivityRange.x; i < rotateSensitivityRange.y; i++)
            {
                if (micChannel1[i] > rotateSensitivity)
                {
                    sum += micChannel1[i];
                }
            }

            float average = sum / (rotateSensitivityRange.y - rotateSensitivityRange.x);

            parent_Visualization.Rotate(rotateAmmount * average, Space.Self);
        }
        if (scale)
        {
            float sum = 0f;

            for (int i = (int)scaleSensitivityRange.x; i < scaleSensitivityRange.y; i++)
            {
                if (micChannel1[i] > scaleSensitivity)
                {
                    sum += micChannel1[i];
                }
            }

            float average = sum / (scaleSensitivityRange.y - scaleSensitivityRange.x);

            parent_Visualization.localScale = Vector3.Lerp(parent_Visualization.localScale, new Vector3(1 + (average * scaleAmmount), 1 + (average * scaleAmmount), 1 + (average * scaleAmmount)), scaleSmooth);
        }
        if (color)
        {
            if ((pd.correct % 10) == 0)
            {
                oldCorrect = pd.correct;
            }

            for (int i = 0; i < particles.Length; i++)
            {
                //float k = audioReader.audioSamples[i] * mulColorAmmount / maxColorAmmount;
                float k = Mathf.Abs(micChannel1[i]) * mulColorAmmount / maxColorAmmount;
                //Debug.Log(k);
                var main = particles[i].main;
                //main.startColor = Color.Lerp(minColor, maxColor, k);
                if (pd.correct != oldCorrect)
                {
                    main.startColor = new Color(0,255,0);
                }
                else if (pd.correct == oldCorrect)
                {
                    main.startColor = particleColorGradient.Evaluate(Random.Range(-0.1f, k));
                }

                if (duplicateInverted)
                {
                    float k2 = audioReader.audioSamples[i] * inverted_mulColorAmmount / inverted_maxColorAmmount;

                    var invmain = inverted_particles[i].main;
                    invmain.startColor = Color.Lerp(inverted_minColor, inverted_maxColor, k2);
                }
            }
        }
        if (movement)
        {
            parent_Visualization.transform.position = originalPosition + new Vector3(Mathf.Sin(Time.time * movXSpeed) * movXAmmount, Mathf.Cos(Time.time * movYSpeed) * movYAmmount, 0);
        }
    }
}


