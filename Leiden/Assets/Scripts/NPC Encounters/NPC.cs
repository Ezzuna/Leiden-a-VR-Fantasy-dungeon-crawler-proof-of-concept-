using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;

public class NPC : MonoBehaviour
{
    public Transform tr;
    
    public string[] mData;
    
    private int index = 0;
    
    public Text mText;
    public Transform YawTransform;
    private Transform TargetTransform;
   
    private bool isTalk = false;
    private EncounterDecider attachedEncounter;
    public GameObject Dialog;
    private bool isShow = false;
    float mCurrentYawAngle;
    Vector3 mYawTarget;
    float  mYawAngleOffset, mAimAngleOffset;
    Vector3  mYawCross;
    public int MaxYawAngle = 180;



    private PlayerState playerState;
    private GameObject playerCamera;
    float fireRate;
    float timeToFire;

    private void Start()
    {
        isShow = false;
        Dialog.SetActive(isShow);
        attachedEncounter = this.GetComponent<EncounterDecider>();
        playerState = GameObject.Find("OVRCameraRig").GetComponent<PlayerState>();
        TargetTransform = GameObject.Find("OVRCameraRig").transform;
        playerCamera = GameObject.Find("CenterEyeAnchor");       
        fireRate = 1.0f;


    }
    void Update()
    {
        YawTransform.localEulerAngles = new Vector3(0, YawRotation(20 * Time.deltaTime), 0);
        Debug.DrawRay(YawTransform.position, YawTransform.forward * 100, Color.red);

        //Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        


        Ray mRay = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit mHi;

 
        
        Dialog.SetActive(isShow);
        if (Physics.Raycast(mRay, out mHi))
        {

            if (mHi.collider.gameObject.tag == "NPC")
            {
                playerState.setPlayerState(pState.playerInDialogue);
                isTalk = true;
                isShow = true;
                Dialog.SetActive(isShow);



             OVRInput.Update();
                if ((Input.GetKeyDown("t") || (OVRInput.Get(OVRInput.Button.One))) &&  Time.time > timeToFire)
                {
                    timeToFire = Time.time + fireRate;
                    if (index < mData.Length)
                    {
                        mText.text = "NPC:" + mData[index];
                        index = index + 1;
                    }
                    else
                    {
                        index = 0;
                        mText.text = "NPC:" + mData[index];
                    }
                }
            }

        }



    }

 

    public void OnBtnYes()
    {
        Debug.Log("Yes");
        attachedEncounter.PlayerConfirmed();
        playerState.setPlayerState(pState.playerIdle);
        Destroy(this.gameObject);
    }

    public void OnBtnNo()
    {
        Debug.Log("No");
        playerState.setPlayerState(pState.playerIdle);
        Destroy(this.gameObject);
    }

    float YawRotation(float speed)
    {
        mCurrentYawAngle = YawTransform.localEulerAngles.y;
        if (mCurrentYawAngle > 180)
        {
            mCurrentYawAngle = -(360 - mCurrentYawAngle);
        }

        mYawTarget = Vector3.ProjectOnPlane(TargetTransform.position - YawTransform.position, YawTransform.up).normalized;
        mYawAngleOffset = Vector3.Angle(YawTransform.forward, mYawTarget);

        if (mYawAngleOffset == 0)
        {
            return mCurrentYawAngle;
        }

        if (mYawAngleOffset < speed)
        {
            speed = mYawAngleOffset;
        }

        mYawCross = Vector3.Cross(mYawTarget, YawTransform.forward).normalized;
        if (mCurrentYawAngle > -MaxYawAngle && mYawCross == YawTransform.up)
        {
            return mCurrentYawAngle - speed;
        }
        else if (mCurrentYawAngle < MaxYawAngle && mYawCross != YawTransform.up)
        {
            return mCurrentYawAngle + speed;
        }
        return mCurrentYawAngle;
    }
}
