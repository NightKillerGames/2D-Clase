using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : AbstractEnemy{

    private bool verticalPatrol = false;

    public override void Update()
    {
        CheckIsDead();
        ator2.SetBool("Muerto", muerto);
        if (muerto)
        {
            return;
        }
        checkPlayerPosition();
        Move();
        Patrol();
    }

    public override void Move()
    {
        if(InitialPosition.x == FinalPosition.x)
        {
            verticalPatrol = true;
        }
        gameObject.transform.position = Vector2.Lerp(InitialPosition, FinalPosition, _currentPatrolTime);
    }
    public override void Patrol()
    {
        if (_direction)
        {
            _currentPatrolTime += Time.deltaTime * Speed;
        }
        else
        {
            _currentPatrolTime -= Time.deltaTime * Speed;
        }
        if (_currentPatrolTime >= 1)
        {
            _currentPatrolTime = 1;
            _direction = !_direction;
        }
        else if (_currentPatrolTime <= 0)
        {
            _currentPatrolTime = 0;
            _direction = !_direction;
        }
    }
    private void checkPlayerPosition()
    {
        if(pc.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, -360, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }
}
