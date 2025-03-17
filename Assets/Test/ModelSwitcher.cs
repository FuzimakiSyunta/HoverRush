using UnityEngine;

public class CubeSwitcher : MonoBehaviour
{
    public GameObject CubeModel1; // Cube1モデル
    public GameObject CubeModel2; // Cube2モデル

    private bool isCube1Active = true; // 現在のアクティブ状態

    void Update()
    {
        // 上下キーで切り替え
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ToggleCubes();
        }
    }

    void ToggleCubes()
    {
        // 現在のCubeを切り替え
        isCube1Active = !isCube1Active;

        CubeModel1.SetActive(isCube1Active);
        CubeModel2.SetActive(!isCube1Active);
    }
}
