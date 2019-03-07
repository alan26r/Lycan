using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    // Ne pas mettre dans la scène ce créé automatiquement
    public event System.Action<Player_Tps_Script> OnLocalPlayerJoined;
    GameObject gameObject;

#region Instance
    static GameManager m_Instance;
    public static GameManager Instance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = new GameManager();
                m_Instance.gameObject = new GameObject("_gameManager");
                m_Instance.gameObject.AddComponent<InputController_Script>();
                m_Instance.gameObject.AddComponent<Timer>();
                m_Instance.gameObject.AddComponent<Respawner>();
            }
            return m_Instance;
        }
    }
#endregion

#region Input Controller
    InputController_Script m_InputController;
    public InputController_Script InputController
    {
        get
        {
            if(m_InputController == null)
            {
                m_InputController = gameObject.GetComponent<InputController_Script>();
            }
            return m_InputController;
        }
    }
#endregion

#region Local Player
    Player_Tps_Script m_LocalPlayer;
    public Player_Tps_Script LocalPlayer
    {
        get
        {
            return m_LocalPlayer;
        }
        set
        {
            m_LocalPlayer = value;
            if(OnLocalPlayerJoined != null)
            {
                OnLocalPlayerJoined(m_LocalPlayer);
            }
        }
    }
    #endregion

#region Timer
    Timer m_Timer;
    public Timer timer
    {
        get
        {
            if(m_Timer == null)
            {
                m_Timer = gameObject.GetComponent<Timer>();
            }
            return m_Timer;
        }
    }
    #endregion

#region Respawner
    Respawner m_Respawner;
    public Respawner Respawner
    {
        get
        {
            if(m_Respawner == null)
            {
                m_Respawner = gameObject.GetComponent<Respawner>();
            }
            return m_Respawner;
        }
    }
#endregion
}