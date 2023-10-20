extends Area2D

# Booleans
var RESPAWN_GRAVITY

# Integers
var ID

# NodePath
var level
var player

# Called when the node enters the scene tree for the first time.
func _ready():
	RESPAWN_GRAVITY = get_meta("RESPAWN_GRAVITY")
	ID = get_meta("ID")
	level = get_parent().get_parent()
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if player == null:
		pass
	else:
		if (player.isDead || player.hasReset) && player.currentRespawnPoint == ID:
			level._reset_room()
			player.position = global_position
			player.isDead = false
			player.hasReset = false
			player.hasKey = false
			
			if level.count == 0:
				player.animatedSprite2D.visible = true
			elif level.count < level.maxCount:
				player.animatedSprite2D.visible = false
		elif player.currentRespawnPoint != ID:
			player = null
		pass
	pass


func _on_body_entered(body):
	if body.name == "MUSH_Player" && ID >= body.currentRespawnPoint:
		player = body
		player.currentRespawnPoint = ID
	pass
