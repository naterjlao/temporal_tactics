// © 2019 Flying Saci Game Studio
// written by Sylker Teles
// reviewed and edited by Henry Dana

using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// The Loot class.
/// </summary>
[Serializable]
public class Loot
{
    /// <summary>
    /// The loot GameObject prefab.
    /// </summary>
    public GameObject loot;

    /// <summary>
    /// The loot drop chance. 1 to 100% chance of drop.
    /// </summary>
    [Range(0,1)] public float dropChance;
}
public class Chest
{
    public GameObject chest;
}


/// <summary>
/// These are the ways you can open the box.
/// </summary>
public enum OpeningMethods { OpenOnCollision, OpenOnKeyPress, OpenOnTouch }

/// <summary>
/// Loot Box class.
/// </summary>
public class LootBox : MonoBehaviour
{
    // list to store previously opened loot boxes
    public static List<LootBox> openedChests = new List<LootBox>();

    // flag to check if the chest has previously been opened
    private bool hasBeenOpened = false;

    /// <summary>
    /// How should the player open the box?
    /// </summary>
    /// Loot box is to be opened via collision
    public OpeningMethods openingMethod = OpeningMethods.OpenOnCollision;

    /// <summary>
    /// You can use a tag for the player that can open the box.
    /// </summary>
    public string playerTag = "Player";

    /// <summary>
    /// You can also use a key to open the box. Player must be close.
    /// </summary>
    public KeyCode keyCode = KeyCode.Space;

    /// <summary>
    /// Activates the leaping animation.
    /// </summary>
    public bool bouncingBox = true;

    /// <summary>
    /// Close the box after player goes away
    /// </summary>
    public bool closeOnExit = true;

    /// <summary>
    /// The loot inside the box. 
    /// You can use any prefab and set up their drop chance.
    /// </summary>
    public List<Loot> boxContents = new List<Loot>();

    /// <summary>
    /// Flags when player is close enough to open the box.
    /// </summary>
    private bool isPlayerAround;

    /// <summary>
    /// Flags when the box is open avoiding opening it twice.
    /// </summary>
    /// <value><c>true</c> if is open; otherwise, <c>false</c>.</value>
    public bool isOpen { get; set; }

    /// <summary>
    /// The box animator for leaping, open and close animations.
    /// </summary>
    Animator animator;

    /// <summary>
    /// You can call OnBoxOpen and OnBoxClose as events, for
    /// instance, you can get what's inside the box.
    /// </summary>
    public event Action <GameObject[]> OnBoxOpen;

    // Start is called before the first frame update
    void Start()
    {
        // gets the animator
        animator = GetComponent<Animator>();

        // set the animation to bounce or not
        BounceBox(bouncingBox);
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    private void Update()
    {
        // when player is close enough to open the box
        if (isPlayerAround)
        {
            // in case of Key Press method for opening the box,
            // waits for a key to be pressed
            if (Input.GetKey(keyCode)) Open();
        }
    }

    /// <summary>
    /// Bounces the box.
    /// </summary>
    /// <param name="bounceIt">If set to <c>true</c> bounce it.</param>
    public void BounceBox (bool bounceIt)
    {
        // flag the animator property "bounce" accordingly
        if (animator) animator.SetBool("bounce", bounceIt);
    }

    /// <summary>
    /// Open the box.
    /// </summary>
    public void Open ()
    {
        // avoid opening when it's already open
        if (isOpen) return;
        isOpen = true;

        // play the open animation
        if (animator) animator.Play("Open");

        // checks to see if the chest has previously been opened
        if (!hasBeenOpened)
        {
            // add gold to the player's gold count
            playerStats.goldCount += 300;
            string goldCountStringUpdated = playerStats.goldCount.ToString();
            Debug.Log(goldCountStringUpdated);

            // add this chest to the list of chests that have been opened
            openedChests.Add(this);

            // mark the chest as opened
            hasBeenOpened = true;
        }
    }

    /// <summary>
    /// Close the box
    /// </summary>
    public void Close()
    {
        // avoid closing when it's already open
        if (!isOpen) return;
        isOpen = false;

        // play the close animation
        if (animator) animator.Play("Close");
    }

    /// <summary>
    /// When player touches or click on the treasure box
    /// </summary>
    private void OnMouseDown()
    {
        // checks if the opening method is OpenOnTouch
        if (openingMethod != OpeningMethods.OpenOnTouch) return;

        // Open the box.
        Open();
    }

    /// <summary>
    /// When something hits our treasure box.
    /// </summary>
    /// <param name="collision">Collision.</param>
    private void OnCollisionEnter(Collision collision)
    {
        // OnCollisionMethod is not for OpenOnTouch method
        if (openingMethod == OpeningMethods.OpenOnTouch) return;

        // check if the hitting object is our player
        if (collision.gameObject.tag == playerTag)
        {
            // if the method is OpenOnKeyPress, let's just flag the player as close
            if (openingMethod == OpeningMethods.OpenOnKeyPress) isPlayerAround = true;

            // otherwise, open the box.
            else Open();
        }
    }

    /// <summary>
    /// When player goes away from box.
    /// </summary>
    /// <param name="collision">Collision.</param>
    private void OnCollisionExit(Collision collision)
    {
        // flag the player as away.
        isPlayerAround = false;

        // if the box is suppose to close on exit, close it
        if (closeOnExit) Close();
    }
}
