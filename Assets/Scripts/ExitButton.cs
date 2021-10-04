using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private Button _button;

    // Start is called before the first frame update
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    void Start()
    {
        _button.onClick.AddListener(Application.Quit);
    }
}
