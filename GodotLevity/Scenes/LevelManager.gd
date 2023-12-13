extends Node2D

# Booleans
var done
var shouldStartCount

# Integers
var count
var maxCount

# ScenePacks
var level

# Strings
var AREA_PREFAB
var LEVEL_PREFAB

func _delete_room():
	self.get_child(0).queue_free()
	pass
	
func _reset_room():
	_delete_room()
	shouldStartCount = true
	pass

# Called when the node enters the scene tree for the first time.
func _ready():
	count = 0
	maxCount = 50
	done = false
	shouldStartCount = false
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if !done:
		AREA_PREFAB = get_parent().name
		LEVEL_PREFAB = str("res://Scenes/", AREA_PREFAB, "/", get_child(0).name, ".tscn")
		#print(LEVEL_PREFAB)
		level = load(LEVEL_PREFAB)
		done = true
	
	if shouldStartCount:
		count += 1
		if count > maxCount:
			count = 0
			shouldStartCount = false
			var level_instance = level.instantiate()
			self.add_child(level_instance)
	pass
