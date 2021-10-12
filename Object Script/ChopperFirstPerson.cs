using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using System.IO;
using System.Reflection;
using System;


/// <summary>
/// A first person helicopter controller, 
/// using keyboard to control a helicopter model, 
/// a w s d for horizontal moving, 
/// q e for horizontal turning. 
/// z x for vertical turning up and down. 
/// left shift and space for descending and ascending
/// the helicopter model's rotor should be rotating with the rotate rotor script.
/// And it can be combined with shootPrefab script to launch weapon
/// 
/// </summary>
namespace UnityStandardAssets.Characters.FirstPerson
{

    public class ChopperFirstPerson : MonoBehaviour
    {
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] public int speed = 2;

        public GameObject helicopter;

        private void Start()
        {
            //m_Camera = Camera.main;
            m_MouseLook.Init(transform, helicopter.transform);
           
        }

        /*
        private IEnumerator Screenshot()
        {
            while( true)
            {
                m_Camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
                RenderTexture currentRT = RenderTexture.active;
                RenderTexture.active = m_Camera.targetTexture;
                m_Camera.Render();
                Texture2D texture = new Texture2D(m_Camera.targetTexture.width, m_Camera.targetTexture.height, TextureFormat.RGB24, false);
                texture.ReadPixels(new Rect(0, 0, m_Camera.targetTexture.width, m_Camera.targetTexture.height), 0, 0);
                texture.Apply();
                RenderTexture.active = currentRT;
                
                Color[] textureColors = texture.GetPixels();
                for (int i = 0; i < 1000; i++)
                {
                    textureColors[i] = Color.black;
                }
                texture.SetPixels(textureColors);
                
                yield return new WaitForSeconds(1);
            }
        }


    */
        void Update()
        {
            //RotateView();

            int lateral = 0;
            int forward = 0;
            int upward = 0;

            int turn = 0;
            int nose = 0;

            if (Input.GetKeyDown(KeyCode.R))
            {
                speed = 2 - (speed + 1) % 2;
            }
            if (Input.GetKey(KeyCode.A))
            {
                lateral += speed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                lateral -= speed;
            }
            if (Input.GetKey(KeyCode.W))
            {
                forward -= speed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                forward += speed;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                upward += speed;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                upward -= speed;
            }

            if (Input.GetKey(KeyCode.E))
            {
                turn += speed;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                turn -= speed;
            }


            if (Input.GetKey(KeyCode.X))
            {
                nose += speed;
            }

            if (Input.GetKey(KeyCode.Z))
            {
                nose -= speed;
            }




            transform.Translate(Vector3.right * lateral * 0.05f);
            transform.Translate(Vector3.forward * forward * 0.1f);
            transform.Translate(Vector3.up * upward * 0.05f);

            transform.Rotate(Vector3.up*turn*0.2f);
            transform.Rotate(Vector3.right * nose * 0.05f);


        }

        
        private void FixedUpdate()
        {
            m_MouseLook.UpdateCursorLock();
        }
        

        private void RotateView()
        {
            
            //m_MouseLook.LookRotation(helicopter.transform,transform);
           

        }
    }


}