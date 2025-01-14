using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonRotation : MonoBehaviour
{
    public Vector3 _maxRotation; //Rotación Maxima
    public Vector3 _minRotation; //Rotación Minima
    private float offset = -51.6f; 
    public GameObject ShootPoint; //Punto de disparo
    public GameObject Bullet; //Bala
    public float ProjectileSpeed = 0; //Velocidad del proyectil
    public float MaxSpeed; //Velocidad Maxima
    public float MinSpeed; //Velocidad Minima
    public GameObject PotencyBar; //Barra de potencia
    private float initialScaleX; 

    private void Awake()
    {
        initialScaleX = PotencyBar.transform.localScale.x;
    }
    void Update()
    {
        //PISTA: mireu TOTES les variables i feu-les servir

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//guardem posici� del ratol� a la c�mera
        var direction = mousePos - transform.position;
        var angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);

        if (Input.GetMouseButton(0))
        {
            if (ProjectileSpeed <=  MaxSpeed){ 
                ProjectileSpeed += Time.deltaTime*4; //Se va actualizando cada segundo
            }

            if (ProjectileSpeed > MaxSpeed){ //Si la velocidad es maxima
                ProjectileSpeed = MaxSpeed; 
            }

            if (ProjectileSpeed < MinSpeed){ //Si la velocidad no es la velocidad minima 
                ProjectileSpeed = MinSpeed;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            var projectile = Instantiate(Bullet, ShootPoint.transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = (direction * ProjectileSpeed); 
            ProjectileSpeed = 0f;
        }
        CalculateBarScale();

    }
    public void CalculateBarScale()
    {
        PotencyBar.transform.localScale = new Vector3(Mathf.Lerp(0, initialScaleX, ProjectileSpeed / MaxSpeed),
            PotencyBar.transform.localScale.y,
            PotencyBar.transform.localScale.z);
    }
}
