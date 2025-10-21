using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData_SO", menuName = "Data/PlayerData")]
//继承ScriptableObject即可创建为SO文件
public class SampleData_SO : ScriptableObject
{
    //标签类型
    //[Header("xxx")]在SO盒上显示xxx标题
    //[Tooltip ("xxx")]在鼠标移动到下一个变量的变量名上时显示xxx内容
    //[Multiline]提供一个可以填写更多string内容的区域
    // [Space(xx)]添加xx距离的空白范围
    //[Range(x,y)]提供一个滑块，并且将数据限制在x到y的范围当中
    [Header("角色数据")]
    [Header("ID与头像")]
    [Tooltip ("玩家需要自己命名")]//标签作用
    public string playerName;//玩家名称
    public Sprite playerIcon;//头像
    [Multiline]public string playerDescription;//玩家介绍
    //Multiline表示介绍框，会有更多的填写范围

    [Space(10)]//空格（10）可以调整分区距离
    [Header("基础数据")]
    [Header("血量,最高100")]
    [Range(0,10)]public int playerHP;//血量
    //Range表示限制（x到y）的范围
    [Header("速度")]
    public float playerSpeed;//速度
    //也可以没有Range
    [Header("攻击力")]
    public int playerAttack;//攻击力

    [Space(10)]
    [Header("角色状态")]
    public StateType StateType;
    //StateType是Enum中设置的


    [Space(10)]
    [Header("体能，初始为0，体能影响战斗")]
    public int physicalFitness;//体能经验值
    [Range(0, 10)] public int physicalFitnessLevel;//体能等级
    

    [Header("独立开关")]
    public bool isDead;//是否死亡
    public bool attackable;//能否攻击
    public bool removable;//能否移动
    public bool invincible;//是否无敌
}
