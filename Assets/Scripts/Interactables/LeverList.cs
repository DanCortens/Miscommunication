using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverList : MonoBehaviour
{
    public string activeLevers;
    public string key;
    public bool solved;
    [SerializeField] private AudioSource completeSound;

    private void Start()
    {
        activeLevers = "";
    }
    public void LeverTurned(string lever)
    {
        activeLevers += lever;
        if (activeLevers.Length == key.Length)
        {
            if (activeLevers.Equals(key))
            {
                solved = true;
                completeSound.Play();
            }
            else
                activeLevers = "";
        }
    }
}
