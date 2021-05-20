/*
Attach to Parent GameObject of the main camera which should also have the Attachment Hands and Hand Models as children
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Leap.Unity.Interaction;
using Leap.Unity.Attachments;

public class CameraPanBehavior : MonoBehaviour
{

    public float horizontalPanSpeed = 1000f;
    public float verticalPanSpeed = 500f;
    [Tooltip("Pan right when hand goes greater than this or left when hand goes less than the negative")]
    public float horizontalPanZone = 0.3f;
    [Tooltip("Pan up when hand goes greater than this")]
    public float topPanZone = 0.5f;
    [Tooltip("Pan up when hand goes less than this")]
    public float bottomPanZone = 0.2f;

    GameObject attachmentHands;
    GameObject leftAttachmentHand;
    GameObject rightAttachmentHand;
    GameObject leftAttachmentPalm;
    GameObject rightAttachmentPalm;

    Vector3 leftPalmLocalPos;
    Vector3 rightPalmLocalPos;

    float xRotation;
    float yRotation;
    bool rotationFlag;

    // Start is called before the first frame update
    void Start()
    {
        // Relies on a child GameObject beign the Attachment Hands
        attachmentHands = transform.Find("Attachment Hands").gameObject;
        leftAttachmentHand = attachmentHands.transform.Find("Attachment Hand (Left)").gameObject;
        rightAttachmentHand = attachmentHands.transform.Find("Attachment Hand (Right)").gameObject;
        leftAttachmentPalm = leftAttachmentHand.transform.Find("Palm").gameObject;
        rightAttachmentPalm = rightAttachmentHand.transform.Find("Palm").gameObject;

        xRotation = 0;
        yRotation = 0;
    }

    void FixedUpdate()
    {
        // Get local positions of the attachment hand palms
        leftPalmLocalPos = leftAttachmentPalm.transform.localPosition;
        rightPalmLocalPos = rightAttachmentPalm.transform.localPosition;
        rotationFlag = false;

        // Update yRotation when only right palm or left palm is past pan zone
        // transform.localRotation should only be called when actually panning to preserve framerate
        if (leftPalmLocalPos.x < (horizontalPanZone * -1f) && !(rightPalmLocalPos.x > horizontalPanZone))
        {
            yRotation += (leftPalmLocalPos.x + horizontalPanZone) * horizontalPanSpeed * Time.deltaTime;
            rotationFlag = true;
        }
        else if (rightPalmLocalPos.x > horizontalPanZone && !(leftPalmLocalPos.x < (horizontalPanZone * -1f)))
        {
            yRotation += (rightPalmLocalPos.x - horizontalPanZone) * horizontalPanSpeed * Time.deltaTime;
            rotationFlag = true;
        }


        // flags for if either hand is past the pan zones
        bool panDownFlag = leftPalmLocalPos.y < bottomPanZone || rightPalmLocalPos.y < bottomPanZone;
        bool panUpFlag = leftPalmLocalPos.y > topPanZone || rightPalmLocalPos.y > topPanZone;

        // Update xRotation when only one flag is true at a time
        if (panDownFlag && !panUpFlag)
        {
            float yPosition = rightPalmLocalPos.y < leftPalmLocalPos.y ? rightPalmLocalPos.y : leftPalmLocalPos.y;
            xRotation -= (yPosition - horizontalPanZone) * verticalPanSpeed * Time.deltaTime;
            rotationFlag = true;
        }
        else if (panUpFlag && !panDownFlag)
        {
            Debug.Log("Pan Up");
            float yPosition = rightPalmLocalPos.y > leftPalmLocalPos.y ? rightPalmLocalPos.y : leftPalmLocalPos.y;
            xRotation -= (yPosition - horizontalPanZone) * verticalPanSpeed * Time.deltaTime;
            rotationFlag = true;
        }


        if (rotationFlag)
        {
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            //transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }

}


