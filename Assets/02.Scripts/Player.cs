using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private GameObject[] Weapons;
    private int weaponIndex = 0;
    [SerializeField]
    private Transform ShootTransform;
    [SerializeField]
    private float ShootInterval = 0.05f;
    private float LastShotTime = 0f;
    void Update()
    {   /*float horizontalInput = Input.GetAxisRaw("Horizontal");
        //float vertcalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveTo = new Vector3(horizontalInput, 0f, 0f);
        transform.position += moveTo * moveSpeed * Time.deltaTime;*/

        /*Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= moveTo;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += moveTo;
        }*/

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float tox = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        transform.position = new Vector3(tox, transform.position.y, transform.position.z);

        if (GameManager.instance.isGameOver == false)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        if (Time.time - LastShotTime > ShootInterval)
        {
            Instantiate(Weapons[weaponIndex], ShootTransform.position, Quaternion.identity);
            LastShotTime = Time.time;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Coin")
        {
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    public void Upgrade()
    {
        weaponIndex += 1;
        if ( weaponIndex >= Weapons.Length)
        {
            weaponIndex = Weapons.Length - 1;
        }
    }
}
