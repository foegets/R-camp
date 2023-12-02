using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    // Start is called before the first frame update
    public Text healthText;
    public static int currentHealth;
    public static int maxHealth;

    private Image healthBar;

    void Start()
    {
        healthBar = GetComponent<Image>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        healthBar.fillAmount = (float)currentHealth/(float)maxHealth;
        healthText.text = currentHealth.ToString()+"/"+maxHealth.ToString();
    }
}
