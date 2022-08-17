using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryTeller : MonoBehaviour
{
    string starting = "Gasoline, iron, evil... Can you smell it? Our forest is in danger.You are our only hope Foxy help us save the forest",
           movement_Tutorial = "Press 'A,D' or arrow keys to move",
           jump_Tutorial = "Press 'Spacebar' to jump. Press consecutively for double jump.",
           enemyFrog_Tutorial = "There's a Frog up ahead. Possessed by evil. If you want to kill you have to jump on it. There is a %15 chance of dropping Health Gem",
           gem_Tutorial = "Diamonds are for score. If you collect all the gems in the level you will earn a badge.",
           healthGem_Tutorial = "Cherry is health gem. You have 6 hp. If your health is full, you can not collect it",
           checkpoint_Tutorial = "The last checkpoint you pass will be active and if you die you will respawn there.",
           switch_Tutorial = "This switch will enable or disable the obstacle in your path. Mostly it will disable it.",
           platform_Tutorial = "There are different types of platforms. Some of them work two-way, some one-way and some will fall when you step on them and will respawn with you",
           portal_Tutorial = "Portals for transport to another portal. Not all portals have a destination", 
           possess_Tutorial = "Press the 'T' button to turn transmission mode on or off. In transmission mode, you can use 'w,a,s,d' or arrow keys to control laser. When the laser hits the robot, press the 'P' key to possess the robot. Then the robot will be active. They will help you achieve your goal, but they are not exactly the same as you. Once you take control of the robot, press 'B' to return to Foxy. ",
           Sacrifice_Tutorial = "Sometimes success needs sacrifice",
           MovementRestrictor_Tutorial = "On the level, there are hidden areas. These areas will restrict your movement for some time or until you enter one of this area again", 
           fakeGrounds = "Be careful where you step on.",
           maze_Tutorial = "Press 'M' to view the whole map and find your way.",
           bossFirstmeet = "Oh! Here is the evil that wants to harm our forest. Tank Merc. Defeat him!",
           enemyEagle_Tutorial = "Eagle is ahead. don't get too close. You don't want him to chase you.",
           bouncePad_Tutorial = "Bounce Pad will make you bounce higher",
           robotRespawn_Tutorial= "When the robot dies, you have to suicide for robot's respawn",
           restart_Tutorial="Press 'R' to restart level",
           levelSelect_Tutorial= "Move through the level with 'w,a,s,d' or arrow keys. Press 'Spacebar' to play this level. Unlock the level by completing the previous level. If you finish the level in less than the target time or collect all the gems, you will get badges at the top corner of the level.",
           slammer_Tutorial= " The slammer will slam down as you pass under it and then slowly reset. The moment of reset may be the right time to pass";
        



    public bool is_movement_Tutorial, is_jump_Tutorial, is_enemyFrog_Tutorial, is_gem_Tutorial, is_healthGem_Tutorial,
        is_checkpoint_Tutorial, is_switch_Tutorial, is_platform_Tutorial, is_portal_Tutorial, is_possess_Tutorial,
        is_Sacrifice_Tutorial, is_MovementRestrictor_Tutorial, is_fakeGrounds, is_maze_Tutorial, is_bossFirstmeet,
        is_enemyEagle_Tutorial,is_starting, is_slammer_Tutorial, is_bouncePad_Tutorial, is_robotRespawn_Tutorial,
        is_levelSelect_Tutorial, is_restart_Tutorial;


    public Image img;
    public Text name, txt;
    public GameObject panel;

    public Sprite image;

    public static StoryTeller instance;


    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        panel.SetActive(false);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("alandayýz");
            panel.SetActive(true);
            img.sprite = image;

            if (is_movement_Tutorial)
            {
                EditUI("OFY", movement_Tutorial);
            }
            else if (is_bossFirstmeet)
            {
                EditUI("OFY", bossFirstmeet);
            }
            else if (is_checkpoint_Tutorial)
            {
                EditUI("OFY", checkpoint_Tutorial);
            }
            else if (is_enemyEagle_Tutorial)
            {
                EditUI("OFY", enemyEagle_Tutorial);
            }
            else if (is_enemyFrog_Tutorial)
            {
                EditUI("OFY", enemyFrog_Tutorial);
            }
            else if (is_fakeGrounds)
            {
                EditUI("OFY", fakeGrounds);
            }
            else if (is_gem_Tutorial)
            {
                EditUI("OFY", gem_Tutorial);
            }
            else if (is_healthGem_Tutorial)
            {
                EditUI("OFY", healthGem_Tutorial);
            }
            else if (is_jump_Tutorial)
            {
                EditUI("OFY", jump_Tutorial);
            }
            else if (is_maze_Tutorial)
            {
                EditUI("OFY", maze_Tutorial);
            }
            else if (is_MovementRestrictor_Tutorial)
            {
                EditUI("OFY", MovementRestrictor_Tutorial);
            }
            else if (is_platform_Tutorial)
            {
                EditUI("OFY", platform_Tutorial);
            }
            else if (is_portal_Tutorial)
            {
                EditUI("OFY", portal_Tutorial);
            }
            else if (is_possess_Tutorial)
            {
                EditUI("OFY", possess_Tutorial);
            }
            else if (is_Sacrifice_Tutorial)
            {
                EditUI("OFY", Sacrifice_Tutorial);
            }
            else if (is_switch_Tutorial)
            {
                EditUI("OFY", switch_Tutorial);
            }
            else if(is_starting)
            {
                EditUI("OFY", starting);
            }
            else if (is_slammer_Tutorial)
            {
                EditUI("OFY",slammer_Tutorial);
            }
            else if (is_bouncePad_Tutorial)
            {
                EditUI("OFY",bouncePad_Tutorial);
            }
            else if (is_robotRespawn_Tutorial)
            {
                EditUI("OFY", robotRespawn_Tutorial);
            }
            else if (is_restart_Tutorial)
            {
                EditUI("OFY",restart_Tutorial);
            }
            else if (is_levelSelect_Tutorial)
            {
                Debug.Log("level Select");
                EditUI("OFY",levelSelect_Tutorial);
            }
            else Debug.Log("tutorial yok ");


        }
        else
        {
            Debug.Log("Player girmedi giren= "+other.tag);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        panel.SetActive(false);
    }

    public void EditUI(string _name, string _text)
    {
        name.text = _name;
        txt.text = _text;
    }

}
