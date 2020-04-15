using UnityEngine;
using UnityEngine.UI;
using SaveGame;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    
    public int total_levels = 20;
    public GameObject moving_area;
    public Sprite locked;
    public Sprite unlocked;

    private int levels_cleared = 1;
    
    
    // Start is called before the first frame update
    void Start() {
        Save_game obj = new Save_game();
        obj.LoadFile();
        levels_cleared = obj.get_levels_cleared();
        Debug.Log(levels_cleared);

        for(int i=1; i<=total_levels; i++) {
            string s = "lvl ("+i.ToString()+")";
            Image btn = GameObject.Find(s).GetComponent<Image>();
            if(i <= levels_cleared+1){
                btn.sprite = unlocked;
            } else {
                btn.sprite = locked;
            }
            // Debug.Log(btn);
        }
    }

    public void go_to_level(int lvl){
        if(lvl <= levels_cleared+1){
            SceneManager.LoadScene(lvl);
        }
    }

}
