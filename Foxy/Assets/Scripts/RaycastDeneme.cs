using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDeneme : MonoBehaviour
{
	public static RaycastDeneme instance;

	public LineRenderer rayLine;

	private float rayAngle;

	 
	public float raySpeed = 3f,distance;

	// lazerin içinden geçmesin sýnýr olduðunu bilsin dedðimiz katmanlar(ground,platform..vb)
	public LayerMask rayCollisionMask;

	// lazerin ucunda görünecek hedef imgesi
	public SpriteRenderer targetSprite;

	private SpriteRenderer playerSprite;


	public bool  isTransmission, isPossessed, isEnabled=false;

	public bool isPlayer = true;
	public RobotController roboComponent;
	public RobotHealthController robotHealthController;

    public void Awake()
    {
		instance = this;
    }

    void Start()
    {
		playerSprite = GetComponent<SpriteRenderer>();    //Sprite Renderer i alacak
	}

	// Update is called once per frame
	void Update()
	{
		if (isPlayer)
		{

			// havadayken basýp inputu durdurmasýn
			if (Input.GetKeyDown(KeyCode.T) && PlayerController.instance.isGrounded  )
			{
				PlayerController.instance.rb2D.velocity = new Vector2(0,0) ;
				PlayerController.instance.stopInput = !isTransmission;
				this.rayLine.enabled = !isTransmission;
				isTransmission = !isTransmission;

			}


			if (isTransmission)
			{
				Vector2 raySource = this.transform.position;
				Vector2 vector = new Vector2(Mathf.Cos(this.rayAngle), Mathf.Sin(this.rayAngle));
				float currentDistance = 100f;
				RaycastHit2D hit = Physics2D.Raycast(raySource, vector, currentDistance, this.rayCollisionMask);
				Vector2 v;



				if (hit.collider)
				{
					Debug.Log("raycast hit");
					v = hit.point;
					currentDistance = hit.distance;
					this.UpdateRayAngle(currentDistance);
				}
				else
				{
					v = raySource + currentDistance * vector;
					UpdateRayAngle(distance);

				}

				this.rayLine.SetPosition(0, raySource);
				this.rayLine.SetPosition(1, v);
				this.targetSprite.transform.position = Vector3.Lerp(this.targetSprite.transform.position, v, 125f);

				// lazeri takip ederken
				CameraController.instance.target = targetSprite.transform;
				CameraController.instance.SetCameraSize(8);
				targetSprite.color = new Color(targetSprite.color.r, targetSprite.color.g, targetSprite.color.b, 1f);



				roboComponent = hit.transform.GetComponent<RobotController>();
				robotHealthController = hit.transform.GetComponent<RobotHealthController>();
				if (roboComponent != null)
				{
					Debug.Log("Robot Controller");
					this.targetSprite.enabled = true;

					rayLine.startColor = Color.green;
					rayLine.endColor = Color.green;



					// robota geçildiðinde
					if (Input.GetKeyDown(KeyCode.P))
					{
						Debug.Log("Robot Active");
						isPossessed = true;
						isTransmission = false;
					}
				}
				else
				{
					rayLine.startColor = Color.red;
					rayLine.endColor = Color.red;
				}


			}
			else if (isPossessed)
			{
				CameraController.instance.target = roboComponent.transform;
				CameraController.instance.SetCameraSize(5);
				targetSprite.color = new Color(targetSprite.color.r, targetSprite.color.g, targetSprite.color.b, 0f);
				this.rayLine.enabled = false;
				isPlayer = false;
				roboComponent.isRobotActive = true;

				PlayerController.instance.rb2D.bodyType = RigidbodyType2D.Static;
				roboComponent.rb2D.bodyType = RigidbodyType2D.Dynamic;

			}
			else
			{
				// lazer kapandýðýnda
				CameraController.instance.target = this.transform;
				CameraController.instance.SetCameraSize(5);
				targetSprite.color = new Color(targetSprite.color.r, targetSprite.color.g, targetSprite.color.b, 0f);
			}

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
			float num3 = num2 - this.raySpeed * Time.deltaTime * Mathf.Sign(num2) / Mathf.Min(distance, 10f);
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
