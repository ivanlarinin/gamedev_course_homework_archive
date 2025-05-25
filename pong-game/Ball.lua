Ball = Class{}

function Ball:init(x, y, width, height)
    self.x = x
    self.y = y
    self.width = width
    self.height = height

    -- random vel
    if servingPlayer == 1 then
        self.dx = 150
    else
        self.dx = -150
    end

   
    self.dy = math.random(-50, 50)
end

-- ball collision
function Ball:collides(box)
    if self.x > box.x + box.width or self.x + self.width < box.x then
        return false
    end
    if self.y > box.y + box.height or self.y + self.height < box.y then
        return false
    end

    return true
end

-- ball reset
function Ball:reset()
    self.x = VIRTUAL_WIDTH / 2 - 2
    self.y = VIRTUAL_HEIGHT / 2 - 2
    
    if servingPlayer == 1 then
        self.dx = 150
    else
        self.dx = -150
    end

    self.dy = math.random(-50, 50)
end

-- updates ball's position
function Ball:update(dt)
    self.x = self.x + self.dx * dt
    self.y = self.y + self.dy * dt
end

-- ball draw
function Ball:render()
    love.graphics.rectangle('fill', self.x, self.y, 4, 4)
end