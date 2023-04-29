using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public float cooldDownd;
    [HideInInspector] public bool isCoolDown;
    private Image shieldImage;
    private Player player;
    
    void Start()
    {
        shieldImage = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        isCoolDown = true;
        
    }

    void Update()
    {
        if(isCoolDown)
        {
            shieldImage.fillAmount -= 1 / cooldDownd * Time.deltaTime;
            if(shieldImage.fillAmount <= 0)
            {
                shieldImage.fillAmount = 1;
                isCoolDown = false;
                player.shield.SetActive(false);
                gameObject.SetActive(false);    
            }    
        }
    }

    public void ResetTimer()
    {
        shieldImage.fillAmount = 1; 
    }
}
