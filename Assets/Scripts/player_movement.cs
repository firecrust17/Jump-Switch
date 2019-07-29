using UnityEngine;

public class player_movement : MonoBehaviour
{
	public Rigidbody rb;
    private int jumps_left = 2;
    public bool PlayerState = true;
    private bool is_game_over = false;
    public Color TrueState;
    public Color FalseState;
    public float forwardForce;
    public float jumpForce;
    public float torqueForce;
    public Material trueMat;
    public Material falseMat;
    public Material PlayerMat;
    public GameObject LostPanel;
    public GameObject WinPanel;

    private void Start() {
        // set PlayerMaterial color to true
        // rb.velocity = new Vector3(0, 0, forwardForce);
        // TrueState = Random.ColorHSV();
        // FalseState = Random.ColorHSV();
        trueMat.SetColor("_Color", TrueState);
        falseMat.SetColor("_Color", FalseState);
        PlayerMat.SetColor("_Color", FalseState);

    }

    // Update is called once per frame
    void FixedUpdate() {
        // rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        // transform.position += transform.forward * Time.deltaTime * forwardForce;
        transform.Translate(Vector3.forward * Time.deltaTime * forwardForce, Space.World);
        // transform.position.x = 0;
        // rb.velocity = new Vector3(0, 0, forwardForce);

        // if(Input.GetKey("d")){

        // }
        
    }

    private void OnCollisionEnter(Collision other) {
        jumps_left = 2;
    }
    private void OnCollisionStay(Collision colInfo) {
        // jumps_left = 2;

        // Debug.Log(colInfo.collider.tag);
        if((PlayerState && colInfo.collider.tag == "FalseState") || (!PlayerState && colInfo.collider.tag == "TrueState")){
            gameOver(false);
        }
        if(colInfo.collider.tag == "FinishState"){
            gameOver(true);
        }
    }
    
    // private void OnCollisionExit(Collision colInfo) {
        // Debug.Log("Exit");
        // jumps_left = 2;

        // Debug.Log(colInfo.collider.tag);
        // if((PlayerState && colInfo.collider.tag == "FalseState") || (!PlayerState && colInfo.collider.tag == "TrueState")){
        //     gameOver();
        // }
    // }
    
    private void gameOver(bool is_complete){
        if(!is_game_over){
            // Debug.Log("Game Over");
            is_game_over = true;
            if(is_complete){
                WinPanel.SetActive(true);
            } else {
                LostPanel.SetActive(true);
            }
            Invoke("disable_script", 1);
            // rb.AddForce(0, 0, -forwardForce * Time.deltaTime);
        }
    }

    private void disable_script() {
        // Debug.Log("invoked");
        this.enabled = false;
    }

    public void SwitchState(){
        PlayerState = !PlayerState;
        if(!PlayerState){
            PlayerMat.SetColor("_Color", TrueState);
        } else {
            PlayerMat.SetColor("_Color", FalseState);
        }
    }

    public void Jump(){
        // Debug.Log(jumps_left);
        jumps_left--;
        if(jumps_left >= 0){
            // rb.AddForce(0, jumpForce, 0);
            rb.velocity = Vector3.up * jumpForce;
            // Debug.Log(jumps_left);
            if(jumps_left == 0){
                rb.AddTorque(transform.right * torqueForce);
                // Debug.Log(jumps_left);
            }
            // Jump
        } else {
            Debug.Log("No jumps left");
        }
    }

}
