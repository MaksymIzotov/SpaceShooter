using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthTextUpdate : MonoBehaviour
{
    public static HealthTextUpdate Instance;

    private void Awake() => Instance = this;

    public void HealthUpdate(int health) => GetComponent<TMP_Text>().text = "Health: " + health;
}
