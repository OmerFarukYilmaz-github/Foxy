using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
	public float distance=30f;
	private float vectorDirection = 1f;
	public LayerMask robotLayer;

	private float rayAngle;
	public LineRenderer rayLine;
	public GameObject robot;


	public SpriteRenderer targetSprite;
	private Vector2 targetPoisition;



	// Start is called before the first frame update
	void Start()
	{
		targetSprite.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			PlayerController.instance.stopInput = true;
			targetSprite.enabled = true;



			// raycast oyuncunun yönüne göre dösün diye yoksa hep tek yöne doðru oluyor
			if (PlayerController.instance.isFacingRight)
			{
				vectorDirection = 1f;
			}
			else
			{
				vectorDirection = -1f;
			}

			// raycast direk bizim collidera çarpýp çarptým zannetmesþin
			Physics2D.queriesStartInColliders = false;


			Vector2 vector = new Vector2(Mathf.Cos(this.rayAngle), Mathf.Sin(this.rayAngle));
			RaycastHit2D hit = Physics2D.Raycast(transform.position, vector * vectorDirection, distance, robotLayer);

			if (hit.collider)
			{

				//Debug.DrawLine(transform.position, hit.point, Color.green);
				targetPoisition = hit.point;
/*
				this.rayLine.SetPosition(0, transform.position);
				this.rayLine.SetPosition(1, hit.point);
				this.targetSprite.transform.position = Vector3.Lerp(this.targetSprite.transform.position, hit.point, 5f * Time.deltaTime);
*/


			}
			else
			{
				//Debug.DrawLine(transform.position, transform.position + new Vector3(Mathf.Cos(this.rayAngle), Mathf.Sin(this.rayAngle),0f)*distance*vectorDirection , Color.red);
				targetPoisition = transform.position + new Vector3(Mathf.Cos(this.rayAngle), Mathf.Sin(this.rayAngle), 0f) * distance * vectorDirection;
/*
				this.rayLine.SetPosition(0, transform.position);
				this.rayLine.SetPosition(1, transform.position + new Vector3(Mathf.Cos(this.rayAngle), Mathf.Sin(this.rayAngle), 0f) * distance * vectorDirection);
				this.targetSprite.transform.position = Vector3.Lerp(this.targetSprite.transform.position, hit.point, 5f * Time.deltaTime);
				*/
			}
			this.rayLine.SetPosition(0, transform.position);
			this.rayLine.SetPosition(1, hit.point);
			this.targetSprite.transform.position = Vector3.Lerp(this.targetSprite.transform.position, hit.point, 5f * Time.deltaTime);

			if (hit.collider.tag == "Robot")
			{
				Debug.Log("Raycast Robota degdi");
			}
			UpdateRayAngle(hit.distance);
			UpdateRayAngle(distance);
		}
	}






	private void UpdateRayAngle(float distance)
	{
		Debug.Log("Update Ray Angle");
		Vector2 vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		if (vector.sqrMagnitude > 0f)
		{
			float num = Mathf.Atan2(vector.y, vector.x);
			float num2 = this.rayAngle - num;
			if (num2 < -3.1415927f)
			{
				num2 += 6.2831855f;
			}
			if (num2 > 3.1415927f)
			{
				num2 -= 6.2831855f;
			}
			float num3 = num2 - 1f * Time.deltaTime * Mathf.Sign(num2) / Mathf.Min(distance, 10f);
			if (Mathf.Sign(num3) != Mathf.Sign(num2))
			{
				num3 = 0f;
			}
			this.rayAngle = num3 + num;
			if (this.rayAngle < -3.1415927f)
			{
				this.rayAngle += 6.2831855f;
			}
			if (this.rayAngle > 3.1415927f)
			{
				this.rayAngle -= 6.2831855f;
			}
		}




	}
}
