using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    public HealthSystem(GameObject currentCharacter, int currentHealth)
    {
        health = currentHealth;
        character = currentCharacter;
    }

    GameObject character;
    public GameObject Character
    {
        get
        {
            return character;
        }
    }

    int health;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            if (value < 1)
            {
                health = 0;
            }
            else
            {
                health = value;
            }
        }
    }
}
