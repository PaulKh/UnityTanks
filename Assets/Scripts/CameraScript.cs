using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float speed = 10.0f; 
    public GameObject playerTank;
    public float sensitivity = 1.0f;

    private Vector3 positionDeltaWithTank;
    private float yaw = 0.0f;
    private float pitch = 50.0f;

    protected Transform transformCamera;
    protected Transform transformParent;

    protected Vector3 localRotation;
    protected float cameraDistance = 10f;

    public float mouseSensitivity = 4f;
    public float scrollSensitivity = 2f;
    public float orbitDampening = 10f;
    public float scrollDampening = 6f;

    public bool cameraDisabled = false;


    // Use this for initialization
    void Start()
    {
        transformCamera = this.transform;
        transformParent = this.transform.parent;
        //positionDeltaWithTank = transform.position - playerTank.transform.position;
        //transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    //called after all updated
    void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            cameraDisabled = !cameraDisabled;
        }
        if(!cameraDisabled)
        {
            if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                localRotation.x += Input.GetAxis("Mouse X") * mouseSensitivity;
                localRotation.y -= Input.GetAxis("Mouse Y") * mouseSensitivity;

                //clamp the y rotation
                localRotation.y = Mathf.Clamp(localRotation.y, 0f, 90f);
            }

            if(Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                float scrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
                scrollAmount *= this.cameraDistance * 0.3f;
                this.cameraDistance += scrollAmount * -1f;
                this.cameraDistance = Mathf.Clamp(this.cameraDistance, 1.5f, 100.0f);

            }
        }

        Quaternion qt = Quaternion.Euler(localRotation.y, localRotation.x, 0);
        this.transformParent.rotation = Quaternion.Lerp(this.transformParent.rotation, qt, Time.deltaTime * orbitDampening);

        if(this.transformCamera.localPosition.z != this.cameraDistance * -1f)
        {
            this.transformCamera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this.transform.localPosition.z, this.cameraDistance * -1f, Time.deltaTime * scrollDampening));
        }
    }

    public void tankMove(Vector3 position)
    {
        this.transformParent.position = position;
    }
}
