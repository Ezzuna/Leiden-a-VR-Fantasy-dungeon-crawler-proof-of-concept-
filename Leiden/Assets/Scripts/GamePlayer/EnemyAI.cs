using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Enemy_Stats
{

    public float timer = 2.0f;
    public int fireSpeed = 15;
    public GameObject spell;
    public Transform firePoint;
    //Maximum rotation angle limit
    public int MaxPitchAngle = 60;
    public int MaxYawAngle = 60;

    //spinning speed
    public int Speed = 20;
    public int attackRange = 10;
    //Pitch rotating object
    public Transform PitchTransform;
    //Yaw rotating object
    public Transform YawTransform;
    public AudioClip ShootSound;
    private AudioSource AudioSource;
    //Target
    private Transform TargetTransform;

    Vector3 mPitchTarget, mYawTarget;
    float mCurrentPitchAngle, mCurrentYawAngle;

    Transform mTransform;

    float mPitchAngleOffset, mYawAngleOffset, mAimAngleOffset;
    Vector3 mPitchCross, mYawCross;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        mTransform = transform;
        TargetTransform = GameObject.Find("CenterEyeAnchor").transform;
    }

    void Update()
    {
        if (TargetTransform != null && IsCanAim())
        {
            //Rotate without using PitchTransform.Rotate(x,y,z) to prevent z-axis changes
            if (PitchTransform == YawTransform)
            {
                PitchTransform.localEulerAngles = new Vector3(PitchRotation(Speed * Time.deltaTime), YawRotation(Speed * Time.deltaTime), 0);
            }
            else
            {
                PitchTransform.localEulerAngles = new Vector3(PitchRotation(Speed * Time.deltaTime), 0, 0);
                YawTransform.localEulerAngles = new Vector3(0, YawRotation(Speed * Time.deltaTime), 0);
            }
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                EnemyDistanceCheck();
                timer = 2.0f;
            }            
        }
#if UNITY_EDITOR
        Debug.DrawRay(PitchTransform.position, PitchTransform.forward * 100, Color.red);
#endif
    }

    bool IsCanAim()
    {
        //The angle between the weapon-to-target vector and the front of the weapon is within 90 degrees.
        if (PitchTransform != null && YawTransform != null)
        {
            //mAimAngleOffset = Mathf.Acos(Vector3.Dot(transform.forward, (TargetTransform.position - PitchTransform.position).normalized)) * Mathf.Rad2Deg;
            mAimAngleOffset = Vector3.Angle(mTransform.forward, TargetTransform.position - PitchTransform.position);
            if (mAimAngleOffset < 90)
            {
                return true;
            }
        }
        return false;
    }

    float PitchRotation(float speed)
    {
        //Current rotation angle
        mCurrentPitchAngle = PitchTransform.localEulerAngles.x;
        if (mCurrentPitchAngle > 180)
        {
            mCurrentPitchAngle = -(360 - mCurrentPitchAngle);
        }

        //The weapon to the target vector, mapped to the normal vector on the weapon's yz screen
        mPitchTarget = Vector3.ProjectOnPlane(TargetTransform.position - PitchTransform.position, PitchTransform.right).normalized;

        //Calculate the angle between the current aiming direction and the expected direction. Greater than precision requires pitch rotation to adjust
        mPitchAngleOffset = Vector3.Angle(PitchTransform.forward, mPitchTarget);

        if (mPitchAngleOffset == 0)
        {
            return mCurrentPitchAngle;
        }

        if (mPitchAngleOffset < speed)
        {
            speed = mPitchAngleOffset;
        }

        //Calculate the cross product of two vectors to determine the left and right relationship of the vector
        mPitchCross = Vector3.Cross(mPitchTarget, PitchTransform.forward).normalized;
        if (mCurrentPitchAngle > -MaxPitchAngle && mPitchCross == PitchTransform.right)
        {
            return mCurrentPitchAngle - speed;
        }
        else if (mCurrentPitchAngle < MaxPitchAngle && mPitchCross != PitchTransform.right)
        {
            return mCurrentPitchAngle + speed;
        }
        return mCurrentPitchAngle;
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

    void fire(GameObject spell)
    {

        GameObject bullet;


        //bullet = BulletsPool.bulletsPoolInstance.GetPooledObject(spell);
        bullet = ObjectPool.current.GetObject(spell);
        if (bullet != null)                  
        {
            bullet.SetActive(true);         
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.rotation = firePoint.transform.rotation;
        }


        bullet.GetComponent<Rigidbody>().velocity = PitchTransform.forward * fireSpeed;
        PlayerSound(ShootSound);
        StartCoroutine(ObjectPool.current.Destroy(bullet, 3));
    }

    private void PlayerSound(AudioClip Sound)
    {
        AudioSource.clip = Sound;
        AudioSource.Play();
    }

    void EnemyDistanceCheck()
    {
        float diatanceToPlayer = Vector3.Distance(TargetTransform.position, transform.position);
        if (diatanceToPlayer < attackRange)
        {
            fire(spell);
        }
        
    }
}
