using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float _speed = 5f;

    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    Time.timeScale = 0;
        //}
        float horizontal = Input.GetAxis("Horizontal");//Horizontal kaynaktan alıyoruz 
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement *= Time.deltaTime * _speed;

        transform.Translate(movement, Space.World);
        _animator.SetFloat("Horizontal", horizontal, 0.1f,Time.deltaTime);
        _animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);

    }
}
