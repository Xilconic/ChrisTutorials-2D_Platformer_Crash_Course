using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public void OnExitGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
#if (Unity_Editor || DEVELOPMENT_BUILD)
            Debug.Log($"{this.name} : {nameof(UIManager)} : {nameof(OnExitGame)}");
#endif

#if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
#elif(UNITY_STANDALONE)
            Application.Quit();
#endif
        }
    }
}
