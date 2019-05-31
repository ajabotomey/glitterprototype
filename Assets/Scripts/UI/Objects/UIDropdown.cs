using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDropdown : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;

    // Start is called before the first frame update
    void Awake()
    {
        dropdown.ClearOptions();
    }

    public void SetOptions<T>(T[] options)
    {

    }
}
