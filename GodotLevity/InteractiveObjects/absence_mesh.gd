extends Area2D

# Booleans
var fade

# Called when the node enters the scene tree for the first time.
func _ready():
	fade = false
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if fade:
		if $Sprite2D.modulate.a > 0:
			$Sprite2D.modulate.a -= 1.4 * delta
	else:
		if $Sprite2D.modulate.a < 1:
			$Sprite2D.modulate.a += 1.4 * delta
	pass

func _on_body_entered(body):
	if body.name == "MUSH_Player":
		fade = true
	pass # Replace with function body.


func _on_body_exited(body):
	if body.name == "MUSH_Player":
		fade = false
	pass # Replace with function body.
