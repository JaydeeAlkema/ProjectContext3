using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu( menuName = "ScriptableObjects/New Player Data", fileName = "PlayerData" )]
public class Player_ScriptableObject : ScriptableObject
{
	public float baseMovementSpeed = 5f;
	public float slopeForce = 100f;
	public float slopeForceRayLength = 2f;
	public float jumpMultiplier = 6;
	public AnimationCurve jumpfallOff = default;
	public float slideTime = 0.75f;
	[Space]
	public KeyCode[] jumpKeyCodes = { KeyCode.W, KeyCode.Space };
	public KeyCode[] slideKeyCodes = { KeyCode.S, KeyCode.DownArrow };
	[Space]
	public LayerMask hitMask = default;
	[Space]
	public int maxSpriteBlinkCount = 6;
	public float spriteBlinkInterval = 0.25f;
	[Space]
	[ReadOnly] public bool grounded = false;
	[ReadOnly] public bool isJumping = false;
	[ReadOnly] public bool isSliding = false;
}
