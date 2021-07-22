using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public float initialVelocityY = 200.0f; //Y�����ɑł��o�����x
    public AudioClip brickHit; //�u���b�N�ɓ����������̉�
    public AudioClip wall_paddleHit; //�ǂ�Paddle�ɓ����������̉�
    public Text countDownText; //�J�E���g�_�E����\������e�L�X�g

    private Rigidbody2D rb2D; //RigidBody2D������ϐ�
    private AudioSource audioSource; //AudioSource������ϐ�
    private float initialVelocityX; //X�����ɑł��o�����x
    private int twoNumber; //�ł��o�������������E�����߂�ϐ�
    private float countDown = 3.0f; //�J�E���g�_�E������ϐ�
    private int stateNumber = 0; //�X�e�[�g�i���o�[
    //private bool isCalled = false; //1�񂾂����s���邽�߂̔���

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); //RigidBody2D������
        audioSource = GetComponent<AudioSource>(); //AudioSource������
        countDownText.text = "3"; //3��\������

        twoNumber = Random.Range(0, 2); //0��1��twoNumber�ɓ����

        switch (twoNumber)
        {
            case 0: //�������ɑł��o����
                {
                    initialVelocityX = Random.Range(-initialVelocityY * 2, -initialVelocityY / 2); //-20����-5�͈̔͂Œ��I����
                }break;

            case 1: //�E�����ɑł��o����
                {
                    initialVelocityX = Random.Range(initialVelocityY / 2, initialVelocityY * 2); //5����20�͈̔͂Œ��I����
                }break;

            default: break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime; //���Ԃ̍X�V

        switch (stateNumber) //�X�e�[�g�}�V��
        {
            case 0: //�ҋ@
                {
                    if (countDown < 0.0f)
                    {
                        countDownText.text = ""; //�J�E���g�_�E��������

                        stateNumber = 3; //case3�ɍs��
                    }
                    else if (countDown < 1.0f)
                        stateNumber = 1; //case1�ɍs��

                    else if (countDown < 2.0f)
                        stateNumber = 2; //case2�ɍs��
                }
                break;

            case 1:
                {
                    countDownText.text = "1"; //1��\������

                    stateNumber = 0; //case0�ɖ߂�
                }
                break;

            case 2:
                {
                    countDownText.text = "2"; //2��\������

                    stateNumber = 0; //case0�ɖ߂�
                }
                break;

            case 3: //�Q�[���������Ă���Ƃ�
                {
                    if (!SystemDaemon.isGameStarted)
                    {
                        transform.parent = null; //�e�q�֌W��؂�
                        rb2D.isKinematic = false; //iskinematic�̃`�F�b�N���O��

                        rb2D.AddForce(new Vector2(initialVelocityX, initialVelocityY)); //�{�[���ɗ͂������ē�����

                        SystemDaemon.isGameStarted = true; //�Q�[���𓮂���
                    }
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Brick")
        {
            audioSource.clip = brickHit; //brickHit�̉����Z�b�g����
            audioSource.Play(); //�����Đ�����
            Destroy(collision.gameObject); //�u���b�N����
        }
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Paddle")
        {
            audioSource.clip = wall_paddleHit; //Wall_PaddleHit�̉����Z�b�g����
            audioSource.Play(); //�����Đ�����
        }
    }
}