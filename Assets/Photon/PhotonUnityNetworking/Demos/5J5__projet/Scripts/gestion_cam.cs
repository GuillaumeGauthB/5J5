using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gestion_cam : MonoBehaviour
{
    private float distance = 1.5f; // distance de la cam du joueur
    private float height = 1f; // hauteur de la cam
    [SerializeField]
    private bool followOnStart = false;

    private float smoothSpeed = 2;
    Transform cameraTransform; // transform de la camera
    bool isFollowing; // si la cam suit
    Vector3 cameraoffset = Vector3.zero;// l'offset de la camera
    float mouseSensitivity = 5f; // sensitivite de la souris
    float yaw;
    float pitch;
    Vector3 CurrentRotation;
    Vector3 RotationSmoothVelocity;
    Vector2 PitchMinMax = new Vector2(10, 30);
    public float rotationSmoothTime = .12f;

    // Start is called before the first frame update
    void Start()
    {
        // si on suit a l'execution de la scene, appeler OnStartFollowing
        if (followOnStart)
            OnStartFollowing();

    }

    private void LateUpdate()
    {
        // si le transform de la cam n'existe pas et on doit suive, appeler OnStartFollowing
        if (cameraTransform == null && isFollowing)
            OnStartFollowing();

        // Si on doit suivre, appeler Follow()
        if (isFollowing)
            Follow();
        // ecnore si on suit
        if (isFollowing)
        {
            /*yaw += Input.GetAxis("Mouse X") * mouseSensitivity; // modifier l'axis "drift" en x
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity; // modifier l'axis "drift" en y
            pitch = Mathf.Clamp(pitch, PitchMinMax.x, PitchMinMax.y);
            CurrentRotation = Vector3.SmoothDamp(CurrentRotation, new Vector3(pitch, yaw, 0), ref RotationSmoothVelocity, rotationSmoothTime); // set la rotation de la cam
            cameraTransform.eulerAngles = CurrentRotation; // assigner les rotations
            transform.eulerAngles = Vector3.up * CurrentRotation.y;
            Debug.Log(pitch);*/
        }

    }

    public void OnStartFollowing()
    {
        cameraTransform = Camera.main.transform; // set le transform
        isFollowing = true; // dire de suivre
        Cut(); // appeler cut()
    }

    void Follow()
    {
        cameraoffset.z = -distance; // set l'offset en y et z
        cameraoffset.y = height;
        //cameraTransform.position = Vector3.Lerp(cameraTransform.position, transform.position + transform.TransformVector(cameraoffset), smoothSpeed * Time.deltaTime); // placer la cam dependamment de l'offset
        cameraTransform.LookAt(transform.position); // faire regarder le perso par la cam
    }

    void Cut()
    {
        cameraoffset.z = -distance; // set le offset en y et z
        cameraoffset.y = height;
        cameraTransform.position = transform.position + transform.TransformVector(cameraoffset); // set la position de la camera
    }

    // Update is called once per frame
    void Update()
    {

    }
}
