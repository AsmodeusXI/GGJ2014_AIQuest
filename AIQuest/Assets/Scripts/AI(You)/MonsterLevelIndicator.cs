using UnityEngine;
using System.Collections.Generic;



class MonsterLevelIndicator : MonoBehaviour
{
    public List<Sprite> spriteIndicators;
    public float durationPop = .5f;
    public float popScale = 1.3f;

    private SpriteRenderer thisRenderer;

    private int index = -1;
    private float timerProgress = 0;
    private float currentScale;

    public void Start()
    {
        thisRenderer = this.GetComponent<SpriteRenderer>();
        thisRenderer.sprite = spriteIndicators[0];
        thisRenderer.enabled = false;
    }

    private void RestartCount()
    {
        timerProgress = 0;
        index = -1;
        thisRenderer.sprite = spriteIndicators[0];
    }

    public void increment()
    {
        thisRenderer.enabled = true;

        index++;
        if (index >= spriteIndicators.Count)
            index = spriteIndicators.Count - 1;
        thisRenderer.sprite = spriteIndicators[index];
        this.transform.localScale = new Vector3(popScale, popScale, popScale);
    }

    public void hide()
    {
        thisRenderer.enabled = false;
        RestartCount();

    }

    public void unhide()
    {
        thisRenderer.enabled = true;
        index = 0;
    }

    public void Update()
    {
        if (timerProgress < durationPop)
            timerProgress += Time.deltaTime;

        currentScale = Mathf.Lerp(popScale,1, timerProgress/durationPop);
        this.gameObject.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }
}

