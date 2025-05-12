using UnityEngine;

public class Background_Move : MonoBehaviour
{
    [SerializeField] private float speed = 2f;  // ����� �̵��ϴ� �ӵ�
    [SerializeField] private SpriteRenderer[] backgroundSprites; // ���� ��� ��������Ʈ �迭
    private float width;      // ��� ��������Ʈ�� �ʺ�
    private Vector3 startPos; // ����� ���� ��ġ

    void Start()
    {
        // ù ��° ��������Ʈ�� ũ�⸦ ���, ���� ��ġ�� �����մϴ�.
        if (backgroundSprites.Length > 0)
        {
            width = backgroundSprites[0].bounds.size.x;  // ù ��° ��������Ʈ�� ���� ũ��
            startPos = backgroundSprites[0].transform.position;
        }
    }

    void Update()
    {
        // ��� ��������Ʈ�� �������� �̵���ŵ�ϴ�.
        foreach (var sprite in backgroundSprites)
        {
            sprite.transform.position += Vector3.left * speed * Time.deltaTime;
        }

        // ��� ��������Ʈ�� ȭ���� ����� ��ġ�� �缳���Ͽ� �̾������� �մϴ�.
        if (backgroundSprites[0].transform.position.x <= startPos.x - width)
        {
            ResetBackgroundPositions();
        }
    }

    private void ResetBackgroundPositions()
    {
        // ��� ��� ��������Ʈ�� �ʱ� ��ġ�� �ǵ����ϴ�.
        foreach (var sprite in backgroundSprites)
        {
            sprite.transform.position = startPos;
        }
    }
}