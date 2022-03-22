using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GUIManager gUIManager;

    public float power;
    public bool canJump;
    bool changing;
    public int dimension;

    public float currentPowerX;
    public float currentPowerY;

    public Vector2 minPower;
    public Vector2 maxPower;

    public Rigidbody2D rb;
    Camera cam;
    public Vector2 force;
    public Vector3 startPoint;
    public Vector3 endPoint;
    Touch touch;
    public Vector3 direction;

    [Header("Trajectory")]
    public LineRenderer line;
    public Vector2 _velocity;

    public AudioSource Pull;
    public AudioSource Snap;
    public AudioSource pickup;


    private void Start() {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        line = FindObjectOfType<LineRenderer>();

    }

    private void Update() {
        direction = startPoint - endPoint;

        currentPowerX = Mathf.Clamp(direction.x, minPower.x, maxPower.x);
        currentPowerY = Mathf.Clamp(direction.y, minPower.y, maxPower.y);


        if (Input.touchCount == 2) {
            TriggerDimension();
        }
        StartCoroutine(SwitchDimensions());

        

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Floor") {
            canJump = true;
           
        }
        if(collision.tag == "PowerUp") {
            StartCoroutine(Boost());
        }
        if(collision.tag == "Pickup") {
            pickup.Play();
            gUIManager.starsCollected += 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.tag == "Floor") {
            canJump = false;
        }
    }



    public IEnumerator SwitchDimensions() {
        
        switch (dimension) {
            case 0:

                rb.gravityScale = 2;
                Movement();
                break;
            case 1:

                rb.gravityScale = -2;
                Movement();
                break;
        }
        yield return null;
    }

    public IEnumerator Boost() {
        rb.velocity = new Vector3(force.x, force.y, 0) * power * 1.5f;

        yield return null;
    }


    public void Movement() {
        if (!changing) {
            if (rb.velocity.magnitude <= 0 ) {
                canJump = true;
            }

            if (Input.GetMouseButtonDown(0)) {
                Pull.Play();
                startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                startPoint.z = 0;
                if (canJump) {
                    line.gameObject.SetActive(true);
                }

            }

            if (Input.GetMouseButton(0)) {
                endPoint = cam.ScreenToWorldPoint(Input.mousePosition);

                if (canJump) {
                    _velocity = new Vector2(currentPowerX, currentPowerY) * power;

                    Vector2[] trajectory = Plot(rb, (Vector2)transform.position, _velocity, 500);

                    line.positionCount = trajectory.Length;

                    Vector3[] position = new Vector3[trajectory.Length];

                    for (int i = 0; i < trajectory.Length; i++) {
                        position[i] = trajectory[i];
                    }

                    line.SetPositions(position);
                }
            }

            if (Input.GetMouseButtonUp(0)) {
                Pull.Stop();
                Snap.Play();
                line.gameObject.SetActive(false);
                endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = 0;
                Jump();
            }
        }
    }

    public void Jump() {
        if (canJump) {
            force = new Vector2(currentPowerX, currentPowerY);
            rb.velocity = new Vector3(force.x, force.y, 0) * power;
        }
        canJump = false;
    }

    public Vector2[] Plot(Rigidbody2D rigidbody2D, Vector2 pos, Vector2 velocity, int steps) {
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations; 
        Vector2 gravityAccel = rigidbody2D.gravityScale * Physics2D.gravity * (timestep * timestep);

        float drag = 1f - timestep * rigidbody2D.drag;
        Vector2 moveStep = (velocity) * timestep;

        for(int i = 0; i <steps; i++) {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }

        return results;
    }

    public void TriggerDimension() {
        changing = true;

        if (dimension == 0) {
            dimension = 1;
        }
        else {
            dimension = 0;
        }

        changing = false;

   }
}
