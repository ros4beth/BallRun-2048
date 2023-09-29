using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BogusCollider : MonoBehaviour
{
  public PlayerController gameManager;

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      gameManager.GameOver();
    }
  }
}
