using UnityEngine;
using UnityEngine.UI;
using SaveGame;
using UnityEngine.SceneManagement;

public class player_movement : MonoBehaviour
{
	public Rigidbody rb;
    private int jumps_left = 2;
    private int max_jumps = 2;
    private int levels_cleared = 1;
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
    public Text jumpsText;

    private void Start() {
        // set PlayerMaterial color to true
        // rb.velocity = new Vector3(0, 0, forwardForce);
        // TrueState = Random.ColorHSV();
        // FalseState = Random.ColorHSV();
        
        Save_game obj = new Save_game();
        // obj.SaveFile(6,0);
        obj.LoadFile();//update from saved file
        max_jumps = obj.get_jumps();
        levels_cleared = obj.get_levels_cleared();
        // Debug.Log(max_jumps);
        // max_jumps = 6;  //  hard coded for testing

        trueMat.SetColor("_Color", TrueState);
        falseMat.SetColor("_Color", FalseState);
        PlayerMat.SetColor("_Color", FalseState);

    }

    // Update is called once per frame
    void FixedUpdate() {
        // rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        // transform.position += transform.forward * Time.deltaTime * forwardForce;
        jumpsText.text = jumps_left.ToString()+" Jumps Left";
        transform.Translate(Vector3.forward * Time.deltaTime * forwardForce, Space.World);
        // transform.position.x = 0;
        // rb.velocity = new Vector3(0, 0, forwardForce);

        // if(Input.GetKey("d")){

        // }
        
    }

    private void OnCollisionEnter(Collision other) {
        jumps_left = max_jumps;
    }
    private void OnCollisionStay(Collision colInfo) {
        // jumps_left = 2;

        // Debug.Log(colInfo.collider.tag);
        if((PlayerState && colInfo.collider.tag == "FalseState") || (!PlayerState && colInfo.collider.tag == "TrueState") || (colInfo.collider.tag == "HellState")){
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
                increment_jumps();
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
            if(jumps_left != (max_jumps-1)){
                rb.AddTorque(transform.right * torqueForce);
                // Debug.Log(jumps_left);
            }
            // Jump
        } else {
            jumps_left = 0;
            Debug.Log("No jumps left");
        }
    }

    private void increment_jumps() {
        // increment jumps after every 4 levels
        // jumps are persisted
        // show message if nre jump count is greater than current jump count
        bool show_bonus = false;
        int level = SceneManager.GetActiveScene().buildIndex;
        if(level == 4 && max_jumps == 2) {
            show_bonus = true;
        } else if (level == 8 && max_jumps == 3) {
            show_bonus = true;
        } else if (level == 12 && max_jumps == 4) {
            show_bonus = true;
        } else if (level == 16 && max_jumps == 5) {
            show_bonus = true;
        } else if (level == 20 && max_jumps == 6) {
            show_bonus = true;
        }

        if(show_bonus) {
            GameObject wp = GameObject.Find("WinPanel");
            wp.transform.GetChild(1).gameObject.SetActive(true);
            Save_game obj = new Save_game();
            max_jumps = max_jumps + 1;
        }
        if(level > levels_cleared){
            levels_cleared = level;
        }
        Save_game obj1 = new Save_game();
        obj1.SaveFile(max_jumps, levels_cleared);
    }



}
