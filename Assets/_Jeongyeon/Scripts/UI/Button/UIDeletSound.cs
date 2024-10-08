using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIDeletSound : MonoBehaviour
{
    private Button deleteButton;
    private void Awake()
    {
        deleteButton = GetComponent<Button>(); 
    }

    private void Start()
    {
        deleteButton.onClick.AddListener(DeleteSound);
    }

    private void DeleteSound()
    {
        SoundManager.Instance.DeleteSound();
    }
}
