using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
  void Start()
{
    DontDestroyOnLoad(gameObject); // This prevents the Player from being destroyed on scene load
}

}
