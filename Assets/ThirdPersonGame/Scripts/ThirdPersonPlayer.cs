using TMPro;
using Unity.Netcode;
using UnityEngine;

/*
public struct MyType : INetworkSerializeByMemcpy
{
    public int a;
    public Vector2 b;
}
*/

public class ThirdPersonPlayer : NetworkBehaviour
{
    [SerializeField] GameObject spawnedPrefab;
	public NetworkVariable<int> number = new(42, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
	// public NetworkVariable<MyType> testVariable = new(new MyType(), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

	[SerializeField] Rigidbody rb;
    [SerializeField] float walkMaxSpeed = 2.0f;
    [SerializeField] float runMaxSpeed = 6.0f;
    [SerializeField] float acceleration = 3.0f;
    [SerializeField] float deceleration = 5.0f;

	[SerializeField] float maxAngularSpeed;

	[SerializeField] TMP_Text numberText;

	Transform cameraTransform;

    public override void OnNetworkSpawn()
	{
        if (IsOwner)
        {
            ThirdPersonCamera cam = FindAnyObjectByType<ThirdPersonCamera>();
            cam.target = transform;
            cameraTransform = cam.transform;
        }
        number.OnValueChanged += NumberChanged;
	}

    void NumberChanged(int previousValue, int newValue)
    {
        numberText.text = newValue.ToString();    
    }

	void Update()
	{
        if (IsOwner)
        {
            if (Input.GetKeyDown(KeyCode.I))
                number.Value++;

			if (Input.GetKeyDown(KeyCode.Alpha0))
				FindObjectOfType<NetworkUI>().SendUIMessage("");

			if (Input.GetKeyDown(KeyCode.Alpha1))
				TestServerRpc(OwnerClientId.ToString());

			if (Input.GetKeyDown(KeyCode.Alpha2))
				TestClientRpc();

			if (Input.GetKeyDown(KeyCode.Alpha3))
				SpawnServerRpc(transform.position);

			if (Input.GetKeyDown(KeyCode.Alpha4))
				DestroyServerRpc();

		}
	}

	void FixedUpdate()
    {
        if (!IsOwner) return;


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool run = Input.GetAxis("Run") != 0;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        Vector3 inputDir = forward * z + right * x;
        inputDir.y = 0;
        inputDir.Normalize();

        Vector3 velocity = rb.velocity;
         

        if (inputDir == Vector3.zero) // LASSULÁS
        {
            velocity = Vector3.MoveTowards(velocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
        }
        else // GYORSULÁS
        { 
            float maxSpeed = run ? runMaxSpeed : walkMaxSpeed;
            velocity = Vector3.MoveTowards(velocity, inputDir * maxSpeed, acceleration * Time.fixedDeltaTime); 
        }

        rb.velocity = velocity;

        if (inputDir != Vector3.zero) 
        {
            Quaternion targetRotaion = Quaternion.LookRotation(inputDir);

            float angle = maxAngularSpeed * Time.fixedDeltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotaion, angle);
        }

    }


	[ServerRpc]
	void TestServerRpc(string clientName)
	{
		FindObjectOfType<NetworkUI>().SendUIMessage("Hello! Runs On Server. Sender: " + clientName);
	}

	[ClientRpc]
	void TestClientRpc()
	{
		FindObjectOfType<NetworkUI>().SendUIMessage("Hello! I'm a Client");
	}

    GameObject lastCreated;

	[ServerRpc]
	void SpawnServerRpc(Vector3 position)
	{
		lastCreated = Instantiate(spawnedPrefab);
		lastCreated.GetComponent<NetworkObject>().Spawn(true);
		lastCreated.transform.position = position;

	}

	[ServerRpc]
    void DestroyServerRpc()
	{
        Destroy(lastCreated);
		// lastCreated.GetComponent<NetworkObject>().Despawn();
	}
}

