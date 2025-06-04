using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class GroupModel
{
    public string name;
    public List<StudentModel> students;
    public List<string> configuredGames; // or List<GameConfig> if structured
    public RewardProfile rewardProfile;
}