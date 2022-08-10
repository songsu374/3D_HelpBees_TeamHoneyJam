using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    [SerializeField]
    Transform characterBody;
    [SerializeField]
    Transform cameraArm;
    //[SerializeField]
    //GameObject characterPos;

    public static TPSCamera instance;
    public float playerSpeed = 10f;
    //Vector3 offset;


    private void Awake()
    {
        instance = this;
        //offset = transform.position - characterPos.transform.position;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
    }

    public Vector2 moveInput;

    public float attackSpeed = 20;
    public void Move()
    {
        if (false == Player.instance.missionPenel.Panel.activeSelf)
        {

            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            bool isMove = moveInput.magnitude != 0;
            

            if (isMove)
            {
                Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
                Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
                Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

                characterBody.forward = moveDir;
                transform.position += moveDir * Time.deltaTime * playerSpeed;

            }


        }
        //Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);


    }



    void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if(x< 180f)
        {
            x = Mathf.Clamp(x, 1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
}
