using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
  public PlayerController controller;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    transform.Rotate(20 * Time.deltaTime, 0, 0);
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      controller.IncreaseScore();
      Destroy(gameObject);
    }
  }

}
