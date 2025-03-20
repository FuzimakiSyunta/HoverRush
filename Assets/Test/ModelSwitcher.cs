using UnityEngine;

public class CubeSwitcher : MonoBehaviour
{
    public GameObject CubeModel1; // Cube1���f��
    public GameObject CubeModel2; // Cube2���f��

    private bool isCube1Active = true; // ���݂̃A�N�e�B�u���

    void Update()
    {
        // �㉺�L�[�Ő؂�ւ�
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ToggleCubes();
        }
    }

    void ToggleCubes()
    {
        // ���݂�Cube��؂�ւ�
        isCube1Active = !isCube1Active;

        CubeModel1.SetActive(isCube1Active);
        CubeModel2.SetActive(!isCube1Active);
    }
}
