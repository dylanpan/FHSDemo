EventSystem = 
{
	listEvent = {},
	listTimerEvent = {}
}

function EventSystem:RegisterEvent(eventType, callback, caller)
	if self.listEvent[eventType] == nil then
		self.listEvent[eventType] = {}
	end

	local event = 
	{
		callback = callback,
		caller = caller
	}
	
	table.insert(self.listEvent[eventType], event)
end

function EventSystem:UnregisterEvent(eventType, callback, caller)
	if self.listEvent[eventType] ~= nil then
		for i = #self.listEvent[eventType], 1, -1 do
			local event = self.listEvent[eventType][i]
			if event.caller == caller and event.callback == callback then
				table.remove(self.listEvent, i)
			end
		end
	end
end

function EventSystem:SendEvent(eventType, param)
	if self.listEvent[eventType] ~= nil then
		for i = 1, #self.listEvent[eventType] do
			local event = self.listEvent[eventType][i]
			if event.caller == nil then
				event.callback(param)
			else
				event.callback(event.caller, param)	
			end
		end
	end 
end

function EventSystem:SendTimerEventHandler(eventType, callback, param, delay, times, caller)
	if self.listTimerEvent[eventType] == nil then
		self.listTimerEvent[eventType] = {}
		for i = 1, #self.listTimerEvent[eventType] do
			local event = self.listTimerEvent[eventType][i]
			self:InvokeCallback(event)
		end
	end 
end

function EventSystem:RemoveTimerEvent(eventType)
	self.listTimerEvent[eventType] = nil
end

function EventSystem:Update(deltaTime)
	for i = #self.listTimerEvent, 1, -1 do
		local len = #self.listTimerEvent[i]
		for j = len, 1, -1 do
			local event = self.listTimerEvent[i][j]
			if event.time <= 0 then
				self:InvokeCallback(event)
			end
		end
	end
end

function EventSystem:InvokeCallback(event)
	if event.caller == nil then
		event.callback(param)
	else
		event.callback(event.caller, param)	
	end
end