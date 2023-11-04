using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
   public Camera camera;
   private float xRotation = 0f;
   public float xSensivity = 25f;
   public float ySensivity = 25f;

   public void ProcessLook(Vector2 input)
   {
      float mouseX = input.x;
      float mouseY = input.y;
      //up and down
      xRotation -= (mouseY * Time.deltaTime) * ySensivity;
      xRotation = Mathf.Clamp(xRotation, -75f, 75f);
      
      camera.transform.localRotation = Quaternion.Euler(xRotation,0,0);
      //left and right
      transform.Rotate(Vector3.up * (mouseX * Time.deltaTime * xSensivity));
   }
}
