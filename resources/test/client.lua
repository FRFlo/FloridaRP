-- Show a number input dialog
RegisterCommand("input", function(source, args, rawCommand)
	TriggerEvent('FloInput:create', 'Enter a number', 'number', {
		min = 0,
		max = 100,
		placeholder = 'Enter a number between 0 and 100',
		value = '50'
	}, function(data)
		print('You entered: ' .. data)
	end)
end, false)

-- Show a text input dialog
RegisterCommand("input2", function(source, args, rawCommand)
	TriggerEvent('FloInput:create', 'Enter a text', 'text', {
		minLength = 1,
		maxLength = 10,
		placeholder = 'Enter a text between 1 and 10 characters',
		value = 'Hello World!'
	}, function(data)
		print('You entered: ' .. data)
	end)
end, false)

-- Show a small text input dialog
RegisterCommand("input3", function(source, args, rawCommand)
	TriggerEvent('FloInput:create', 'Enter a small text', 'small_text', {
		minLength = 1,
		maxLength = 10,
		placeholder = 'Enter a small text between 1 and 10 characters',
		value = 'Hello World!'
	}, function(data)
		print('You entered: ' .. data)
	end)
end, false)

-- -- Example usage: small_text
-- RegisterCommand('input', function(source, args, rawCommand)
-- 	TriggerEvent("FloInput:create", 'Enter your name', 'small_text',
-- 		{
-- 			minlength = 3,
-- 			maxLength = 32,
-- 			secret = false,
-- 			placeholder = "test oui",
-- 			value = "valeur par défaut"
-- 		}, function(result)
-- 			print('Your name is: ' .. result)
-- 		end)
-- end, false)

-- -- Example usage: text
-- RegisterCommand('input2', function(source, args, rawCommand)
-- 	TriggerEvent("FloInput:create", 'Enter your lore', 'text',
-- 		{
-- 			minlength = 3,
-- 			maxLength = 64,
-- 			secret = false,
-- 			placeholder = "test oui",
-- 			value = "valeur par défaut"
-- 		}, function(result)
-- 			print('Your name is: ' .. result)
-- 		end)
-- end, false)

-- -- Example usage: number
-- RegisterCommand('input3', function(source, args, rawCommand)
-- 	TriggerEvent("FloInput:create", 'Enter your age', 'number',
-- 		{
-- 			min = 0,
-- 			max = 100,
-- 			secret = false,
-- 			placeholder = "test oui",
-- 			value = "valeur par défaut"
-- 		}, function(result)
-- 			print('Your age is: ' .. result)
-- 		end)
-- end, false)