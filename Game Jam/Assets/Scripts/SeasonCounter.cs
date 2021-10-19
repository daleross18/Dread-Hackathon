using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonCounter : MonoBehaviour
{
    [SerializeField] private TextMesh yearLabel=null;
    private int _year = 0;
    private string _season;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      _year = GameStateManager.GetNumCompletedSeasons()/4;
      if (GameStateManager.GetSeason() == Season.Summer) {
         _season = "Summer";
      }
      if (GameStateManager.GetSeason() == Season.Fall) {
         _season = "Fall";
      }
      if (GameStateManager.GetSeason() == Season.Winter) {
         _season = "Winter";
      }
      if (GameStateManager.GetSeason() == Season.Spring) {
         _season = "Spring";
      }
      if (GameStateManager.GetGameState() == GameState.Play) {
         yearLabel.text = "Year " + _year + ", " + _season;
      }

    }
}
