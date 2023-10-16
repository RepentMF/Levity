extends Area2D

# Strings
var UPGRADE_TYPE

func _apply_upgrade(player):
	match UPGRADE_TYPE:
		"walljump":
			player.maxWallJumpCount += 1
pass

# Called when the node enters the scene tree for the first time.
func _ready():
	UPGRADE_TYPE = get_meta("UPGRADE_TYPE")
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _on_body_entered(body):
	if body.name == "MUSH_Player":
		_apply_upgrade(body)
		queue_free()
	pass # Replace with function body.
