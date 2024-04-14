using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public void Attack()
    {

        //Collider2D[] hit = Physics2D.OverlapBoxAll(Slash_Zone.position, Slash_Size, 0);//collider array to hit multiple enemies at once

        //if (mouse_direction == 0 && Allow_Slash == true)//change the slash collider pos according to direction
        //{
        //    Slash_Zone.position = new Vector2(MC_Pos.position.x, MC_Pos.position.y - 0.1f);
        //    Slash_Size.x = Slash_Size_X;
        //    Slash_Size.y = Slash_Size_Y;
        //}
        //else if (mouse_direction == 1 && Allow_Slash == true)
        //{
        //    Slash_Zone.position = new Vector2(MC_Pos.position.x, MC_Pos.position.y + 1.65f);
        //    Slash_Size.x = Slash_Size_X;
        //    Slash_Size.y = Slash_Size_Y;
        //}
        //else if (mouse_direction == 2 && Allow_Slash == true)
        //{
        //    Slash_Zone.position = new Vector2(MC_Pos.position.x + 1f, MC_Pos.position.y + 0.65f);
        //    Slash_Size.x = Slash_Size_Y + 0.5f;
        //    Slash_Size.y = Slash_Size_X;
        //}
        //else if (mouse_direction == 3 && Allow_Slash == true)
        //{
        //    Slash_Zone.position = new Vector2(MC_Pos.position.x - 1f, MC_Pos.position.y + 0.65f);
        //    Slash_Size.x = Slash_Size_Y + 0.5f;
        //    Slash_Size.y = Slash_Size_X;
        //}
    }

    void FindMouseDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector3 pos = transform.InverseTransformPoint(mousePos);
        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

        //if (angle >= -150 && angle <= -30)
        //    mouse_direction = 0;
        //else if (angle <= 150 && angle > 30)
        //    mouse_direction = 1;
        //else if (angle >= 150 && angle <= 180)
        //    mouse_direction = 3;
        //else if (angle <= 30 && angle >= -30)
        //    mouse_direction = 2;
    }
}

public interface IHitable
{ 
    public void Hit();
}