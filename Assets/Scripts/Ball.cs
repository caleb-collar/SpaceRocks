using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] private PaddleBehavior paddle;
    [SerializeField] private float xVelocity = 1f;
    [SerializeField] private float yVelocity = 12f;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioClip chargeSound;
    [SerializeField] private AudioClip fullyChargedSound;
    [SerializeField] private AudioClip dischargedSound;
    [SerializeField] private AudioClip beamSound;
    [Range(1,50)]
    [SerializeField] private float burn = 40;
    [Range(1,50)]
    [SerializeField] private float rechargeRate = 25;

    private Vector2 paddleBallOffset, directionVector;
    private Rigidbody2D ballRB;
    private AudioSource audioSource, paddleAudio;
    private Slider beamUIBar;
    private bool hasLaunched = false, below75 = false;
    private GameObject[] tractorable;
    private float beamMeter;
    
    void Start()
    {
        beamMeter = 100;
        ballRB = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        paddleAudio = paddle.GetComponent<AudioSource>();
        tractorable = GameObject.FindGameObjectsWithTag("Tractorable");
        beamUIBar = GameObject.FindGameObjectWithTag("StatusBar1").GetComponent<Slider>();
        paddleBallOffset = transform.position - paddle.transform.position;
    }
    
    void Update()
    {
        beamUIBar.value = beamMeter * 0.01f;
        
        if (!hasLaunched)
        {
            LockToPaddle();
            Launch(); 
        }

        if (hasLaunched && beamMeter >= 0 && Input.GetMouseButton(0))
        {
            SubtractFromMeter();
            if (!paddleAudio.isPlaying)
            {
                paddleAudio.clip = beamSound;
                paddleAudio.Play();
            }
            directionVector = (paddle.transform.position - transform.position).normalized;
            ballRB.AddForce(new Vector2(directionVector.x * 1.1f, -1.1f), ForceMode2D.Force);
            foreach (var obj in tractorable)
            {
                obj.SetActive(true);
            }
        } else
        {
            if (paddleAudio.clip == beamSound)
            {
                paddleAudio.Stop();
            }
            AddToMeter();
            foreach (var obj in tractorable)
            {
                obj.SetActive(false);
            }
        }
    }

    private void SubtractFromMeter()
    {
        if (beamMeter > 0)
        {
            beamMeter -= burn * Time.deltaTime;;
        }

        if (beamMeter <= 75f)
        {
            below75 = true;
        }

        if (beamMeter <= 0 && !audioSource.isPlaying)
        {
            audioSource.clip = dischargedSound;
            audioSource.Play();
        }
        
    }

    private void AddToMeter()
    {
        if (beamMeter < 100f)
        {
            beamMeter += rechargeRate * Time.deltaTime;
            //audioSource.PlayOneShot(chargeSound,0.8f);
        }

        if (hasLaunched && beamMeter >= 99f && !audioSource.isPlaying && below75)
        {
            audioSource.clip = fullyChargedSound;
            audioSource.Play();
            below75 = false;
        }
    }
    
    private void Launch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity,yVelocity);
            hasLaunched = true;
        }
    }

    void LockToPaddle()
    {
        Vector2 paddlePosition = paddle.transform.position;
        transform.position = paddlePosition + paddleBallOffset;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (hasLaunched)
        {
            audioSource.PlayOneShot(audioClips[UnityEngine.Random.Range(0, audioClips.Length)],1f);
        }
    }
}
