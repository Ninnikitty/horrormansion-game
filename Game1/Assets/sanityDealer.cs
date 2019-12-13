using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sanityDealer : MonoBehaviour
{

    public Image sanityBar;
    public Image sanityFx;
    float maxSanity = 100;
    float minSanity = 0;
    float sanity;
    int sanityLevel;

    public Sprite sanity_sprite_1;
    public Sprite sanity_sprite_2;
    public Sprite sanity_sprite_3;
    public Sprite sanity_sprite_4;
    public Sprite sanity_sprite_5;

    InteractionCounterScript ic;

    // Start is called before the first frame update
    void Start()
    {  
        sanity = maxSanity;
        sanityLevel = 5;
        GameObject player = GameObject.Find("player");
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
        sanityBar.transform.localScale = new Vector3(sanity/100, 1, 1);
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
        switch (sanityLevel) {
            case 0:
                sanityFx.sprite = sanity_sprite_5;
                sanityFx.color = new Color(0, 0, 0, 1f);
                gameOver();
                break;
            case 1:
                sanityFx.sprite = sanity_sprite_1;
                sanityFx.color = new Color(0, 0, 0, 1);
                break;
            case 2:
                sanityFx.sprite = sanity_sprite_2;
                sanityFx.color = new Color(0, 0, 0, 1);
                break;
            case 3:
                sanityFx.sprite = sanity_sprite_3;
                sanityFx.color = new Color(0, 0, 0, 1);
                break;
            case 4:
                sanityFx.sprite = sanity_sprite_4;
                sanityFx.color = new Color(0, 0, 0, 1);
                break;
            case 5:
                sanityFx.sprite = sanity_sprite_5;
                sanityFx.color = new Color(0, 0, 0, 0);
                break;
            default:
                break;
        }
    }
    private void gameOver() { Debug.Log("You Died!"); }

}
