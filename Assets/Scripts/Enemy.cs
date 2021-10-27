using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public Vector2 speedMinMax;
    private float _halfHeight; 
    [SerializeField]
    private float speed;

    private void Start()
    {
        _halfHeight = Camera.main.orthographicSize;
        speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, Difficulty.GetDifficultyPercent());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * (speed * Time.deltaTime));
        if (IsOutOfScreen())
        {
            Destroy(gameObject);
        }
    }

    private bool IsOutOfScreen()
    {
        return -_halfHeight > transform.position.y + transform.localScale.y;
    }
}
