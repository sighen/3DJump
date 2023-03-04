using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    Rigidbody rigid;
    public float jumpPower = 10;
    bool isJump;
    public int itemCount;
    AudioSource audio;
    public GameManagerLogic manager;

    void Awake(){
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update(){
        if(Input.GetButtonDown("Jump") && !isJump){
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Floor"){
            isJump = false;
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Item"){
            itemCount++;
            audio.Play();
            other.gameObject.SetActive(false);
            manager.GetItem(itemCount);
        }
        else if(other.tag == "Point"){
            if(itemCount == manager.totalItemCount){
                if(manager.stage == 2){
                    SceneManager.LoadScene("Example1_0");
                }
                else{
                    SceneManager.LoadScene("Example1_" + (manager.stage+1).ToString());
                }
            }
            else{
                SceneManager.LoadScene("Example1_" + (manager.stage).ToString());
            }
        }
    }
}
