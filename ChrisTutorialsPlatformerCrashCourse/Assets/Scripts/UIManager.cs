using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    Canvas _gameCanvas;

    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;

    private void Awake()
    {
        _gameCanvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable()
    {
        CharacterEvents.CharacterDamaged += CharacterTookDamage;
        CharacterEvents.CharacterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharacterEvents.CharacterHealed -= CharacterHealed;
        CharacterEvents.CharacterDamaged -= CharacterTookDamage;
    }

    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text text = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, _gameCanvas.transform)
            .GetComponent<TMP_Text>();
        text.text = damageReceived.ToString();
    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text text = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, _gameCanvas.transform)
            .GetComponent<TMP_Text>();
        text.text = healthRestored.ToString();
    }
}
