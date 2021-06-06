﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : MonoBehaviour
{
    #region Variables

    [Header("UI")]

    [SerializeField] private Button continueButton;
    [SerializeField] private Button exitButton;

   
    #endregion

    #region Events

    public static event Action OnClosed;

    #endregion

    #region Unity lifecycle

    void Start()
    {
        continueButton.onClick.AddListener(ContinueClickHandler);
        exitButton.onClick.AddListener(ExitClickHandler);
    }

    #endregion

    #region Event Handlers

    private void ContinueClickHandler()
    {
        
        OnClosed?.Invoke();
    }

    private void ExitClickHandler()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    #endregion

    #region Private methods

    #endregion
}
