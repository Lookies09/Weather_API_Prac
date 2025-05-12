using UnityEngine;

public class Background_Move : MonoBehaviour
{
    [SerializeField] private float speed = 2f;  // 배경이 이동하는 속도
    [SerializeField] private SpriteRenderer[] backgroundSprites; // 여러 배경 스프라이트 배열
    private float width;      // 배경 스프라이트의 너비
    private Vector3 startPos; // 배경의 시작 위치

    void Start()
    {
        // 첫 번째 스프라이트의 크기를 얻고, 시작 위치를 저장합니다.
        if (backgroundSprites.Length > 0)
        {
            width = backgroundSprites[0].bounds.size.x;  // 첫 번째 스프라이트의 가로 크기
            startPos = backgroundSprites[0].transform.position;
        }
    }

    void Update()
    {
        // 모든 스프라이트를 왼쪽으로 이동시킵니다.
        foreach (var sprite in backgroundSprites)
        {
            sprite.transform.position += Vector3.left * speed * Time.deltaTime;
        }

        // 배경 스프라이트가 화면을 벗어나면 위치를 재설정하여 이어지도록 합니다.
        if (backgroundSprites[0].transform.position.x <= startPos.x - width)
        {
            ResetBackgroundPositions();
        }
    }

    private void ResetBackgroundPositions()
    {
        // 모든 배경 스프라이트를 초기 위치로 되돌립니다.
        foreach (var sprite in backgroundSprites)
        {
            sprite.transform.position = startPos;
        }
    }
}