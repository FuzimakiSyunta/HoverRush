using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControll : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;

    //select
    private SelectorMenu selectorMenuScript;
    public GameObject selectMenu;

    // �v���C���[��ϐ��Ɋi�[
    public GameObject Player;
    // ��]������X�s�[�h
    private float rotateSpeed = 20.0f;
    float angle;

    // �J�����̏����I�t�Z�b�g
    private Vector3 cameraUPBackOffset = new Vector3(0, 2.8f, 9.02f);
    // �J�����̌���I�t�Z�b�g
    public Vector3 cameraBackOffset = new Vector3(0, 1, -200.0f);

    // �J�����̌���I�t�Z�b�g
    private Vector3 cameraRightBackOffset = new Vector3(2.48f, 1.95f, 3.77f);


    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        selectorMenuScript = selectMenu.GetComponent<SelectorMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���Ԉˑ��̈ړ�
        if (gameManagerScript.IsGameStart() == false)
        {
            // Time.deltaTime ���|���Ď��Ԉˑ��̃X�s�[�h��
            angle = rotateSpeed * Time.deltaTime;
            // �v���C���[�ʒu���
            Vector3 playerPos = Player.transform.position;
            // �J��������]������
            transform.RotateAround(playerPos, Vector3.up, angle);
        }
        //�Q�[���X�^�[�g�{�^����������
        if (selectorMenuScript.IsStartButtonFlag() == true)
        {
            // �v���C���[�̕�������
            Vector3 direction = Player.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);

            // ����Ɉړ�
            Vector3 targetPosition = Player.transform.position + cameraUPBackOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 2.0f);
        }
        //�Q�[���X�^�[�g�{�^����������
        if (selectorMenuScript.IsColorMenuFlag() == true)
        {
            // �v���C���[�̕�������
            Vector3 direction = Player.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
            // �E����Ɉړ�
            Vector3 targetPosition = Player.transform.position + cameraRightBackOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 2.0f);
        }
        //�V�[�����ڂ�����
        if (selectorMenuScript.IsSeaneEffectFlag() == true)
        {
            // �v���C���[�̕�������
            Vector3 direction = Player.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);

            // Z�����Ɍ�ނ�������
            StartCoroutine(MoveBackForSeconds(2.0f));
        }

        IEnumerator MoveBackForSeconds(float duration)
        {
            float elapsedTime = 0.0f;
            Vector3 startPosition = transform.position;
            Vector3 endPosition = startPosition + new Vector3(0, 0, -20.0f); // Z�����ɉ�����ʂ�ݒ�

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = endPosition; // �ŏI�ʒu�𐳊m�ɐݒ�
        }


    }
}