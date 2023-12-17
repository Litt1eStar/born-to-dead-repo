using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public Player player;
    public PlayerProfileUI profile;
    public InventoryController inventory;
    public Transform damagePopup;

    public GameObject playerObj;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
    }
}
