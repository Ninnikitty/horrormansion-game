using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sanityDealer : MonoBehaviour
{
    GameObject[] sanityObjs;
    float maxSanity = 100;
    float minSanity = 0;
    float sanity;
    int sanityLevel;
    float interwall, timer = 0;
    bool dead = false;

    public Sprite sanity_sprite_1;
    public Sprite sanity_sprite_2;
    public Sprite sanity_sprite_3;
    public Sprite sanity_sprite_4;
    public Sprite sanity_sprite_5;

    public AudioClip heartBeatSound;
    public AudioClip deathSound;
    public AudioSource playerAudio;

    InteractionCounterScript ic;

    // Start is called before the first frame update
    void Start()
    {  
        sanity = maxSanity;
        sanityLevel = 5;
        playerAudio.clip = heartBeatSound;
        GameObject player = GameObject.Find("player");
        sanityObjs = GameObject.FindGameObjectsWithTag("sanity");
        ic = player.GetComponent<InteractionCounterScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check for damage
        if(ic.interactingObjectName == "Good") {
            adjustSanity(0.1f);
        }
        else if (ic.interactingObjectName == "Bad") {
            adjustSanity(-0.1f);
        }
        else {
            adjustSanity(-0.004f);
        }

        UpdateSanityBar();
        setSanityLevel();
        setSanityEffects();
    }
    private void UpdateSanityBar() {
        foreach(GameObject obj in sanityObjs) {
            if (obj.name == "sanity_bar") {
                obj.transform.localScale = new Vector3(sanity / 100, 1, 1);
            }
        }
        //sanityBar.transform.localScale = new Vector3(sanity / 100, 1, 1);
    }
    private void adjustSanity(float amount) {
        sanity += amount;
        if (sanity < minSanity) {
            sanity = minSanity;
        }
        else if (sanity > maxSanity) {
            sanity = maxSanity;
        }
    }
    private void setSanityLevel() {
        sanityLevel = ((int)(sanity*0.2f))/4;
        if (sanity > 0) {
            sanityLevel++;
        }
        //Debug.Log("Sanity level: " + sanityLevel + ", " + sanity);
    }
    private void setSanityEffects() {
        playSanitySound();
        switch (sanityLevel) {
            case 0:
                foreach (GameObject obj in sanityObjs) {
                    if (obj.name == "sanity_effect") {
                        obj.GetComponent<Image>().sprite = sanity_sprite_5;
                        obj.GetComponent<Image>().color = new Color(0, 0, 0, 1f);
                    }
                }
                gameOver();
                break;
            case 1:
                foreach (GameObject obj in sanityObjs) {
                    if (obj.name == "sanity_effect") {
                        obj.GetComponent<Image>().sprite = sanity_sprite_1;
                        obj.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    }
                }
                break;
            case 2:
                foreach (GameObject obj in sanityObjs) {
                    if (obj.name == "sanity_effect") {
                        obj.GetComponent<Image>().sprite = sanity_sprite_2;
                        obj.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    }
                }
                break;
            case 3:
                foreach (GameObject obj in sanityObjs) {
                    if (obj.name == "sanity_effect") {
                        obj.GetComponent<Image>().sprite = sanity_sprite_3;
                        obj.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    }
                }
                break;
            case 4:
                foreach (GameObject obj in sanityObjs) {
                    if (obj.name == "sanity_effect") {
                        obj.GetComponent<Image>().sprite = sanity_sprite_4;
                        obj.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    }
                }
                break;
            case 5:
                foreach (GameObject obj in sanityObjs) {
                    if (obj.name == "sanity_effect") {
                        obj.GetComponent<Image>().sprite = sanity_sprite_5;
                        obj.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    }
                }
                break;
            default:
                break;
        }
    }
    private void gameOver() { Debug.Log("You Died!"); }

    private void playSanitySound() {
        playerAudio.clip = heartBeatSound;
        switch (sanityLevel) {
            case 0:
                if (!playerAudio.isPlaying) {
                    if (!dead) {
                        playerAudio.PlayOneShot(deathSound);
                        dead = true;
                    }
                }
                break;
            case 1:
                if (!playerAudio.isPlaying) {
                    playerAudio.Play();
                    Debug.Log("Playing sanity level 1");
                }
                break;
            case 2:
                interwall = 0.5f;
                if (!playerAudio.isPlaying) {
                    timer += Time.deltaTime;
                    if (timer > interwall) {
                        playerAudio.Play();
                        timer = 0;
                    }
                    Debug.Log("Playing sanity level 2");
                }
                break;
            case 3:
                interwall = 1f;
                if (!playerAudio.isPlaying) {
                    timer += Time.deltaTime;
                    if (timer > interwall) {
                        playerAudio.Play();
                        timer = 0;
                    }
                    Debug.Log("Playing sanity level 3");
                }
                break;
            case 4:
                interwall = 2f;
                if (!playerAudio.isPlaying) {
                    timer += Time.deltaTime;
                    if (timer > interwall) {
                        playerAudio.Play();
                        timer = 0;
                    }
                    Debug.Log("Playing sanity level 4");
                }
                break;
            case 5:
                break;
            default:
                break;
        }
    }

}
